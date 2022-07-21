using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzeController : Controller
    {
        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzaList = db.Pizzas.ToList<Pizza>();
                return View(pizzaList);
            }
        }

        [Route("/menu")]
        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = db.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).FirstOrDefault();

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovato");
                }
                else
                {
                    return View(current);
                }
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }
            else
            {
                PizzeriaContext db = new PizzeriaContext();
                model.Pizza.IngredientsList = new List<Ingredient>();
                foreach (string ingredientId in model.SelectedIngredients)
                {
                    Ingredient ingredient = db.Ingredients.Find(int.Parse(ingredientId));
                    model.Pizza.IngredientsList.Add(ingredient);
                }
                db.Pizzas.Add(model.Pizza);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = db.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).FirstOrDefault();

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovato");
                }
                else
                {
                    PizzaViewModel newModel = new PizzaViewModel();
                    newModel.Pizza = current;
                    return View(newModel);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaViewModel model)
        {
            model.Pizza.PizzaId = id;
            
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            else
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    Pizza current = db.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).FirstOrDefault();

                    if (current == null)
                    {
                        return NotFound($"La pizza con id {id} non è stato trovato");
                    }
                    else
                    {
                        current.Name = model.Pizza.Name;
                        current.Description = model.Pizza.Description;
                        current.ImgPath = model.Pizza.ImgPath;
                        current.Price = model.Pizza.Price;
                        current.CategoryId = model.Pizza.CategoryId;
                        db.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = db.Pizzas.Find(id);

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovato");
                }
                else
                {
                    db.Remove(current);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}

