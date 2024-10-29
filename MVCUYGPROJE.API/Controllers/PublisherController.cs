using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.PublisherDTOs;
using MVCUYGPROJE.Aplication.Services.PublisherServices;

namespace MVCUYGPROJE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _publisherService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PublisherCreateDTO publisherCreateDTO)
        {
            var result = await _publisherService.CreateAsync(publisherCreateDTO);
            return result.IsSuccess ? Ok(result) : Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(PublisherUpdateDTO publisherUpdateDTO)
        {
            var result = await _publisherService.UpdateAsync(publisherUpdateDTO);
            return result.IsSuccess ? Ok(result) : Ok();
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : Ok();
        }
    }
}
