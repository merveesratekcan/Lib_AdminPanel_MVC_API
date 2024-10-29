using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.AdminDTOs;
using MVCUYGPROJE.Aplication.Services.AdminServices;

namespace MVCUYGPROJE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _adminService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdminCreateDTO adminCreateDTO)
        {
            var result = await _adminService.CreateAsync(adminCreateDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _adminService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _adminService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
        {
            var result = await _adminService.UpdateAsync(adminUpdateDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
