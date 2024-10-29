using Mapster;
using MVCUYGPROJE.Aplication.DTOs.CategoryDTOs;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IDataResult<CategoryDTO>> CreateAsync(CategoryCreateDTO categoryCreateDTO)
    {
        if (await _categoryRepository.AnyAsync(x => x.Name.ToLower() == categoryCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<CategoryDTO>("Mevcut kategori sistemde kayıtlı!");
        }
        var newCategory = categoryCreateDTO.Adapt<Category>();
        await _categoryRepository.AddAsync(newCategory);
        await _categoryRepository.SaveChangesAsync();
        var categoryDTO = newCategory.Adapt<CategoryDTO>();
        return new SuccessDataResult<CategoryDTO>(categoryDTO,"Kategori ekleme başarılı!");


    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
       var deletinCategory= await _categoryRepository.GetByIdAsync(id);
        if (deletinCategory == null)
        {
            return new ErrorResult("Silmek istediğiniz kategori bulunamadı!");
        }
        await _categoryRepository.DeleteAsync(deletinCategory);
        await _categoryRepository.SaveChangesAsync();
        return new SuccessResult("Kategori silme işlemi başarılı!");
    }

    public async Task<IDataResult<List<CategoryListDTO>>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        if (categories.Count() <= 0)
        {
            return new ErrorDataResult<List<CategoryListDTO>>("Listelenecek Kategori bulunamadı!");
        }
        var categoryListDtos = categories.Adapt<List<CategoryListDTO>>();
        return new SuccessDataResult<List<CategoryListDTO>>(categoryListDtos, "Kategoriler listeleme başarılı!");
    }

    public async Task<IDataResult<List<CategoryListDTO>>> GetByIdAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        if(categories.Count() <= 0)
        {
            return new ErrorDataResult<List<CategoryListDTO>>("Listelenecek Kategori bulunamadı!");
        }
        var categoryListDtos = categories.Adapt<List<CategoryListDTO>>();
        return new SuccessDataResult<List<CategoryListDTO>>(categoryListDtos, "Kategoriler listeleme başarılı!");
    }

    public async Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorDataResult<CategoryDTO>("Gösterilecek Kategori bulunamadı!");
        }
        var categoryDto = category.Adapt<CategoryDTO>();
        return new SuccessDataResult<CategoryDTO>(categoryDto, "Kategori gösterme başarılı!");
    }

    public async Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
    {

        //Güncelleme yapılacak kategoriyi GetByIdAsync ile bulur.
        var updatingCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDTO.Id);
        //Kategori null değilse, DTO'dan gelen bilgilerle kategori nesnesinin alanlarını günceller.
        if (updatingCategory is null)
        {
            return new ErrorDataResult<CategoryDTO>("Güncellenecek kategori bulunamadı!");
        }
        var updatedCategory = categoryUpdateDTO.Adapt(updatingCategory);
        await _categoryRepository.UpdateAsync(updatedCategory);
        await _categoryRepository.SaveChangesAsync();
        return new SuccessDataResult<CategoryDTO>(updatedCategory.Adapt<CategoryDTO>(), "Kategori güncelleme başarılı!");



    }
}
