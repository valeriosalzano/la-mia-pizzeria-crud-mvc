using la_mia_pizzeria.ValidationAttributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace la_mia_pizzeria.Models
{

    [Table("pizzas"), Index(nameof(Slug), IsUnique = true)]
    public class Pizza
    {
        #region *** Table Columns ***

        [Column(name:"id"), Key]
        public int PizzaId { get; set; }

        [Column(name:"name", TypeName = "VARCHAR(100)"), Required(AllowEmptyStrings = false, ErrorMessage = "The name of the pizza is required.")]
        public string? Name { get; set; }

        [Column(name: "slug", TypeName = "VARCHAR(100)"), Required(AllowEmptyStrings = true), UniqueSlug]
        public string? Slug { get; set; }

        [Column(name:"price", TypeName = "DECIMAL(5, 2)"), Range(0.01,999.99,ErrorMessage = "The price must be between 0,01 and 999,99."), Required(ErrorMessage = "The price is required.")]
        public decimal Price { get; set; }

        [Column(name: "description", TypeName = "VARCHAR(1000)"), WordsCount(Min = 5, ErrorMessage = "The description must have 5 words at least."), Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Column(name: "img_path", TypeName = "VARCHAR(1000)"), Required(AllowEmptyStrings = true)]
        public string? ImgPath { get; set; }

        [Column(name: "category_id")]
        public int? CategoryId { get; set; }
        #endregion

        #region *** Relations ***
        public Category? Category { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        #endregion
    }
}
