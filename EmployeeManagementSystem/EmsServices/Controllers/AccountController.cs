using EmsServices.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly List<Users> usersData = new List<Users>()
        {
            new Users{ Id= 1, Username="test", Password="test"},
            new Users{ Id=2, Username="new", Password="new"}
        };
        IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            var user = Authenticate(request);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        private string Generate(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username),

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Users Authenticate(UserLogin userLogin)
        {
            Users results = null;
            if (usersData.Any(x => x.Username == userLogin.Username && x.Password == userLogin.Password))
            {
                results = usersData.First(z => z.Username == userLogin.Username && z.Password == userLogin.Password);
            }


            if (results != null)
            {
                return results;
            }
            return null;
        }
    }
}
