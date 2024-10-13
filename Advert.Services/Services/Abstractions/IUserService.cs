using Advert.Entity.DTOs.UserDTOs;
using Advert.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.Services.Abstractions
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
        Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId);
        Task<List<AppRole>> GetAllRolesAsync();
        Task<List<UserDto>> GetAllUsersWithRoleAsync();
        Task<AppUser> GetAppUserByIdAsync(Guid userId);
        Task<string> GetUserRoleAsync(AppUser user);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<UserProfiloDto> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfiloDto userProfileDto);
    }
}
