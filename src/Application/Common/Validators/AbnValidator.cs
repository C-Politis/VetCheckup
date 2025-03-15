namespace VetCheckup.Application.Common.Validators;

public class AbnValidator : AbstractValidator<string>
{

    #region constructors

    public AbnValidator()
    {
        _ = this.RuleFor(e => e)
            .Matches("^(\\d *?){11}$")
            .Must(e =>
            {
                int[] weights = [10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19];
                int[] abn = Array.ConvertAll(e.Split(""), d => int.Parse(d));
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
