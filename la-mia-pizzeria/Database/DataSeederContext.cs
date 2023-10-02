using la_mia_pizzeria.Utility;
using la_mia_pizzeria.Models;
using System.Net;
using System.Reflection.Metadata;

namespace la_mia_pizzeria.Database
{
    public static class DataSeederContext
    {
        public static void PopulateDb()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Database.EnsureCreated();

                int testPizza = db.Pizzas.Count();
                if (testPizza == 0)
                {
                    Console.WriteLine("pizzas count 0");
                    // GET FILE CONTENT
                    string filePath = Path.GetFullPath(@"./Database/Seeder/pizzas.csv");
                    List<string[]> fileContent = Helper.GetCSVContent(filePath, ";");

                    // PARSING TO ADDRESSES
                    List<Pizza> pizzasList = GetPizzasFromFile(fileContent);

                    foreach(Pizza pizza in pizzasList)
                    {
                        Console.WriteLine("Aggiungo la pizza " + pizza.Name);
                        try
                        {
                            db.Add(pizza);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.InnerException);
                        }
                    }
                }

                db.SaveChanges();
            }
        }

        private static List<Pizza> GetPizzasFromFile(List<string[]> fileContent)
        {
            List<Pizza> pizzasList = new List<Pizza>();

            int rowCounter = 0;
            foreach (string[] row in fileContent)
            {
                if (rowCounter > 0)
                {
                    if (row.Length != 3)
                    {
                        Console.WriteLine("Rilevata riga non formattata correttamente. Riga " + rowCounter);
                    }
                    else
                    {
                        try
                        {
                            pizzasList.Add(new Pizza { Name = row[0], Slug = Helper.GetSlugFromString(row[0]), Price = decimal.Parse(row[1]), Description = row[2]  });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Impossibile creare una pizza con i dati della riga {rowCounter}");
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
                rowCounter++;
            }
            return pizzasList;
        }
    }
}
