using MVCUYGPROJE.Aplication.DTOs.AuthorDTOs;
using MVCUYGPROJE.Aplication.DTOs.CategoryDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.AuthorServices;

public interface IAuthorService
{
    Task<IDataResult<AuthorDTO>> CreateAsync(AuthorCreateDTO authorCreateDTO);
    Task<IDataResult<List<AuthorListDTO>>> GetAllAsync();
    Task<IDataResult<List<AuthorListDTO>>> GetByIdAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AuthorDTO>> UpdateAsync(AuthorUpdateDTO authorUpdateDTO);
}
