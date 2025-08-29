using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> GetRoles();
        IEnumerable<IdentityUser> GetUsers();
        Task<IdentityUser> GetOneUserAsync(string Name);
        Task<UserDtoForUpdate> GetOneUserForUpdateAsync(string Name);

        Task<IdentityResult> CreateUserAsync(UserDtoForInsertion userDtoForInsertion);
        Task UpdateUserAsync(UserDtoForUpdate dtoForUpdate);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task DeleteAsync(string UserName);

    }
    
}