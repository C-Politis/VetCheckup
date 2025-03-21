﻿using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{

    #region Constructors

    public UpdateContactRequestValidator()
    {
        _ = this.RuleFor(e => e.Email)
            .MaximumLength(100)
            .When(e => !string.IsNullOrWhiteSpace(e.Email));

        _ = this.RuleFor(e => e.Mobile)
            .Matches("^\\d{0,20}$")
            .When(e => !string.IsNullOrWhiteSpace(e.Mobile));
    }

    #endregion

}
