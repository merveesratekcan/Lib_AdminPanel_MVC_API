using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE.Aplication.DTOs.ProfileUserDTOs;
using MVCUYGPROJE.Aplication.Services.AccountServices;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Enums;
using MVCUYGPROJE_Domain.Utilities.Concretes;
using MVCUYGPROJE_Domain.Utilities.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.ProfileUserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.ProfileUserServices;

public class ProfileUserService : IProfileUserService
{
    private readonly IAccountService _accountService;
    private readonly IProfileUserRepository _profileUserRepository;

    public ProfileUserService(IAccountService accountService, IProfileUserRepository profileUserRepository)
    {
        _accountService = accountService;
        _profileUserRepository = profileUserRepository;
    }

    public async Task<IDataResult<ProfileUserDTO>> CreateAsync(ProfileUserCreateDTO profileUserCreateDTO)
    {
        if(await _accountService.AnyAsync(x => x.Email == profileUserCreateDTO.Email))
        {
            return new ErrorDataResult<ProfileUserDTO>("Bu email adresi kullanılmaktadır.");
        }
        IdentityUser identityUser = new()
        { 
            Email = profileUserCreateDTO.Email,
            NormalizedEmail = profileUserCreateDTO.Email.ToUpperInvariant(),
            UserName = profileUserCreateDTO.Email,
            NormalizedUserName = profileUserCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true,
        };
        DataResult<ProfileUserDTO> result=new ErrorDataResult<ProfileUserDTO>();

        var strategy = await _profileUserRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _profileUserRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.ProfileUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<ProfileUserDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var profileUser=profileUserCreateDTO.Adapt<ProfileUser>();
                profileUser.IdentityId = identityUser.Id;
                await _profileUserRepository.AddAsync(profileUser);
                await _profileUserRepository.SaveChangesAsync();
                result = new SuccessDataResult<ProfileUserDTO>("Kullanıcı ekleme başarılı");
                transactionScope.Commit();

            }
            catch (Exception ex)
            {
               result = new ErrorDataResult<ProfileUserDTO>("Ekleme Başarısız"+ex.Message);
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
        var deletingProfileUser = await _profileUserRepository.GetByIdAsync(id);
        if (deletingProfileUser == null)
        {
            return new ErrorResult("Silmek istediğiniz kullanıcı bulunamadı!");
        }
        await _profileUserRepository.DeleteAsync(deletingProfileUser);
        await _profileUserRepository.SaveChangesAsync();
        return new SuccessResult("Kullanıcı silme işlemi başarılı!");
    }

    public async Task<IDataResult<List<ProfileUserListDTO>>> GetAllAsync()
    {
       
        var profileUsers = await _profileUserRepository.GetAllAsync();
        var profileUserDTOs = profileUsers.Adapt<List<ProfileUserListDTO>>();

        if (profileUsers.Count() <= 0)
        {
            return new ErrorDataResult<List<ProfileUserListDTO>>(profileUserDTOs,"Kullanıcı bulunamadı");
        }
        return new SuccessDataResult<List<ProfileUserListDTO>>(profileUserDTOs, "Kullanıcılar listelendi");


    }

    public async Task<IDataResult<List<ProfileUserDTO>>> GetByIdAsync()
    {
        var profileUsers = await _profileUserRepository.GetAllAsync();
        if (profileUsers.Count() <= 0)
        {
            return new ErrorDataResult<List<ProfileUserDTO>>("Listelenecek kullanıcı bulunamadı!");
        }
        var profileUserListDtos = profileUsers.Adapt<List<ProfileUserDTO>>();
        return new SuccessDataResult<List<ProfileUserDTO>>(profileUserListDtos, "Kullanıcılar listeleme başarılı!");
    }

    public async Task<IDataResult<ProfileUserDTO>> GetByIdAsync(Guid id)
    {
        var profileUser = await _profileUserRepository.GetByIdAsync(id);
        if (profileUser == null)
        {
            return new ErrorDataResult<ProfileUserDTO>("Görüntülenecek kullanıcı bulunamadı.");
        }
        var profileUserDto = profileUser.Adapt<ProfileUserDTO>();
        return new SuccessDataResult<ProfileUserDTO>(profileUserDto, "Kullanıcı görüntüleme başarılı!");
    }

    public async Task<IDataResult<ProfileUserDTO>> UpdateAsync(ProfileUserUpdateDTO profileUserUpdateDTO)
    {
        DataResult<ProfileUserDTO> result = new ErrorDataResult<ProfileUserDTO>();
        // Transaction ve strateji başlat
        var strategy = await _profileUserRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _profileUserRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                // Güncellenecek kullanıcıyı ID ile getir
                var updatingUser = await _profileUserRepository.GetByIdAsync(profileUserUpdateDTO.Id, false);
                if (updatingUser == null)
                {
                    result = new ErrorDataResult<ProfileUserDTO>("Güncellenecek Kullanıcı Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }

                // Kullanıcıyı DTO'dan güncelle ve değişiklikleri kaydet
                updatingUser = profileUserUpdateDTO.Adapt(updatingUser);
                await _profileUserRepository.UpdateAsync(updatingUser);
                await _profileUserRepository.SaveChangesAsync();
                result = new SuccessDataResult<ProfileUserDTO>(updatingUser.Adapt<ProfileUserDTO>(), "Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<ProfileUserDTO>("Güncelleme Başarısız: " + ex.Message);
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
