using la_mia_pizzeria.Database;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria.Utility;

namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public ActionResult Index()
        {
            try
            {
                using(PizzeriaContext db = new PizzeriaContext())
                {
                    List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                    return View("Index", pizzas);
                }
            }catch
            {
                return View("Error");
            }
        }

        // GET: PizzaController/Details/pizza-slug
        public ActionResult Details(string slug)
        {
            try
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    Pizza pizza = db.Pizzas.Where(pizza => pizza.Slug == slug).First();
                        return View("Details", pizza);

                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PizzaController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pizza newPizza)
        {
            newPizza.Description ??= "";
            newPizza.ImgPath ??= "";
            newPizza.Slug = Helper.GetSlugFromString(newPizza.Name);

            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Create),newPizza);
            }
            try
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    db.Add(newPizza);
                    db.SaveChanges();

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Create),newPizza);
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(string slug)
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string slug, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(string slug)
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string slug, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PizzaController/PopulateDb
        public ActionResult PopulateDb()
        {
            try
            {
                DataSeederContext.PopulateDb();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
