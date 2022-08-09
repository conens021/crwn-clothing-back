using Microsoft.AspNetCore.Mvc;
using CrwnClothing.BLL.Services;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.Presentation.Attributes;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public IUserService _userService;
        private readonly JwtAuthenticationManager _jwt;


        public UserController(IUserService userService, JwtAuthenticationManager jwt)
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
            return Ok(_userService.GetUser(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDto)
        {
            UserDTO userCreated = _userService.CreateUser(userDto);

            return Created("/", userCreated);
        }

        [HttpGet("email-verification-code")]
        public async Task<IActionResult> GetVerificationCode()
        {
            await _userService.SendEmailVerificationCode(_jwt.GetBearerUser());

            return Ok();
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> Post([FromBody]EmailVerificationCode code)
        {
            bool completed = await _userService.VerifyEmail(_jwt.GetBearerUser(), code);


            return Ok(completed);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
