﻿namespace VetCheckup.Application.Common.Validators;

public class AbnValidator : AbstractValidator<string?>
{

    #region Fields

    private static readonly int[] Weights = [10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19];

    #endregion

    #region Constructors

    public AbnValidator()
    {
        _ = this.RuleFor(e => e)
            .Cascade(CascadeMode.Stop)
            .Matches("^(\\d *?){11}$")
            .WithMessage("ABN must be an 11 digit number")
            .Must(IsValidAbn)
            .WithMessage("ABN is invalid")
            .When(e => !string.IsNullOrWhiteSpace(e));
    }

    #endregion

    #region Methods

    private bool IsValidAbn(string abn)
    {
        int[] abnDigits = abn.Select(d => (int) char.GetNumericValue(d)).ToArray();
        abnDigits[0]--;
        int sum = abnDigits.Zip(Weights, (digit, weight) => digit * weight).Sum();

        return sum % 89 == 0;
    }

    #endregion
}
