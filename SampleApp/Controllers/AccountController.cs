using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Models;
using SampleApp.Models.DTO;

namespace SampleApp.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/values
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AppUserDTO userDto)
        {
            var user = new AppUser {UserName = userDto.Username};
            var registrationResult = await _userManager.CreateAsync(user, userDto.Password);
            return registrationResult.Succeeded ? Ok() : (IActionResult) BadRequest(registrationResult.Errors);
        }
    }
}