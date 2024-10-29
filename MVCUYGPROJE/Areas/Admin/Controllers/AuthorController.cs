
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using MVCUYGPROJE.Aplication.DTOs.AuthorDTOs;
using MVCUYGPROJE.Aplication.Services.AuthorServices;
using MVCUYGPROJE.Areas.Admin.Models.AuthorVMs;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    public class AuthorController : AdminBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var result=await _authorService.GetAllAsync();
            if(!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var authorListVM= result.Data.Adapt<List<AdminAuthorListVM>>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(authorListVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminAuthorCreateVM model)
        {
            var authorDTO = model.Adapt<AuthorCreateDTO>();
            var result = await _authorService.CreateAsync(authorDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _authorService.DeleteAsync(Id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var authorVM = result.Data.Adapt<AdminAuthorUpdateVM>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(authorVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminAuthorUpdateVM model)
        {
            var authorDTO = model.Adapt<AuthorUpdateDTO>();
            var result = await _authorService.UpdateAsync(authorDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var authorDetailVM = result.Data.Adapt<AdminAuthorDetailVM>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(authorDetailVM);
        }

    }
}
