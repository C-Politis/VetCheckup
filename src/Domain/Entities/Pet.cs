﻿namespace VetCheckup.Domain.Entities;

public class Pet
{

    #region Properties

    public required DateTime DateOfBirth { get; set; }

    public required string MicrochipId { get; set; }

    public required string Name { get; set; }

    public required Owner Owner { get; set; }

    public required Guid PetId { get; set; }

    public required Sex Sex { get; set; }

    public required string Species { get; set; }

    #endregion

}
