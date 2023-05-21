using Microsoft.AspNetCore.Identity;
using SMSAPI.Data;
using SMSAPI.DTO;
using SMSAPI.Model;

namespace SMSAPI.Repository
{
    public class AccountUserDbRepository : IAccountUserDbRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SMSDBContext _context;

        public AccountUserDbRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            SMSDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> SignInUserAsync(LogInDTO loginDTO)
        {
            return await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> SignUpUserAsync(ApplicationUser username, string password)
        {
            return await _userManager.CreateAsync(username, password);
        }
    }
}

