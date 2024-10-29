using MVCUYGPROJE.Aplication.DTOs.AdminDTOs;
using MVCUYGPROJE.Aplication.DTOs.ProfileUserDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.AdminServices;

public interface IAdminService
{
    Task<IDataResult<List<AdminListDTO>>> GetAllAsync();
    Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO);

    Task<IDataResult<List<AdminDTO>>> GetByIdAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO);
}
