using CrwnClothing.BLL.External.Contracts;

namespace CrwnClothing.BLL.Services.External
{
    public interface IGoogleAuthService
    {
        public Task<GoogleTokenValidationResult?> ValidateAccessTokenAsync(string debugingToken);
        public Task<GoogleUserInfo?> GetUserInfo(string accessToken);
    }
}
