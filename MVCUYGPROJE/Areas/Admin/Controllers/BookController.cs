using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using MVCUYGPROJE.Aplication.DTOs.AuthorDTOs;
using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE.Aplication.Services.AuthorServices;
using MVCUYGPROJE.Aplication.Services.BookServices;
using MVCUYGPROJE.Aplication.Services.CategoryServices;
using MVCUYGPROJE.Aplication.Services.PublisherServices;
using MVCUYGPROJE.Areas.Admin.Models;
using MVCUYGPROJE.Areas.Admin.Models.BookVMs;
using MVCUYGPROJE_Domain.Entities;
using System.Security.Policy;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    public class BookController : AdminBaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;

        public BookController(IAuthorService authorService, IPublisherService publisherService, ICategoryService categoryService, IBookService bookService)
        {
            _authorService = authorService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetByIdAsync();
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var bookListVMs = result.Data.Adapt<List<AdminBookListVM>>();
            NotifySuccess(result.Messages);
            return View(bookListVMs);
        }
        public async Task<IActionResult> Create()
        {
            var bookCreateVM = new AdminBookCreateVM();
            bookCreateVM.Authors = await GetAuthors();
            bookCreateVM.Publishers = await GetPublishers();
            bookCreateVM.Categories = await GetCategories();
            return View(bookCreateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            //var result = await _bookService.CreateAsync(model.Adapt<BookCreateDTO>());
            //if (!result.IsSuccess)
            //{
            //    NotifyError(result.Messages);
            //    return View(result);
            //}
            //NotifySuccess(result.Messages);
            //return RedirectToAction("Index");

            var bookDTO = model.Adapt<BookCreateDTO>();
            var result = await _bookService.CreateAsync(bookDTO);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View(model);
            }
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }

        private async Task<SelectList> GetCategories(Guid? categoryId = null)
        {
            var categories = (await _categoryService.GetAllAsync()).Data;
            return new SelectList(categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (categoryId != null ? categoryId.Value : categoryId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        private async Task<SelectList> GetPublishers(Guid? publisherId = null)
        {
            var publishers = (await _publisherService.GetAllAsync()).Data;
            return new SelectList(publishers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (publisherId != null ? publisherId.Value : publisherId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        private async Task<SelectList> GetAuthors(Guid? authorId=null)
        {
            var authors = (await _authorService.GetByIdAsync()).Data;
            return new SelectList(authors.Select(x=>new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected=x.Id==(authorId!= null ? authorId.Value : authorId)

            }).OrderBy(x=>x.Text), "Value","Text");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _bookService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var bookUpdateVM = result.Data.Adapt<AdminBookUpdateVM>();
            bookUpdateVM.Authors = await GetAuthors(bookUpdateVM.AuthorId);
            bookUpdateVM.Publishers = await GetPublishers(bookUpdateVM.PublisherId);
            bookUpdateVM.Categories = await GetCategories(bookUpdateVM.CategoryId);
            return View(bookUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminBookUpdateVM model)
        {
         var bookDTO = model.Adapt<BookUpdateDTO>();
            var result = await _bookService.UpdateAsync(bookDTO);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View(model);
            }
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        [HttpPost]


        public async Task<JsonResult> UpdateAvailability(Guid bookId, bool isbookAvailable)
        {
            try
            {
                await _bookService.UpdateAvailability(bookId, isbookAvailable);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}
