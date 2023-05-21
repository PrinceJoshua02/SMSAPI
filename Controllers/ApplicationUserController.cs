using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMSAPI.Data;
using SMSAPI.DTO;
using SMSAPI.Model;
using SMSAPI.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApplicationUserController : ControllerBase
    {
        public IMapper _Mapper { get;  }
        public IConfiguration _appConfig { get; }
       
        private readonly IAccountUserDbRepository _dbRepository;

        public ApplicationUserController(SMSDBContext Task, IConfiguration appConfig, IMapper mapper, IAccountUserDbRepository dbRepository)
        {
            _appConfig = appConfig;
            _Mapper = mapper;
            _dbRepository = dbRepository;
        }

        /* public async Task<IActionResult> Adduser(string First_Name, string Last_Name, string Address, string UserName, string Password)
         {
             var Id = Guid.NewGuid().ToString();
             var parameters = new[] {
                 new SqlParameter("@Id",Id),
                 new SqlParameter("@First_Name", First_Name),
                 new SqlParameter("@Last_Name", Last_Name),
                 new SqlParameter("@Address", Address),
                 new SqlParameter("@UserName", UserName),
                 new SqlParameter("@Password", Password)

                 };
             var result = await _context.Database.ExecuteSqlRawAsync("EXEC SP_AddApplicationUser @Id, @First_Name, @Last_Name,@Address,@UserName,@Password ", parameters);
             return Ok(result);
         }*/

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpDTO signupDTO)
        {
            var user = _Mapper.Map<ApplicationUser>(signupDTO);

            if (ModelState.IsValid)
            {
                var val = await _dbRepository.SignUpUserAsync(user, signupDTO.Password);
                return Ok(user);
            }
            return BadRequest(ModelState);
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDTO loginDTO)
        {
            // generate a token and return a token
            var issuer = _appConfig["JWT:Issuer"];
            var audience = _appConfig["JWT:Audience"];
            var key = _appConfig["JWT:Key"];

            if (ModelState.IsValid)
            {
                var loginResult = await _dbRepository.SignInUserAsync(loginDTO);
                if (loginResult.Succeeded)
                {
                    // generate a token
                    var user = await _dbRepository.FindUserByEmailAsync(loginDTO.UserName);
                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id) // Set the user ID as NameIdentifier claim
                        };


                        var keyBytes = Encoding.UTF8.GetBytes(key);
                        var theKey = new SymmetricSecurityKey(keyBytes); // 256 bits of key
                        var creds = new SigningCredentials(theKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(issuer, audience, null, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                        // token 
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid username or password."); // Add a custom error message to the model state
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
    }
}