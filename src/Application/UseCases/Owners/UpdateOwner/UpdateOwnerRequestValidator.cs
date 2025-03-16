using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerRequestValidator : AbstractValidator<UpdateOwnerRequest>
    {
    
        #region Constructors

        public UpdateOwnerRequestValidator()
        {
            _ = this.RuleFor(e => e.Address).SetValidator(new UpdateAddressRequestValidator());
            _ = this.RuleFor(e => e.ContactDetails).SetValidator(new UpdateContactRequestValidator());


            _ = this.RuleFor(e => e.FirstName)
                .MaximumLength(100)
                .NotEmpty()
                .When(e => e.FirstName != null);

            _ = this.RuleFor(e => e.MiddleName)
                .MaximumLength(100)
                .NotEmpty()
                .When(e => e.MiddleName != null);

            _ = this.RuleFor(e => e.LastName)
                .MaximumLength(100)
                .NotEmpty()
                .When(e => e.LastName != null);

            _ = this.RuleFor(e => e.Title)
                .IsInEnum()
                .When(e => e.Title != null);

            _ = this.RuleFor(e => e.Suffix)
                .IsInEnum()
                .When(e => e.Suffix != null);

        }

        #endregion
    
    }
}
