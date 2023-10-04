using la_mia_pizzeria.Database;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria.Utility;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using la_mia_pizzeria.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ICustomLogger _logger;
        private readonly PizzeriaContext _database;

        public PizzaController(ICustomLogger logger, PizzeriaContext db)
        {
            _logger = logger;
            _database = db;
        }
        // GET: PizzaController
        public ActionResult Index()
        {
            try
            {
                List<Pizza> pizzas = _database.Pizzas.Include(p => p.Category).ToList<Pizza>();
                return View("Index", pizzas);
            }
            catch
            {
                _logger.WriteLog("Catching exception at Pizza>Index");
                return NotFound();
            }
        }

        // GET: PizzaController/Details/pizza-slug
        public ActionResult Details(string slug)
        {
            try
            {
                Pizza? pizza = _database.Pizzas.Where(pizza => pizza.Slug == slug).Include(pizza => pizza.Category).FirstOrDefault();

                if (pizza is null)
                {
                    _logger.WriteLog($"Catching a null reference at Pizza>Details>{slug}");
                    return NotFound("Can't find the pizza.");
                }
                else
                    return View("Details", pizza);
            }
            catch
            {
                _logger.WriteLog($"Catching exception at Pizza>Details>{slug}");
                return NotFound();
            }
        }

        // GET: PizzaController/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                List<Category> categories = _database.Categories.ToList();
                PizzaFormModel formModel = new PizzaFormModel { Pizza = new Pizza(), Categories = categories };

                return View("Create", formModel);
            }
            catch
            {
                _logger.WriteLog($"Catching exception at GET Pizza>Create");
                return NotFound();
            }
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaFormModel formData)
        {
            try
            {
                ModelState.Clear();
                PrepareForValidation(formData.Pizza);
                if (!TryValidateModel(formData))
                {
                    PrepareFormModel(formData);
                    return View(nameof(Create), formData);
                }
                _database.Add(formData.Pizza);
                _database.SaveChanges();
                return RedirectToAction(nameof(Details), new { slug = formData.Pizza.Slug });
            }
            catch
            {
                _logger.WriteLog($"Catching exception at POST Pizza>Create");
                return NotFound();
            }
        }

        // GET: PizzaController/Edit/pizza-slug
        public ActionResult Edit(string slug)
        {
            try
            {
                Pizza? pizza = _database.Pizzas.Where(pizza => pizza.Slug == slug).Include(pizza => pizza.Category).FirstOrDefault();

                if (pizza is null)
                {
                    _logger.WriteLog($"Catching a null reference at GET Pizza>Edit>{slug}");
                    return NotFound("Can't find the pizza.");
                }
                else
                {
                    List<Category> categories = _database.Categories.ToList();
                    PizzaFormModel formModel = new PizzaFormModel { Pizza = pizza, Categories = categories };
                    return View(nameof(Edit),formModel);
                }
            }
            catch
            {
                _logger.WriteLog($"Catching exception at GET Pizza>Edit>{slug}");
                return NotFound();
            }
        }

        // POST: PizzaController/Edit/pizza-slug
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string slug, PizzaFormModel formData)
        {
            try
            {
                ModelState.Clear();
                PrepareForValidation(formData.Pizza);
                if (!TryValidateModel(formData))
                {
                    PrepareFormModel(formData);
                    return View(nameof(Edit), formData);
                }
                Pizza originalPizza = _database.Pizzas.Where(pizza => pizza.Slug == slug).First();
                EntityEntry<Pizza> originalPizzaEntity = _database.Entry(originalPizza);
                originalPizzaEntity.CurrentValues.SetValues(formData.Pizza);
                _database.SaveChanges();
                return RedirectToAction(nameof(Details), new {slug = formData.Pizza.Slug});
            }
            catch
            {
                _logger.WriteLog($"Catching exception at POST Pizza>Edit>{slug}");
                return NotFound();
            }
            
        }

        // POST: PizzaController/Delete/pizza-slug
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string slug)
        {
            try
            {
                Pizza deletedPizza = _database.Pizzas.Where(pizza => pizza.Slug == slug).First();
                _database.Remove(deletedPizza);
                _database.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.WriteLog($"Catching exception at Pizza>Delete>{slug}");
                return NotFound("Can't find the desired pizza.");
            }
        }

        // POST: PizzaController/PopulateDb
        public ActionResult PopulateDb()
        {
            try
            {
                DataSeeder.PopulateDb();
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
            pizza.ImgPath ??= "";
            pizza.Slug = Helper.GetSlugFromString(pizza.Name);
        }
        private void PrepareFormModel(PizzaFormModel formData)
        {
            List<Category> categories = _database.Categories.ToList();
            formData.Categories = categories;
        }
    }
}
