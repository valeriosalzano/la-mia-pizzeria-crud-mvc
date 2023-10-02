using la_mia_pizzeria.ValidationAttributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{

    [Table("pizzas"), Index(nameof(Slug), IsUnique = true)]
    public class Pizza
    {
        [Column(name:"id"), Key]
        public int PizzaId { get; set; }

        [Column(name:"name", TypeName = "VARCHAR(100)"), Required]
        public string? Name { get; set; }

        [Column(name: "slug", TypeName = "VARCHAR(100)"), Required]
        public string? Slug { get; set; }

        [Column(name:"price", TypeName = "DECIMAL(5, 2)"), Range(0.01,999.99),Required]
        public decimal Price { get; set; }

        [Column(name: "description", TypeName = "VARCHAR(1000)"), WordsCount(Min = 5)]
        public string? Description { get; set; }

        [Column(name: "img_path", TypeName = "VARCHAR(1000)")]
        public string? ImgPath { get; set; }
    }
}
