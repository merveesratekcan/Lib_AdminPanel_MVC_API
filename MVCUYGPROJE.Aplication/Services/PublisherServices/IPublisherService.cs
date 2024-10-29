using MVCUYGPROJE.Aplication.DTOs.CategoryDTOs;
using MVCUYGPROJE.Aplication.DTOs.PublisherDTOs;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.PublisherServices
{
    public interface IPublisherService
    {
        Task<IDataResult<PublisherDTO>> CreateAsync(PublisherCreateDTO publisherCreateDTO);
        Task<IDataResult<List<PublisherListDTO>>> GetAllAsync();
        Task<IDataResult<List<PublisherListDTO>>> GetByIdAsync();
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id);
        Task<IDataResult<PublisherDTO>> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO);
        
    }
}
