using Mapster;
using MVCUYGPROJE.Aplication.DTOs.BookDTOs;
using MVCUYGPROJE.Aplication.DTOs.PublisherDTOs;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.PublicherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.PublisherServices
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IDataResult<PublisherDTO>> CreateAsync(PublisherCreateDTO publisherCreateDTO)
        {
            if(await _publisherRepository.AnyAsync(x => x.Name.ToLower() == publisherCreateDTO.Name.ToLower()))
            {
                return new ErrorDataResult<PublisherDTO>("Bu isimde bir yayınevi zaten mevcut");
            }
            var newPublisher = publisherCreateDTO.Adapt<Publisher>();
            await _publisherRepository.AddAsync(newPublisher);
            await _publisherRepository.SaveChangesAsync();
            var publisherDTO = newPublisher.Adapt<PublisherDTO>();
            return new SuccessDataResult<PublisherDTO>(publisherDTO, "Yayınevi ekleme işlemi başarılı!");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingPublisher = await _publisherRepository.GetByIdAsync(id);
            if (deletingPublisher == null)
            {
                return new ErrorResult("Silmek istediğiniz yayınevi bulunamadı!");
            }
            await _publisherRepository.DeleteAsync(deletingPublisher);
            await _publisherRepository.SaveChangesAsync();
            return new SuccessResult("Yayınevi silme işlemi başarılı!");
        }

        public async Task<IDataResult<List<PublisherListDTO>>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();
            if (publishers.Count() <= 0)
            {
                return new ErrorDataResult<List<PublisherListDTO>>("Listelenecek yayınevi bulunamadı!");
            }
            var publisherListDtos = publishers.Adapt<List<PublisherListDTO>>();
            return new SuccessDataResult<List<PublisherListDTO>>(publisherListDtos, "Yayınevleri listeleme başarılı!");
        }

        public async Task<IDataResult<List<PublisherListDTO>>> GetByIdAsync()
        {
           var publishers = await _publisherRepository.GetAllAsync();
            if (publishers.Count() <= 0)
            {
                return new ErrorDataResult<List<PublisherListDTO>>("Listelenecek yayınevi bulunamadı!");
            }
            var publisherListDtos = publishers.Adapt<List<PublisherListDTO>>();
            return new SuccessDataResult<List<PublisherListDTO>>(publisherListDtos, "Yayınevleri listeleme başarılı!");
        }

        public async Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null)
            {
                return new ErrorDataResult<PublisherDTO>("Listelenecek yayınevi bulunamadı!");
            }
            var publisherDTO = publisher.Adapt<PublisherDTO>();
            return new SuccessDataResult<PublisherDTO>(publisherDTO, "Yayınevi listeleme başarılı!");
        }

        public async Task<IDataResult<PublisherDTO>> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO)
        {
            var updatingPublisher = await _publisherRepository.GetByIdAsync(publisherUpdateDTO.Id);
            if (updatingPublisher == null)
            {
                return new ErrorDataResult<PublisherDTO>("Güncellenecek yayınevi bulunamadı!");
            }
            var updatedPublisher = publisherUpdateDTO.Adapt(updatingPublisher);
            await _publisherRepository.UpdateAsync(updatedPublisher);
            await _publisherRepository.SaveChangesAsync();
            return new SuccessDataResult<PublisherDTO>(updatedPublisher.Adapt<PublisherDTO>(),"Yayınevi güncelleme işlemi başarılı!") ;
        }
       
    }
}
