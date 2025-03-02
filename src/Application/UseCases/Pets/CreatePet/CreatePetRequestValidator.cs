using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.UseCases.Pets.CreatePet;


public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{

    #region Constructors

    public CreatePetRequestValidator()
    {
         
        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty();

        _ = this.RuleFor(e => e.Species)
            .MaximumLength(50)
            .NotEmpty();

        _ = this.RuleFor(e => e.Sex)
            .IsInEnum()
            .NotEmpty();

        _ = this.RuleFor(e => e.OwnerId)
            .NotNull();

    }

    #endregion

}
