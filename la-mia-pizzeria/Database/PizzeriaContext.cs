using la_mia_pizzeria.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace la_mia_pizzeria.Database
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzeriaContext;Integrated Security=True;TrustServerCertificate=True");
        }

    }
}
