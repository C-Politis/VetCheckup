using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Infrastructure.Data.Seeding;

public static class DatabaseSeeder
{
    private const string AdminUserName = "admin";
    private const string OrganisationManagerUserName = "orgmanager";
    private const string VetUserName = "vetdemo";
    private const string OwnerUserName = "ownerdemo";
    private const string OrganisationName = "Vet Checkup Clinic";
    private const string OrganisationAbn = "12345678901";
    private const string DemoPassword = "Password123!";

    public static async Task SeedDevelopmentDataAsync(ApplicationDbContext dbContext, IPasswordHasher<User> passwordHasher, CancellationToken cancellationToken = default)
    {
        var changed = false;

        if (!await dbContext.Users.AnyAsync(user => user.UserName == AdminUserName, cancellationToken))
        {
            dbContext.Users.Add(CreateUser(AdminUserName, "admin@vetcheckup.local", Roles.Administrator, passwordHasher));
            changed = true;
        }

        var organisation = await dbContext.Set<Organisation>()
            .Include(e => e.OrganisationManager)
            .FirstOrDefaultAsync(e => e.Abn == OrganisationAbn, cancellationToken);

        if (organisation is null)
        {
            var organisationManager = CreateOrganisationManager(passwordHasher);
            organisation = CreateOrganisation(organisationManager);
            organisationManager.Organisation = organisation;

            dbContext.Add(organisation);
            changed = true;
        }

        if (!await dbContext.Users.AnyAsync(user => user.UserName == VetUserName, cancellationToken))
        {
            dbContext.Add(CreateVet(passwordHasher, organisation));
            changed = true;
        }

        if (!await dbContext.Users.AnyAsync(user => user.UserName == OwnerUserName, cancellationToken))
        {
            dbContext.Add(CreateOwner(passwordHasher));
            changed = true;
        }

        if (!changed)
        {
            return;
        }

        await ((IApplicationDbContext)dbContext).SaveChangesAsync(cancellationToken);
    }

    private static OrganisationManager CreateOrganisationManager(IPasswordHasher<User> passwordHasher)
    {
        var user = CreateUser(OrganisationManagerUserName, "orgmanager@vetcheckup.local", Roles.OrganisationManager, passwordHasher);

        return new OrganisationManager
        {
            OrganisationManagerId = Guid.NewGuid(),
            User = user,
            Address = new Address
            {
                AddressId = Guid.NewGuid(),
                Country = "Australia",
                PostalCode = "3001",
                State = "VIC",
                StreetAddress = "200 Bourke Street",
                Suburb = "Melbourne"
            },
            ContactDetails = new Contact
            {
                ContactId = Guid.NewGuid(),
                Email = "orgmanager@vetcheckup.local",
                Mobile = "0400000003"
            },
            DateOfBirth = new DateTime(1985, 11, 7),
            Title = Title.Mr,
            FirstName = "Chris",
            MiddleName = "A",
            LastName = "Morgan",
            Suffix = Suffix.None,
            Organisation = null
        };
    }

    private static Organisation CreateOrganisation(OrganisationManager organisationManager)
    {
        var organisation = new Organisation
        {
            OrganisationId = Guid.NewGuid(),
            Abn = OrganisationAbn,
            Address = new Address
            {
                AddressId = Guid.NewGuid(),
                Country = "Australia",
                PostalCode = "3002",
                State = "VIC",
                StreetAddress = "250 Swanston Street",
                Suburb = "Melbourne"
            },
            ContactDetails = new Contact
            {
                ContactId = Guid.NewGuid(),
                Email = "clinic@vetcheckup.local",
                Mobile = "0400000004"
            },
            Name = OrganisationName,
            OrganisationType = OrganisationType.Clinic,
            VetOrganisations = new List<VetOrganisation>(),
            OrganisationManager = organisationManager
        };

        return organisation;
    }

    private static User CreateUser(string userName, string email, Roles role, IPasswordHasher<User> passwordHasher)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            NormalizedUserName = userName.ToUpperInvariant(),
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Role = role
        };

        user.PasswordHash = passwordHasher.HashPassword(user, DemoPassword);

        return user;
    }

    private static Vet CreateVet(IPasswordHasher<User> passwordHasher, Organisation organisation)
    {
        var user = CreateUser(VetUserName, "vet@vetcheckup.local", Roles.Vet, passwordHasher);

        var vet = new Vet
        {
            VetId = Guid.NewGuid(),
            User = user,
            Address = new Address
            {
                AddressId = Guid.NewGuid(),
                Country = "Australia",
                PostalCode = "3000",
                State = "VIC",
                StreetAddress = "100 Collins Street",
                Suburb = "Melbourne"
            },
            ContactDetails = new Contact
            {
                ContactId = Guid.NewGuid(),
                Email = "vet@vetcheckup.local",
                Mobile = "0400000001"
            },
            DateOfBirth = new DateTime(1988, 5, 14),
            Title = Title.Dr,
            FirstName = "Alex",
            MiddleName = "J",
            LastName = "Taylor",
            Suffix = Suffix.None,
            VetOrganisations = new List<VetOrganisation>()
        };

        var vetOrganisation = new VetOrganisation
        {
            Vet = vet,
            Organisation = organisation,
            IsPrimaryOrganisation = true
        };

        vet.VetOrganisations.Add(vetOrganisation);
        organisation.VetOrganisations.Add(vetOrganisation);

        return vet;
    }

    private static Owner CreateOwner(IPasswordHasher<User> passwordHasher)
    {
        var user = CreateUser(OwnerUserName, "owner@vetcheckup.local", Roles.Owner, passwordHasher);

        var owner = new Owner
        {
            OwnerId = Guid.NewGuid(),
            User = user,
            Address = new Address
            {
                AddressId = Guid.NewGuid(),
                Country = "Australia",
                PostalCode = "2000",
                State = "NSW",
                StreetAddress = "10 George Street",
                Suburb = "Sydney"
            },
            ContactDetails = new Contact
            {
                ContactId = Guid.NewGuid(),
                Email = "owner@vetcheckup.local",
                Mobile = "0400000002"
            },
            DateOfBirth = new DateTime(1990, 9, 21),
            Title = Title.Ms,
            FirstName = "Jamie",
            MiddleName = "R",
            LastName = "Walker",
            Suffix = Suffix.None,
            Pets = new List<Pet>()
        };

        owner.Pets.Add(new Pet
        {
            PetId = Guid.NewGuid(),
            Owner = owner,
            DateOfBirth = new DateTime(2021, 2, 3),
            MicrochipId = "999000111222333",
            Name = "Mochi",
            Sex = Sex.Female,
            Species = "Cat"
        });

        return owner;
    }
}
