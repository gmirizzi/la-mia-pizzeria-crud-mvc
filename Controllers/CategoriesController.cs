using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
	public class CategoriesController : Controller
	{
		// GET: CategoriesController
		public IActionResult Index()
		{
			using (PizzeriaContext db = new PizzeriaContext())
			{
				List<Category> categoriresList = db.Categories.ToList<Category>();
				return View(categoriresList);
			}
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", category);
			}
			else
			{
				PizzeriaContext db = new PizzeriaContext();
				db.Categories.Add(category);
				db.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
		}

		// GET: CategoriesController/Edit/5
		public ActionResult Edit(int id)
		{
			using (PizzeriaContext db = new PizzeriaContext())
			{
				Category current = db.Categories.Find(id);

				if (current == null)
				{
					return NotFound($"La categoria con id {id} non è stato trovata");
				}
				else
				{
					return View(current);
				}
			}
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Category category)
		{
			category.CategoryId = id;

			if (!ModelState.IsValid)
			{
				return View("Edit", category);
			}
			else
			{
				using (PizzeriaContext db = new PizzeriaContext())
				{
					Category current = db.Categories.Find(id);

					if (current == null)
					{
						return NotFound($"La categoria con id {id} non è stato trovata");
					}
					else
					{
						current.Name = category.Name;
						db.SaveChanges();
						return RedirectToAction(nameof(Index));
					}
				}

			}
		}

		// GET: CategoriesController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			using (PizzeriaContext db = new PizzeriaContext())
			{
				Category current = db.Categories.Where(c => c.CategoryId == id).Include(c => c.PizzasList).FirstOrDefault();

				if (current == null)
				{
					return NotFound($"La categoria con id {id} non è stato trovata");
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
