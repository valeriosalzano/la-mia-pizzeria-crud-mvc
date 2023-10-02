using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.ValidationAttributes
{
    public class WordsCount : ValidationAttribute
    {
        public int Min { get; set; } = 1;
        public int Max { get; set; } = int.MaxValue;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            

            if (value is string)
            {
                string input = (string)value ?? "";
                int wordsCount = input.Trim().Split(" ").Length;

                if (wordsCount < Min)
                {
                    return new ValidationResult($"Field must contain {Min} words at least");
                }
                if (wordsCount > Max) 
                {
                    return new ValidationResult($"Field must contain no more than {Max} words");
                }

                return ValidationResult.Success;

            }

            return new ValidationResult("Invalid field value");


        }
    }
}
