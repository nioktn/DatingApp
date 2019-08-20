using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Persistance;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Dtos;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //TODO: validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("User with such username already exists");

            var userToCreate = new User { Username = userForRegisterDto.Username };

            var createdUser = _repo.Register(userToCreate, userForRegisterDto.Password);

            //TODO: implement returning of CreatedAtRoute result
            return StatusCode(201);
        }
    }
}