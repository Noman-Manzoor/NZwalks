using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.DTOs;
using NZwalks.Repository;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IUserRepository _userRepository;
        public ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            //validate incomin request

            //check if user is autheticated

            var user = await _userRepository.AuthenticateUser(loginRequestModel.username, loginRequestModel.password);
            if (user != null)
            {
                //generate jwt token
                var token = await _tokenHandler.CreateToken(user);
                return Ok(token);

            }

            return BadRequest("username or password is incorrect");
        }

    }
}
