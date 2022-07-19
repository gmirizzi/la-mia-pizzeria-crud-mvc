using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validators
{
    public class FiveWordsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string textValue = (string)value;
            char[] separators = new char[] { ' ', '.', ',' };
            string[] subs = textValue.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (subs.Count()<5)
            {
                return new ValidationResult("Il campo deve contenere almeno 5 parole");
            } else
            {
                return ValidationResult.Success;
            }
        }
    }
}
