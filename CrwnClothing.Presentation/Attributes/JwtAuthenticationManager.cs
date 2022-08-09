using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.External.Contracts;
using CrwnClothing.BLL.Services.External;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrwnClothing.Presentation.Attributes
{
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccesor;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IGoogleAuthService _googleAuthService;


        public JwtAuthenticationManager
            (
                IConfiguration configuration,
                IHttpContextAccessor contextAccesor,
                IFacebookAuthService facebookAuthService,
                IGoogleAuthService googleAuthService
            )
        {
            _configuration = configuration;
            _contextAccesor = contextAccesor;
            _facebookAuthService = facebookAuthService;
            _googleAuthService = googleAuthService;
        }

        public string Auth(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_configuration["AppSettings:JWTEncryptionKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    #pragma warning disable CS8604 // Possible null reference argument.
                    new Claim(ClaimTypes.NameIdentifier,(user.Id != null) ? user.Id.ToString() : ""),
                    #pragma warning restore CS8604 // Possible null reference argument.
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,user.Username),
                    new Claim("verified",user.Verified.ToString() ?? "false")
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }

        public async Task<GoogleUserInfo> VerifyGoogleToken(ExternalAuthDTO externalAuth)
        {
            var tokenInfo = await
                _googleAuthService.ValidateAccessTokenAsync(externalAuth.IdToken);

            if (tokenInfo == null) throw new Exception("User not found, or invalid access token!");

            var userInfo = await _googleAuthService.GetUserInfo(externalAuth.IdToken);

            if (userInfo == null) throw new Exception("Wrong user id!");


            return userInfo;
        }

        public async Task<FacebokUserInfo> VerifyFacebookToken(ExternalAuthDTO externalAuth)
        {
            string facebokAccessToken = _configuration["ExternalKey:Facebook"];


            var tokenInfo = await
                _facebookAuthService.ValidateAccessTokenAsync(externalAuth.IdToken, facebokAccessToken);

            if (tokenInfo == null) throw new Exception("User not found, or invalid access token!");


            string userId = tokenInfo.Data.UserId;

            var userInfo = await _facebookAuthService.GetUserInfo(userId, externalAuth.IdToken);


            if (userInfo == null) throw new Exception("Wrong user id!");

            return userInfo;
        }


        public UserDTO GetBearerUser()
        {

            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:JWTEncryptionKey"]);

            var token = GetToken();

            var handler = new JwtSecurityTokenHandler();

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);


            UserDTO userAuthorize = ToUser(claims);


            return userAuthorize;
        }


        private string GetToken()
        {
            var jwtTokenValue = _contextAccesor?.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");


            return jwtTokenValue;
        }


        private static UserDTO ToUser(ClaimsPrincipal claims)
        {

            var id = claims.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier.ToString()).FirstOrDefault()?.Value;
            var email = claims.Claims.Where(c => c.Type == ClaimTypes.Email.ToString()).FirstOrDefault()?.Value;
            var username = claims.Claims.Where(c => c.Type == ClaimTypes.Name.ToString()).FirstOrDefault()?.Value;
            var verified = claims.Claims.Where(c => c.Type == "verified").FirstOrDefault()?.Value;


            UserDTO userAuthorize = new UserDTO();

            userAuthorize.Id = id == null ? 0 : Int32.Parse(id);
            userAuthorize.Email = email == null ? "" : email;
            userAuthorize.Username = username ?? "";
            userAuthorize.Verified = Convert.ToBoolean(verified);


            return userAuthorize;
        }
    }

}
