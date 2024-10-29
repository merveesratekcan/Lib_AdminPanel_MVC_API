using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.Services.BookServices;
using MVCUYGPROJE.Areas.ProfileUser.Models.BookListVMs;

namespace MVCUYGPROJE.Areas.ProfileUser.Controllers
{
    public class BookController : ProfileUserBaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllProfileUserAsync();
            var bookListVMs = result.Data.Adapt<List<BookListVM>>();
            return View(bookListVMs);
        }
    }
}
