using Readify.Data;
using Readify.Models;
using Readify.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Readify.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var categories = await _category.ReadAsync();
            return View(categories);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _category.CreateAsync(category);
                TempData["Success"] = "Category added successfully!";

                return RedirectToAction("Index");
            }

            return View(category);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var categoryToUpdate = await _category.FindAsync(id);
            if (categoryToUpdate == null) return NotFound();
            
            return View(categoryToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _category.UpdateAsync(category);
                TempData["Success"] = "Category updated successfully!";

                return RedirectToAction("Index");
            }

            return View(category);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            
            var categoryToDelete = await _category.FindAsync(id);

            if (categoryToDelete == null) return NotFound();

            return View(categoryToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _category.FindAsync(id);
            if (category == null)
                return NotFound();

            try
            {
                await _category.DeleteAsync(id);
                TempData["Success"] = "Category deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["Warning"] = "Cannot delete this category. There are books associated with it!";
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the category. Please try again.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
        #endregion
    }
}
