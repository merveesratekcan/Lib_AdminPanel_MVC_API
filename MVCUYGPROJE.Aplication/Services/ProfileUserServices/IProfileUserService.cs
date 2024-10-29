using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE.Aplication.DTOs.ProfileUserDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;


namespace MVCUYGPROJE.Aplication.Services.ProfileUserServices;

public interface IProfileUserService
{
    Task<IDataResult<List<ProfileUserListDTO>>> GetAllAsync();
    Task<IDataResult<ProfileUserDTO>> CreateAsync(ProfileUserCreateDTO profileUserCreateDTO);

    
    Task<IDataResult<List<ProfileUserDTO>>> GetByIdAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<ProfileUserDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<ProfileUserDTO>> UpdateAsync(ProfileUserUpdateDTO profileUserUpdateDTO);
}
