using Readify.Models;
using Readify.Models.Authentication;
using Readify.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Readify.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBook _bookRepository;
        private readonly IRental _rentalRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IBook bookRepository, IRental rentalRepository, UserManager<ApplicationUser> userManager)
        {
            _bookRepository = bookRepository;
            _rentalRepository = rentalRepository;
            _userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.ReadAsync();
            return View(books);
        }
        #endregion

        #region Rent
        public async Task<IActionResult> Rent(int id)
        {
            var book = await _bookRepository.FindAsync(id);
            if (book == null)
                return NotFound();

            var rental = new Rental
            {
                intBookId = book.intBookId,
                Book = book
            };

            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent(Rental rental)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            rental.intUserId = user.Id;
            rental.strUserName = user.UserName;

            var book = await _bookRepository.FindAsync(rental.intBookId);
            if (book == null)
                return NotFound();

            rental.Book = book;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Unable to process the transaction. Please try again.";
                return View(rental);
            }

            rental.strPaymentMethod = rental.strPaymentMethod;
            rental.ysnPaid = true;
            await _rentalRepository.CreateAsync(rental);
            TempData["PaymentSuccess"] = $"Payment of {rental.dclTotalPrice.ToString("C", CultureInfo.GetCultureInfo("en-US"))} was successful!";
            return RedirectToAction(nameof(Rental));
        }
        #endregion

        #region Rental
        [HttpGet]
        public async Task<IActionResult> Rental()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var rentals = await _rentalRepository.ReadAsync();

            List<Rental> rentalsToShow;

            if (await _userManager.IsInRoleAsync(user, "Admin")) {
                rentalsToShow = rentals.ToList();
            } else {
                rentalsToShow = rentals
                    .Where(r => r.intUserId == user.Id)
                    .ToList();
            }

            return View(rentalsToShow);
        }
        #endregion

        #region Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var rental = await _rentalRepository.FindAsync(id);
            if (rental == null)
                return NotFound();

            if (!rental.ysnReturned)
            {
                rental.ysnReturned = true;
                rental.dtmReturnDate = DateTime.Now;
                await _rentalRepository.UpdateAsync(rental);
                TempData["Success"] = $"Book returned successfully!";
            }

            return RedirectToAction(nameof(Rental));
        }
        #endregion

        #region About
        public IActionResult About()
        {
            return View();
        }
        #endregion
    }
}
