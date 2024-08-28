using ASPNetEntityFrameWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNetEntityFrameWork.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult Login(LoginDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please provide Username/Password");
            }

            LoginResponseDTO response = new() { UserName=model.UserName};
            if(model.UserName == "Intikhab" && model.Password=="abc@123") 
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSceret"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDecriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        //new Claim(ClaimTypes.Role,"Admin")
                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials=new (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDecriptor);
                //var tpkenGenerated=tokenHandler.WriteToken(token);
                response.Token=tokenHandler.WriteToken(token);
            }
            else
            {
                return Ok("Please enetr valid username and password");
            }
            return Ok(response);
        }

    }
}
