using Readify.Models;
using Readify.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Readify.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _book;
        private readonly ICategory _category;

        public BookController(IBook book, ICategory category)
        {
            _book = book;
            _category = category;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _book.ReadAsync();
            return View(books);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = new SelectList(
                await _category.ReadAsync(),
                "intCategoryId",
                "strSubject"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    book.strImageUrl = await SaveImage(imageFile);
                }
                await _book.CreateAsync(book);
                TempData["Success"] = "Book added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = new SelectList(
                await _category.ReadAsync(),
                "intCategoryId",
                "strSubject",
                book.intCategoryId
            );

            return View(book);
        }
        #endregion

        #region Detail
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _book.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _book.FindAsync(id);
            if (book == null)
                return NotFound();

            ViewBag.CategoryList = new SelectList(
                await _category.ReadAsync(),
                "intCategoryId",
                "strSubject",
                book.intCategoryId
            );

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingBook = await _book.FindAsync(id);
                if (existingBook == null)
                    return NotFound();

                existingBook.strTitle = book.strTitle;
                existingBook.strAuthor = book.strAuthor;
                existingBook.dclPrice = book.dclPrice;
                existingBook.intCategoryId = book.intCategoryId;

                if (imageFile != null && imageFile.Length > 0)
                    existingBook.strImageUrl = await SaveImage(imageFile);

                await _book.UpdateAsync(existingBook);
                TempData["Success"] = "Book updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = new SelectList(
                await _category.ReadAsync(),
                "intCategoryId",
                "strSubject",
                book.intCategoryId
            );

            return View(book);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _book.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _book.FindAsync(id);
            if (book == null)
                return NotFound();

            try
            {
                await _book.DeleteAsync(id);
                TempData["Success"] = "Book deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["Warning"] = "Cannot delete this book. It is currently on rentals!";
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the book. Please try again.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
        #endregion

        #region SaveImage
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/books"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/books/" + uniqueFileName;
        }
        #endregion
    }
}
