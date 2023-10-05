using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria.Models
{
    public class PizzaFormModel
    {
        public required Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredientsId { get; set; }
    }
}
