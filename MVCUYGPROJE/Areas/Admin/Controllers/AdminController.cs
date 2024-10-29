
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.AdminDTOs;
using MVCUYGPROJE.Aplication.Services.AdminServices;
using MVCUYGPROJE.Areas.Admin.Models.AdminVMs;

namespace MVCUYGPROJE.Areas.Admin.Controllers;

public class AdminController : AdminBaseController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await  _adminService.GetAllAsync();
        if (!result.IsSuccess)
        {
            NotifyError(result.Messages);
            return View();
        }
        var adminListVMs = result.Data.Adapt<List<AdminListVM>>();
        NotifySuccess(result.Messages);
        return View(adminListVMs);

    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AdminCreateVM model)
    {
        var result = await _adminService.CreateAsync(model.Adapt<AdminCreateDTO>());
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
        var result = await _adminService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            NotifyError(result.Messages);
            return View();
        }
        NotifySuccess(result.Messages);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(Guid id)
    {
        var result = await _adminService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyError(result.Messages);
            return View();
        }
        var adminUpdateVM = result.Data.Adapt<AdminUpdateVM>();
        NotifySuccess(result.Messages);
        return View(adminUpdateVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AdminUpdateVM model)
    {
        var result = await _adminService.UpdateAsync(model.Adapt<AdminUpdateDTO>());
        if (!result.IsSuccess)
        {
            NotifyError(result.Messages);
            return View(model);
        }
        NotifySuccess(result.Messages);
        return RedirectToAction("Index");
    }
}
