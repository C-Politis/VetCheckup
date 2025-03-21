﻿using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet;

public class UpdatePetRequest : IRequest
{

    #region Properties
    
    public required Guid PetId { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string? Name { get; set; }

    public Sex? Sex { get; set; }

    public string? Species { get; set; }

    public string? MicrochipId { get; set; }

    public Guid? OwnerId { get; set; }

    #endregion

}
