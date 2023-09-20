using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.BLL.Services.External;
using CrwnClothing.Presentation.Helpers;
using DroneDropshipping.BLL.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrwnClothing.Presentation.Attributes
{
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly HeaderParser _headerParser;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IGoogleAuthService _googleAuthService;

        public JwtAuthenticationManager
            (
                IConfiguration configuration,
                HeaderParser headerParser,
                IFacebookAuthService facebookAuthService,
                IGoogleAuthService googleAuthService
            )
        {
            _configuration = configuration;
            _headerParser = headerParser;
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

        private async Task<ExternalUserInfoDTO> VerifyGoogleToken(string token)
        {
            var tokenInfo = await
                _googleAuthService.ValidateAccessTokenAsync(token);

            if (tokenInfo == null) throw new Exception("User not found, or invalid access token!");

            var userInfo = await _googleAuthService.GetUserInfo(token);

            if (userInfo == null) throw new Exception("Wrong user id!");


            return userInfo.ToDTO();
        }

        private async Task<ExternalUserInfoDTO> VerifyFacebookToken(string token)
        {
            string facebokAccessToken = _configuration["ExternalKey:Facebook"];


            var tokenInfo = await
                _facebookAuthService.ValidateAccessTokenAsync(token, facebokAccessToken);

            if (tokenInfo == null) throw new Exception("Expired or invalid user token!");


            string userId = tokenInfo.Data.UserId;

            var userInfo = await _facebookAuthService.GetUserInfo(userId, token);


            if (userInfo == null) throw new Exception("Invalid extarnal user id!");

            return userInfo.ToDTO();
        }

        public async Task<ExternalUserInfoDTO> VerifyExternalToken(ExternalAuthDTO externalAuthDTO)
        {
            switch (externalAuthDTO.Provider)
            {
                case "FACEBOOK":
                    return await VerifyFacebookToken(externalAuthDTO.IdToken);

                case "GOOGLE":
                    return await VerifyGoogleToken(externalAuthDTO.IdToken);

                default:
                    throw new BusinessException("Invalid Oauth2 provider", 404);
            }

        }

        public UserDTO GetBearerUser()
        {

            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:JWTEncryptionKey"]);

            string? token = GetToken();

            if (token == null)
                throw new BusinessException("Authorization token not provided!", 401);

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


        private string? GetToken()
        {
            string? jwtTokenValue = _headerParser.Get("Authorization")?.Replace("Bearer ", "");


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
            userAuthorize.Verified = verified != null && verified != String.Empty && Convert.ToBoolean(verified);


            return userAuthorize;
        }
    }

}
