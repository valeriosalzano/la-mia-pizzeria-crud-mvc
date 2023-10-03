using la_mia_pizzeria.Database;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria.Utility;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public ActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Index", pizzas);
            }
        }

        // GET: PizzaController/Details/pizza-slug
        public ActionResult Details(string slug)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizza = db.Pizzas.Where(pizza => pizza.Slug == slug).FirstOrDefault();

                if (pizza is null)
                    return NotFound("Can't find the pizza.");
                else
                    return View("Details", pizza);
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
            PrepareForValidation(newPizza);

            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Create),newPizza);
            }
            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Add(newPizza);
                db.SaveChanges();
            }
            return View(nameof(Details),newPizza.Slug);
        }

        // GET: PizzaController/Edit/pizza-slug
        public ActionResult Edit(string slug)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizza = db.Pizzas.Where(pizza => pizza.Slug == slug).FirstOrDefault();

                if (pizza is null)
                    return NotFound("Can't find the pizza.");
                else
                    return View(nameof(Edit), pizza);
            }
        }

        // POST: PizzaController/Edit/pizza-slug
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string slug, Pizza modifiedPizza)
        {
            PrepareForValidation(modifiedPizza);
            if (!ModelState.IsValid)
            {
                RedirectToAction(nameof(Edit), modifiedPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                try
                {
                    Pizza originalPizza = db.Pizzas.Where(pizza => pizza.Slug == slug).First();
                    EntityEntry<Pizza> originalPizzaEntity = db.Entry(originalPizza);
                    originalPizzaEntity.CurrentValues.SetValues(modifiedPizza);
                    db.SaveChanges();
                    return View(nameof(Details), originalPizza.Slug);
                }
                catch
                {
                    return NotFound("Can't find the desired pizza.");
                }
            }
        }

        // POST: PizzaController/Delete/pizza-slug
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string slug)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                try
                {
                    Pizza deletedPizza = db.Pizzas.Where(pizza => pizza.Slug == slug).First();
                    db.Remove(deletedPizza);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return NotFound("Can't find the desired pizza.");
                }
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
                return View("Error");
            }
        }

        private void PrepareForValidation(Pizza pizza)
        {
            pizza.Description ??= "";
            pizza.ImgPath ??= "/img/pizza-default.jpg";
            pizza.Slug = Helper.GetSlugFromString(pizza.Name);
        }
    }
}
