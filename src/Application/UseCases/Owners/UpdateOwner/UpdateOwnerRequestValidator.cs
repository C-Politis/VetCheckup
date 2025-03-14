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

            _ = this.RuleFor(e => e.Name)
                .MaximumLength(100)
                .NotEmpty()
                .When(e => e.Name != null);

        }

        #endregion
    
    }
}
