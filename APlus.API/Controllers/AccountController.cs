using APlus.API.Data;
using APlus.API.Model;
using APlus.DTO.ReadDTO;
using APlus.Infacstructure.Jwt;
using APlus.Infacstructure.Jwt.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace APlus.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<APlusUser> userManager;
        private readonly SignInManager<APlusUser> signInManager;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AccountController(UserManager<APlusUser> userManager, SignInManager<APlusUser> signInManager, IOptions<JwtSettings> jwtSettimgs)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _jwtSettings = jwtSettimgs;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                APlusUser user = await userManager.FindByNameAsync(model.Username);

                if (user == null) return BadRequest("User not Found");

                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (!result.Succeeded) return BadRequest("Failed to login");

                var jwt = new JwtOAuth(_jwtSettings);
                string Token = jwt.GenerateToken(user.Id, user.UserName);

                var dto = new LoginDTO()
                {
                    Token = Token,
                    UserId = user.Id,
                    Username = user.UserName,
                    ExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.Value.ExpirationTime),
                };

                return Ok(dto);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                //verify that username is not in use

                var user = await userManager.FindByNameAsync(model.Username);

                if (user != null) return BadRequest("Username is already in use");

                //create new user
                APlusUser NewUser = new APlusUser()
                {
                    UserName = model.Username,
                };

                IdentityResult result = await userManager.CreateAsync(NewUser, model.Password);

                if (!result.Succeeded) return BadRequest("Something went wrong");

                return Ok("New User Created Successfully");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
