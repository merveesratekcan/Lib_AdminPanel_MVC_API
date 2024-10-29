
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MVCUYGPROJE.Aplication.DTOs.CategoryDTOs;
using MVCUYGPROJE.Aplication.Services.CategoryServices;
using MVCUYGPROJE.Areas.Admin.Models.CategoryVMs;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;

        public CategoryController(ICategoryService categoryService, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _categoryService = categoryService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            if(!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var categoryListVMs=result.Data.Adapt<List<AdminCategoryListVM>>();
            NotifySuccess(_stringLocalizer["Success"]);
            return View(categoryListVMs);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminCategoryCreateVM model)
        {
            var categoryDTO = model.Adapt<CategoryCreateDTO>();
            var result = await _categoryService.CreateAsync(categoryDTO);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View(model);
            }
           
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var categoryUpdateVM = result.Data.Adapt<AdminCategoryUpdateVM>();
            NotifySuccess(result.Messages);
            return View(categoryUpdateVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(AdminCategoryUpdateVM model)
        {
            var categoryDTO = model.Adapt<CategoryUpdateDTO>();
            var result = await _categoryService.UpdateAsync(categoryDTO);
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
            var result = await _categoryService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var categoryDetailVM = result.Data.Adapt<AdminCategoryDetailVM>();
            NotifySuccess(result.Messages);
            return View(categoryDetailVM);
        }


    }
}
