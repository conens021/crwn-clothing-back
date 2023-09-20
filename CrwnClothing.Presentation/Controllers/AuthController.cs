using Microsoft.AspNetCore.Mvc;
using CrwnClothing.BLL.DTOs;
using CrwnClothing.Presentation.Attributes;
using CrwnClothing.BLL.Services;
using static CrwnClothing.BLL.External.Contracts.FacebokUserInfo;
using Microsoft.AspNetCore.SignalR;
using CrwnClothing.Presentation.Hubs;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtAuthenticationManager _jwt;
        private readonly IUserService _userService;

        public AuthController(
            JwtAuthenticationManager jwt, 
            IUserService userService)
        {
            _jwt = jwt;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(AuthUserDTO authUserDTO)
        {
            UserDTO userDTO = _userService.AuthUser(authUserDTO);


            return Ok(_jwt.Auth(userDTO));
        }

        [HttpPost("social")]
        public async Task<IActionResult> SocialLogin(ExternalAuthDTO externalAuth)
        {
            var socialUser = await _jwt.VerifyExternalToken(externalAuth);

            UserDTO userDTO = _userService.GetUserByUsername(socialUser.Email);


            return Ok(_jwt.Auth(userDTO));
        }
    }
}
