using MVCUYGPROJE.Aplication.DTOs.CategoryDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.CategoryServices;

public interface ICategoryService
{
    Task<IDataResult<CategoryDTO>> CreateAsync(CategoryCreateDTO categoryCreateDTO);  
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);
    Task<IDataResult<List<CategoryListDTO>>> GetAllAsync();
}
