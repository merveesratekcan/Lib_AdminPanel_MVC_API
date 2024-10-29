using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.ProfileUserDTOs;
using MVCUYGPROJE.Aplication.Services.ProfileUserServices;

namespace MVCUYGPROJE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IProfileUserService _profileUserService;

        public UserProfileController(IProfileUserService profileUserService)
        {
            _profileUserService = profileUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _profileUserService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProfileUserCreateDTO profileUserCreateDTO)
        {
            var result = await _profileUserService.CreateAsync(profileUserCreateDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _profileUserService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _profileUserService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
