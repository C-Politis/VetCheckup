﻿namespace VetCheckup.Application.Common.Validators;

public class AbnValidator : AbstractValidator<string?>
{

    #region Constructors

    public AbnValidator()
    {
        _ = this.RuleFor(e => e)
            .Matches("^(\\d *?){11}$")
            .Must(e =>
            {
                int[] weights = [10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19];
                int[] abn = Array.ConvertAll(e.ToCharArray(), digit => (int) char.GetNumericValue(digit));
                abn[0]--;
                int sum = 0;
                for (int i = 0; i <= 11; i++)
                {
                    abn[i] *= weights[i];
                    sum += abn[i];
                }

                return sum % 89 == 0;
            })
            .When(e => !string.IsNullOrWhiteSpace(e));
    }

    #endregion

}
