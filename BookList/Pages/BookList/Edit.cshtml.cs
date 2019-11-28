using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookList.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public Book Book { get; set; }
        [TempData]
        public string Message { get; set; }



        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookInDb = _db.Books.Find(Book.Id);
                bookInDb.ISBN = Book.ISBN;
                bookInDb.Title = Book.Title;
                bookInDb.Author = Book.Author;
                bookInDb.Avaibility = Book.Avaibility;
                bookInDb.Price = Book.Price;



                await _db.SaveChangesAsync();
                Message = "Book Updated Successfully...";
                return RedirectToPage("index");
            }
            return RedirectToPage();
        }
    }
}