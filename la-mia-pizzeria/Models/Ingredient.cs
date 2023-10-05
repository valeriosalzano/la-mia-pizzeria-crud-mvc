using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{
    [Table(name: "ingredients")]
    public class Ingredient
    {
        [Key, Column(name: "id")]
        public int IngredientId { get; set; }
        [Column(name: "name"),Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }

        public List<Pizza>? Pizza { get; set; }
    }
}
