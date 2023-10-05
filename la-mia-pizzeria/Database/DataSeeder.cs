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

                #region CHECK CATEGORY TABLE
                int testCategory = db.Categories.Count();
                if (testCategory == 0)
                {
                    db.Add(new Category { Name = "Classics" });
                    db.Add(new Category { Name = "Specialty" });
                    db.Add(new Category { Name = "Innovations" });
                    db.Add(new Category { Name = "Appetizers" });
                }
                db.SaveChanges();
                #endregion
                #region CHECK INGREDIENTS TABLE
                int testIngredient = db.Ingredients.Count();
                if(testIngredient == 0)
                {
                    // GET FILE CONTENT
                    string filePath = Path.GetFullPath(@"./Database/Seeder/ingredients.csv");
                    List<string[]> fileContent = Helper.GetCSVContent(filePath, ";");

                    // PARSING TO INGREDIENTS
                    List<Ingredient> ingredientsList = GetIngredientsFromFile(fileContent);

                    foreach (Ingredient ingredient in ingredientsList)
                    {
                        Debug.WriteLine($"Adding {ingredient.Name} ingredient.");
                        try
                        {
                            db.Add(ingredient);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine(ex.InnerException);
                        }
                    }
                    db.SaveChanges();
                }

                #endregion
                #region CHECK PIZZAS TABLE
                int testPizza = db.Pizzas.Count();
                if (testPizza == 0)
                {
                    // GET FILE CONTENT
                    string filePath = Path.GetFullPath(@"./Database/Seeder/pizzas.csv");
                    List<string[]> fileContent = Helper.GetCSVContent(filePath, ";");

                    // PARSING TO PIZZAS
                    List<Pizza> pizzasList = GetPizzasFromFile(fileContent);

                    using (PizzeriaContext db2 = new PizzeriaContext())
                    {
                        try
                        {
                            foreach (Pizza pizza in pizzasList)
                            {
                                Debug.WriteLine($"Adding {pizza.Name} pizza.");
                                db2.Add(pizza);
                            }
                            db2.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine(ex.InnerException);
                        }
                    }
                    db.SaveChanges();
                }
                #endregion
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
                    if (row.Length != 5)
                    {
                        Debug.WriteLine("Wrong format. Row " + rowCounter);
                    }
                    else
                    {
                        try
                        {

                            Pizza newPizza = new Pizza
                            {
                                Name = row[0],
                                Slug = Helper.GetSlugFromString(row[0]),
                                Price = decimal.Parse(row[1]),
                                Description = row[2],
                                CategoryId = int.Parse(row[3]),
                                ImgPath = "",

                            };
                            //string[] ingredientsIds = row[4].Split(",");
                            //foreach (string ingredientId in ingredientsIds)
                            //{
                            //    using (PizzeriaContext db = new PizzeriaContext())
                            //    {
                            //        Ingredient? ingredient = db.Ingredients.Where(i => i.IngredientId == int.Parse(ingredientId)).FirstOrDefault();
                            //        newPizza.Ingredients.Add(ingredient);
                            //    }
                            //}

                            pizzasList.Add(newPizza);
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

        public static List<Ingredient> GetIngredientsFromFile(List<string[]> fileContent)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();

            int rowCounter = 0;
            foreach (string[] row in fileContent)
            {
                if (rowCounter > 0)
                {
                    if (row.Length != 2)
                    {
                        Debug.WriteLine("Wrong format. Row " + rowCounter);
                    }
                    else
                    {
                        try
                        {
                            ingredientsList.Add(new Ingredient
                            {
                                Name = row[1],
                            });
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Could not create an ingredient with row {rowCounter} data.");
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                rowCounter++;
            }
            return ingredientsList;
        }
    }
}
