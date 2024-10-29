using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE.Aplication.Services.BookServices;

namespace MVCUYGPROJE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetByIdAsync();
            if (!result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
        {
            var result = await _bookService.CreateAsync(bookCreateDTO);

            if (!result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(BookUpdateDTO bookUpdateDTO)
        {
            var result = await _bookService.UpdateAsync(bookUpdateDTO);

            if (!result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid bookId)
        {
            var result = await _bookService.DeleteAsync(bookId);

            if (!result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
