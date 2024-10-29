
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.ProfileUserDTOs;
using MVCUYGPROJE.Aplication.Services.ProfileUserServices;
using MVCUYGPROJE.Areas.Admin.Models.ProfileUserVMs;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    public class ProfileUserController : AdminBaseController
    {
        private readonly IProfileUserService _profileUserService;

        public ProfileUserController(IProfileUserService profileUserService)
        {
            _profileUserService = profileUserService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _profileUserService.GetAllAsync();
           
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var profileUserVms = result.Data.Adapt<List<AdminProfileUserListVM>>();
            NotifySuccess(result.Messages);
            return View(profileUserVms);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminProfileUserCreateVM model)
        {
         
            var result = await _profileUserService.CreateAsync(model.Adapt<ProfileUserCreateDTO>());
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
            var result = await _profileUserService.DeleteAsync(id);
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
            var result = await _profileUserService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View();
            }
            var profileUserUpdateVM = result.Data.Adapt<AdminProfileUserUpdateVM>();
            NotifySuccess(result.Messages);
            return View(profileUserUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminProfileUserUpdateVM model)
        {
            var result = await _profileUserService.UpdateAsync(model.Adapt<ProfileUserUpdateDTO>());
            if (!result.IsSuccess)
            {
                NotifyError(result.Messages);
                return View(model);
            }
            NotifySuccess(result.Messages);
            return RedirectToAction("Index");
        }
    }
}
