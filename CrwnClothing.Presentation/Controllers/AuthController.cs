using Microsoft.AspNetCore.Mvc;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.Presentation.Attributes;
using CrwnClothing.BLL.Services;
using static CrwnClothing.BLL.External.Contracts.FacebokUserInfo;
using Microsoft.AspNetCore.SignalR;
using CrwnClothing.Presentation.Hubs;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtAuthenticationManager _jwt;
        private readonly IUserService _userService;
        private readonly IHubContext<AuthHub> _hubContext;

        public AuthController(JwtAuthenticationManager jwt, IUserService userService, IHubContext<AuthHub> hubContext)
        {
            _jwt = jwt;
            _userService = userService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Login(AuthUserDTO authUserDTO)
        {
            UserDTO userDTO = _userService.AuthUser(authUserDTO);


            return Ok(_jwt.Auth(userDTO));
        }


        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(ExternalAuthDTO externalAuth)
        {
            var googleUser = await _jwt.VerifyGoogleToken(externalAuth);

            UserDTO userDTO = _userService.GetUserByUsername(googleUser.Email);


            return Ok(_jwt.Auth(userDTO));
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> FacebookLogin(ExternalAuthDTO externalAuth)
        {
            var facebookUser = await _jwt.VerifyFacebookToken(externalAuth);

            UserDTO userDTO = _userService.GetUserByUsername(facebookUser.Email);


            return Ok(_jwt.Auth(userDTO));
        }
    }
}
