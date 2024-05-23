using System.ComponentModel.DataAnnotations;

public class CheckRangeString : ValidationAttribute
{
    private readonly string[] _validValues;

    public CheckRangeString(string[] validValues)
    {
        this._validValues = validValues;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var memberName = validationContext.DisplayName ?? validationContext.MemberName;
            var stringValue = value as string;
            if (stringValue != null && !this._validValues.Contains(stringValue))
            {
                return new ValidationResult($"{memberName} parameter is invalid.");
            }
        }
        return ValidationResult.Success;
    }
}

