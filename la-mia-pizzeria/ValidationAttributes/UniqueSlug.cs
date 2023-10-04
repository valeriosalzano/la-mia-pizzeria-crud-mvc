using la_mia_pizzeria.Database;
using la_mia_pizzeria.Models;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.ValidationAttributes
{
    public class UniqueSlug : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            value ??= "";
            string parsedValue = (string)value;
            Pizza? evaluatedPizza = validationContext.ObjectInstance as Pizza;

            using (PizzeriaContext db = new PizzeriaContext())
            {
                // TODO FIX EDIT
                Pizza? foundPizza = db.Pizzas.Where(p => p.Slug == parsedValue).FirstOrDefault();
                
                if (foundPizza is not null && foundPizza.PizzaId != evaluatedPizza?.PizzaId)
                    return new ValidationResult($"A slug for this name already exists, choose a different name.");
            }
            return ValidationResult.Success;
        }
    }
}
