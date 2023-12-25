using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Empty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Empty.Datas;
namespace Empty.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppSettings _applicationSettings;
        private readonly UserContext _context;

        public AuthController(IOptions<AppSettings> appSettings, UserContext context)
        {
            this._applicationSettings = appSettings.Value;
            this._context = context;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _context.Users.Where(x => x.UserName == model.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Invalid Username");
            }
            var match = CheckPassword(model.Password, user);

            if (!match)
            {
                return BadRequest("Invalid Password or Username");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return Ok(new { token = encryptedToken, username = user.UserName });

        }


        private bool CheckPassword(string password, User user)
        {
            bool result;
            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = computedHash.SequenceEqual(user.PasswordHash);
            }
            return result;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Role = model.Role
            };
            if (model.ConfirmPassword == model.Password)
            {
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                }
            }
            else
            {
                return BadRequest("Passwords do not match");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
