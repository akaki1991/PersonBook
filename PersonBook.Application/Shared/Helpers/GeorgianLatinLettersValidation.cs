using FluentValidation.Validators;

namespace PersonBook.Application.Shared.Helpers
{
    public class GeorgianLatinLettersValidation : PropertyValidator
    {
        public GeorgianLatinLettersValidation()
            : base("{PropertyName} must be only in Georgian or only in Latin letters.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var str = context.PropertyValue as string;

            var georgianLetters = LanguageLetters.GeorgianLetters;
            var englishLetters = LanguageLetters.EnglishLetters;

            var hasGeorgian = false;
            var hasEnglish = false;

            foreach (var georgianLetter in georgianLetters)
            {
                if (str.Contains(georgianLetter))
                {
                    hasGeorgian = true;
                    break;
                }
            }

            foreach (var englishLetter in englishLetters)
            {
                if (str.Contains(englishLetter))
                {
                    hasEnglish = true;
                    break;
                }
            }

            var isValid = hasEnglish ^ hasGeorgian;

            if (isValid)
            {
                return true;
            }

            return false;
        }
    }
}
