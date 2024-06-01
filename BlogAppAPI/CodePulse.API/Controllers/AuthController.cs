using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this.tokenRepository= tokenRepository;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var user = new IdentityUser()
            {
                UserName = request.Name.Trim(),
                Email = request.Name.Trim(),


            };

          var identityResult=  await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "READER");

            }

            else
            {
                return Ok(identityResult.Errors);
            }

            return Ok();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(RegisterRequestDto request) 
        {
            var userCheck=await _userManager.FindByNameAsync(request.Name);
            
            if (userCheck != null) 
            {
                var passwordCheck= await _userManager.CheckPasswordAsync(userCheck,request.Password);

                if (passwordCheck) 
                {
                    var roles = await _userManager.GetRolesAsync(userCheck);

                    var jwtToken = tokenRepository.CreateJwtToken(userCheck, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Name = request.Name,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return Ok(response);
                }

               

                

                return Ok(passwordCheck);
            }

            ModelState.AddModelError("", "password or user incorrect");
            return ValidationProblem(ModelState);
        }


    }
}
