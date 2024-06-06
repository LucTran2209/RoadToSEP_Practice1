using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practice1.Dtos.InputDto;
using Practice1.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly PRN231_Assignment2Context _context;
        private readonly IMapper _mapper;
        public AuthenticationController(IConfiguration config, PRN231_Assignment2Context context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login(LoginInputDto inputDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = Authenticate(inputDto);
            if (user == null)
            {
                return BadRequest("Username or password is inconrect!");
            }

            var token = GenerateToken(user);

            return Ok(token);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.FirstName + user.LastName),
                new Claim(ClaimTypes.Role,user.RoleId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        private User Authenticate(LoginInputDto inputDto)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.EmailAddress.Equals(inputDto.Email) && u.Password == inputDto.Password);

            return existUser;
        }
    }
}
