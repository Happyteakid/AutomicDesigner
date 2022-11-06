using AutomicObjectDesigner.Models.Registration;
using AutomicObjectDesignerBack.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace AutomicObjectDesignerBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly IAuthorizationRepository _AuthorizationRepository;

        public AuthorizationController(IConfiguration configuration, IAuthorizationRepository authorizationrepository)
        {
            _AuthorizationRepository = authorizationrepository;
            Configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserRegister request)
        {
            Console.WriteLine(request);
            if (_AuthorizationRepository.FindByCondition(x => x.UserName == request.UserName).FirstOrDefault() != null)
            {
                return BadRequest("User exist");
            }
            HashPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new UserModel();
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Role = "User";

            _AuthorizationRepository.Create(user);
            await _AuthorizationRepository.Save();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin request)
        {
            var user = _AuthorizationRepository.FindByCondition(x => x.UserName == request.UserName).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("User not found");
            }
            if (!CheckPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(user);
            UserLogin userlogin = new UserLogin();
            userlogin.Token = token;

            return Accepted(userlogin);
        }
        // https://localhost:7017/api/Authorization/id
        // https://localhost:7017/swagger
        [HttpGet("userId"), Authorize]
        public ActionResult<string> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
               .FirstOrDefault().ToString();
            return Ok(userId);
        }

        // https://localhost:7017/api/Authorization/userInfo
        // https://localhost:7017/swagger
        [HttpGet("userInfo"), Authorize]
        public ActionResult<object> GetUserInfo()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name).FirstOrDefault();
            var userRole = User.FindFirstValue(ClaimTypes.Role).FirstOrDefault();
            var userEmail = User.FindFirstValue(ClaimTypes.Email).FirstOrDefault();
            return Ok(new { userName, userRole, userEmail });
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool CheckPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
