using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetCheckup.Application.UseCases.Pets.UpdatePet
{
    public class UpdatePetRequestValidator : AbstractValidator<UpdatePetRequest>
    {
        #region Constructors

        public UpdatePetRequestValidator()
        {
            _ = this.RuleFor(e => e.Name)
                .MaximumLength(100)
                .NotEmpty();

            _ = this.RuleFor(e => e.Species)
                .MaximumLength(50)
                .NotEmpty();

            _ = this.RuleFor(e => e.Sex)
                .IsInEnum();

            _ = this.RuleFor(e => e.PetId)
                .NotEmpty();

            _ = this.RuleFor(e => e.OwnerId)
                .NotEmpty();
        }

        #endregion

    }
}
