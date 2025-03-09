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
                .NotEmpty()
                .When(e => e.Name != null);

            _ = this.RuleFor(e => e.Species)
                .MaximumLength(50)
                .NotEmpty()
                .When(e => e.Species != null);

            _ = this.RuleFor(e => e.Sex)
                .IsInEnum();
        }

        #endregion

    }
}
