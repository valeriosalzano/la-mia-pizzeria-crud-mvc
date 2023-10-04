using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{
    [Table("categories")]
    public class Category
    {
        [Key, Column(name: "id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The category name is required.")]
        [StringLength(50)]
        [Column(name: "name")]
        public string? Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
    }
}
