using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.PublisherDTOs;
using MVCUYGPROJE.Aplication.Services.PublisherServices;
using MVCUYGPROJE.Areas.Admin.Models.PublisherVMs;

namespace MVCUYGPROJE.Areas.Admin.Controllers
{
    public class PublisherController : AdminBaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async  Task<IActionResult> Index()
        {
            var result = await _publisherService.GetByIdAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var publisherListVM = result.Data.Adapt<List<AdminPublisherListVM>>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(publisherListVM);
            
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminPublisherCreateVM model)
        {
            var publisherDTO = model.Adapt<PublisherCreateDTO>();
            var result = await _publisherService.CreateAsync(publisherDTO);
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
            var result = await _publisherService.DeleteAsync(Id);
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
            var result = await _publisherService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var publisherUpdateVM = result.Data.Adapt<AdminPublisherUpdateVM>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(publisherUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminPublisherUpdateVM model)
        {
            var publisherDTO = model.Adapt<PublisherUpdateDTO>();
            var result = await _publisherService.UpdateAsync(publisherDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }
       
    }
}
