using la_mia_pizzeria.Utility;
using la_mia_pizzeria.Models;
using System.Net;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace la_mia_pizzeria.Database
{
    public static class DataSeeder
    {
        public static void PopulateDb()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Database.EnsureCreated();

                int testPizza = db.Pizzas.Count();
                if (testPizza == 0)
                {
                    // GET FILE CONTENT
                    string filePath = Path.GetFullPath(@"./Database/Seeder/pizzas.csv");
                    List<string[]> fileContent = Helper.GetCSVContent(filePath, ";");

                    // PARSING TO ADDRESSES
                    List<Pizza> pizzasList = GetPizzasFromFile(fileContent);

                    foreach(Pizza pizza in pizzasList)
                    {
                        Debug.WriteLine($"Adding {pizza.Name} pizza.");
                        try
                        {
                            db.Add(pizza);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine(ex.InnerException);
                        }
                    }
                }

                db.SaveChanges();
            }
        }

        public static List<Pizza> GetPizzasFromFile(List<string[]> fileContent)
        {
            List<Pizza> pizzasList = new List<Pizza>();

            int rowCounter = 0;
            foreach (string[] row in fileContent)
            {
                if (rowCounter > 0)
                {
                    if (row.Length != 3)
                    {
                        Debug.WriteLine("Wrong format. Row " + rowCounter);
                    }
                    else
                    {
                        try
                        {
                            pizzasList.Add(new Pizza { Name = row[0], Slug = Helper.GetSlugFromString(row[0]), Price = decimal.Parse(row[1]), Description = row[2]  });
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Could not create a pizza with row {rowCounter} data.");
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                rowCounter++;
            }
            return pizzasList;
        }
    }
}
