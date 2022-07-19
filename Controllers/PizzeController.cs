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
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }
            else
            {
                PizzeriaContext db = new PizzeriaContext();
                db.Pizzas.Add(pizza);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                    return View(current);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pizza pizza)
        {
            pizza.PizzaId = id;
            if (!ModelState.IsValid)
            {
                return View("Edit", pizza);
            }
            else
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
                        current.Name = pizza.Name;
                        current.Description = pizza.Description;
                        current.ImgPath = pizza.ImgPath;
                        current.Price = pizza.Price;
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

