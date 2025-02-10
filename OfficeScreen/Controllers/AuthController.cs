using System.Data;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using OfficeScreen.Data;
using OfficeScreen.Models.Dtos;
using OfficeScreen.Models.Entities;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;


namespace OfficeScreen.Controllers
{


    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto regModel)
        {
            var regUser = new User {UserName = regModel.UserName, FirstName = regModel.FirstName!, LastName = regModel.LastName!, Admin = regModel.Admin };
            var result = await _userManager.CreateAsync(regUser, regModel.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            string role = (regUser.Admin == true ? RoleDefinitions.admin.ToString() : RoleDefinitions.user.ToString());

            await _userManager.AddToRoleAsync(regUser, role);

            return CreatedAtAction(nameof(Register), new { id = regUser.Id }, new
            {
                message = "User registered successfully!",
                fullname = regModel.Fullname,
                userId = regUser.Id,
                regModel.Admin
            });
        }





        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto authModel)
        {

            
             var authUser = await _userManager.FindByNameAsync(authModel.UserName!);
            if (authUser == null || !(await _userManager.CheckPasswordAsync(authUser, authModel.Password)))
                return Unauthorized("Invalid credentials");



            
            var token = GenerateJwtToken(authUser);
            return Ok(new { token});
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (ClaimTypes.Role, (user.Admin == true ? RoleDefinitions.admin.ToString() : RoleDefinitions.user.ToString()))
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:Expires"])),
                signingCredentials: credentials
            );

           

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
