using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;


namespace MVCUYGPROJE.Aplication.Services.BookServices;

public interface IBookService
{
    Task<IDataResult<List<BookListDTO>>> GetByIdAsync();
    Task<IDataResult<List<BookListDTO>>> GetAllAsync();
    Task<IDataResult<BookDTO>> CreateAsync(BookCreateDTO bookCreateDTO); 
    Task<IDataResult<List<BookListDTO>>> GetAllProfileUserAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<BookDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<BookDTO>> UpdateAsync(BookUpdateDTO bookUpdateDTO);
    Task UpdateAvailability(Guid id, bool isAvailable);

}
