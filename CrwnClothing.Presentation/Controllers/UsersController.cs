using Microsoft.AspNetCore.Mvc;
using CrwnClothing.BLL.Services;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.Presentation.Attributes;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserService _userService;
        private readonly JwtAuthenticationManager _jwt;


        public UsersController(IUserService userService, JwtAuthenticationManager jwt)
        {
            _userService = userService;
            _jwt = jwt;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetById(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO userDto)
        {
            UserDTO userCreated = await _userService.Create(userDto);


            return Ok(_jwt.Auth(userCreated));
        }

        [HttpPost("social")]
        public async Task<IActionResult> CreateSocial([FromBody] ExternalAuthDTO externalAuth)
        {
            ExternalUserInfoDTO externalUserInfoDTO =
                await _jwt.VerifyExternalToken(externalAuth);

            UserDTO userCreated = await _userService.Create(externalUserInfoDTO);


            return Ok(_jwt.Auth(userCreated));
        }

        [HttpGet("email-verification-code")]
        public async Task<IActionResult> GetVerificationCode()
        {
            await _userService.SendEmailVerificationCode(_jwt.GetBearerUser());


            return Ok();
        }

        [HttpGet("email-verification-code/{email}")]
        public async Task<IActionResult> GetVerificationCodeByEmail(string email)
        {
            await _userService.SendEmailVerificationCode(email);


            return Ok();
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> Post([FromBody] EmailVerificationCode code)
        {
            UserDTO userDTO = await _userService.VerifyEmail(_jwt.GetBearerUser(), code);


            return Ok(_jwt.Auth(userDTO));
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
