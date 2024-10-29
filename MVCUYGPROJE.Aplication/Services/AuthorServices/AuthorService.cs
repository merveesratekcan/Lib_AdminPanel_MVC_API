using Mapster;
using MVCUYGPROJE.Aplication.DTOs.AuthorDTOs;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.AuthorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.AuthorServices;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IDataResult<AuthorDTO>> CreateAsync(AuthorCreateDTO authorCreateDTO)
    {
        if(await _authorRepository.AnyAsync(x => x.Name.ToLower() == authorCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<AuthorDTO>("Mevcut yazar sistemde kayıtlı!");
        }
        var newAuthor = authorCreateDTO.Adapt<Author>();
        await _authorRepository.AddAsync(newAuthor);
        await _authorRepository.SaveChangesAsync();
        var authorDTO = newAuthor.Adapt<AuthorDTO>();
        return new SuccessDataResult<AuthorDTO>(authorDTO, "Yazar Ekleme Başarılı!");

    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
       var deletingAuthor=await _authorRepository.GetByIdAsync(id);
        if (deletingAuthor == null)
        {
            return new ErrorResult("Silmek istediğiniz yazar bulunamadı!");
        }
        await _authorRepository.DeleteAsync(deletingAuthor);
        await _authorRepository.SaveChangesAsync();
        return new SuccessResult("Yazar silme işlemi başarılı!");
    }

    public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        if (authors.Count() <= 0)
        {
            return new ErrorDataResult<List<AuthorListDTO>>("Listelenecek yazar bulunamadı!");
        }
        var authorListDtos = authors.Adapt<List<AuthorListDTO>>();
        return new SuccessDataResult<List<AuthorListDTO>>(authorListDtos, "Yazarlar listeleme başarılı!");
    }

    public async Task<IDataResult<List<AuthorListDTO>>> GetByIdAsync()
    {
        var authors =await _authorRepository.GetAllAsync();
        if(authors.Count() <= 0)
        {
            return new ErrorDataResult<List<AuthorListDTO>>("Listelenecek yazar bulunamadı!");
        }
        var authorListDtos = authors.Adapt<List<AuthorListDTO>>();
        return new SuccessDataResult<List<AuthorListDTO>>(authorListDtos, "Yazarlar listeleme başarılı!");
    }

    public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
        {
            return new ErrorDataResult<AuthorDTO>("Listelenecek yazar bulunamadı!");
        }
        var authorDto = author.Adapt<AuthorDTO>();
        return new SuccessDataResult<AuthorDTO>(authorDto, "Yazar listeleme başarılı!");
    }

    public async Task<IDataResult<AuthorDTO>> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
    {
        var updatingAuthor =await
            _authorRepository.GetByIdAsync(authorUpdateDTO.Id);
        if (updatingAuthor == null)
        {
            return new ErrorDataResult<AuthorDTO>("Güncellenecek yazar bulunamadı!");
        }
        var updatedAuthor = authorUpdateDTO.Adapt(updatingAuthor);
        await _authorRepository.UpdateAsync(updatedAuthor);
        await _authorRepository.SaveChangesAsync();
        return new SuccessDataResult<AuthorDTO>(updatedAuthor.Adapt<AuthorDTO>(), "Yazar güncelleme başarılı!");
    }
}
