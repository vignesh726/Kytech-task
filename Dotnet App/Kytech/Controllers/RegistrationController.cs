using Kytech.models;
using Kytech.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kytech.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private IRegistration _registration;
    public RegistrationController(ILogger<RegistrationController> logger,IRegistration registration)
    {
        _logger = logger;
        _registration = registration;
    }

        [HttpGet]     
        public async Task<IEnumerable<UserDetail>> Get()
        {
           return await this._registration.GetAllUsers();
        }
         [HttpGet("{id}")]     
         [Authorize(Roles= "Admin,User")]  
        public async Task<UserDetail> Get(int id)
        {
           return await this._registration.GetUserById(id);         
           
        }
        [HttpPut("{id}")]
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Put([FromBody]UserDetail user, int id)
        {
            var result = await _registration.UpdateUser(user, id); 
            return CreatedAtAction(nameof(Get),
                         new { id = id}, result);
        }
         [HttpPost]
         [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Post([FromBody] UserDetail userDetail)
        {
            await this._registration.AddUser(userDetail);
            return CreatedAtAction(nameof(Get),
                       new { id = userDetail.Id }, userDetail);
        }
         [HttpDelete("{id}")]
         [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _registration.DeleteUser(id))
                return new OkResult();
            else
                return new BadRequestResult();
        }



}
