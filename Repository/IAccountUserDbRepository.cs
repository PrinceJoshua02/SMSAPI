using Microsoft.AspNetCore.Identity;
using SMSAPI.DTO;
using SMSAPI.Model;

namespace SMSAPI.Repository
{
    public interface IAccountUserDbRepository
    {

        Task<IdentityResult> SignUpUserAsync(ApplicationUser username, string password);
        Task<SignInResult> SignInUserAsync(LogInDTO loginDTO);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
    }
}
