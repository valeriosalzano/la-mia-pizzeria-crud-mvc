using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{
    [Table("pizzas")]
    public class Pizza
    {
        [Column(name:"id"), Key]
        public int PizzaId { get; set; }
        [Column(name:"name", TypeName = "VARCHAR(100)"), Required]
        public string? Name { get; set; }
        [Column(name:"price", TypeName = "DECIMAL(5, 2)"), Required]
        public decimal Price { get; set; }
        [Column(name: "description", TypeName = "VARCHAR(1000)"),DefaultValue("")]
        public string? Description { get; set; }

    }
}
