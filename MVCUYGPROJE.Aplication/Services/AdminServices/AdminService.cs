using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE.Aplication.DTOs.AdminDTOs;
using MVCUYGPROJE.Aplication.Services.AccountServices;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Enums;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.AdminRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.AdminServices;

public class AdminService : IAdminService
{
    private readonly IAccountService _accountService;
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAccountService accountService, IAdminRepository adminRepository)
    {
        _accountService = accountService;
        _adminRepository = adminRepository;
    }

    public async Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO)
    {
        if(await _accountService.AnyAsync(x => x.Email == adminCreateDTO.Email))
        {
            return new ErrorDataResult<AdminDTO>("Bu email adresi kullanılmaktadır.");
        }
        IdentityUser identityUser = new ()
        {
            Email = adminCreateDTO.Email,
            NormalizedEmail = adminCreateDTO.Email.ToUpperInvariant(),
            UserName = adminCreateDTO.Email,
            NormalizedUserName = adminCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true,
        };
        DataResult<AdminDTO> result= new ErrorDataResult<AdminDTO>("Admin oluşturulamadı.");

        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.Admin);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var admin = adminCreateDTO.Adapt<Admin>();
                admin.IdentityId = identityUser.Id;
                await _adminRepository.AddAsync(admin);
                await _adminRepository.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>("Kullanıcı ekleme başarılı");
                transactionScope.Commit();

            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Ekleme Başarısız" + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingAdmin = await _adminRepository.GetByIdAsync(id);
        if (deletingAdmin == null)
        {
            return new ErrorResult("Admin bulunamadı.");
        }

        await _adminRepository.DeleteAsync(deletingAdmin);
        await _adminRepository.SaveChangesAsync();
        return new SuccessResult("Admin başarıyla silindi.");
    }

    public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync()
    {
        var admins=await _adminRepository.GetAllAsync();
        var adminListDTO = admins.Adapt<List<AdminListDTO>>();
        if (admins.Count() <= 0)
        {
            return new ErrorDataResult<List<AdminListDTO>>(adminListDTO,"Admin bulunamadı.");
        }
        return new SuccessDataResult<List<AdminListDTO>>(adminListDTO,"Adminler listelendi.");
    }

    public async Task<IDataResult<List<AdminDTO>>> GetByIdAsync()
    {
        var admin =await _adminRepository.GetAllAsync();
        if (admin.Count() <= 0)
        {
            return new ErrorDataResult<List<AdminDTO>>("Admin bulunamadı.");
        }
        var adminListDtos = admin.Adapt<List<AdminDTO>>();
        return new SuccessDataResult<List<AdminDTO>>(adminListDtos, "Adminler listelendi.");
    }

    public async Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id)
    {
        var admin = await _adminRepository.GetByIdAsync(id);
        if (admin == null)
        {
            return new ErrorDataResult<AdminDTO>("Admin bulunamadı.");
        }

        var adminDto = admin.Adapt<AdminDTO>();
        return new SuccessDataResult<AdminDTO>(adminDto, "Admin başarıyla bulundu.");
    }

    public async Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
    {
        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                // Güncellenecek Admin kullanıcıyı ID ile getir
                var updatingUser = await _adminRepository.GetByIdAsync(adminUpdateDTO.Id, false);
                if (updatingUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Güncellenecek Kullanıcı Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }

                // IdentityUser kaydını getir
                var identityUser = await _accountService.FindByIdAsync(updatingUser.IdentityId);
                if (identityUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Güncellenecek Kullanıcıya ait Identity kaydı bulunamadı");
                    transactionScope.Rollback();
                    return;
                }

                // Admin bilgilerini DTO'dan güncelle
                updatingUser = adminUpdateDTO.Adapt(updatingUser);
                await _adminRepository.UpdateAsync(updatingUser);

                // IdentityUser bilgilerini güncelle
                identityUser.Email = adminUpdateDTO.Email;
                identityUser.UserName = adminUpdateDTO.Email;
                identityUser.NormalizedEmail = adminUpdateDTO.Email.ToUpperInvariant();
                identityUser.NormalizedUserName = adminUpdateDTO.Email.ToUpperInvariant();
                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>("Identity bilgilerini güncelleme başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    transactionScope.Rollback();
                    return;
                }

                // Değişiklikleri kaydet
                await _adminRepository.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>(updatingUser.Adapt<AdminDTO>(), "Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Güncelleme Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }
}
