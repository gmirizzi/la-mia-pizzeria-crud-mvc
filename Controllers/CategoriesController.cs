﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
					return NotFound($"La pizza con id {id} non è stato trovato");
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
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
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
	}
}
