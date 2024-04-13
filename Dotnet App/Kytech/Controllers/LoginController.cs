using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kytech.models;
using Kytech.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Kytech.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
       private readonly ILogger<LoginController> _logger;        
        private ILogin _login;
    public LoginController(ILogger<LoginController> logger,ILogin loginC)
    {
        _logger = logger;
        _login=loginC;
    }     
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAuth([FromBody]Login login)
        {
            var user = await _login.GetUserById(login.Username);
            var authClaims = new List<Claim>();
            authClaims.Add(new Claim("User", login.Username.ToString()));
           authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        if (user.Username == "Admin" && user != null && user.Password == login.Password)
            {
                  authClaims.Add(new Claim("Role", "Admin"));
                   var token = GetToken(authClaims);
                    return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else if(user.Username != null && user.Password==login.Password)
             {
               
                authClaims.Add(new Claim("Role", "User"));
                  var token = GetToken(authClaims);
                    return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else{
            return Unauthorized();
            }
        }
  

    private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
           
            var data ="adfafadfasfafafafadfdafasfadfdafdsafasdfdasfdafdafadsfda";           
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(data));
              var token = new JwtSecurityToken(
                issuer:"http://localhost:7228",
                audience:"http://localhost:5167",
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
}
