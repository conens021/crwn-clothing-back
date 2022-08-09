using CrwnClothing.BLL.External.Contracts;

namespace CrwnClothing.BLL.Services.External
{
    public interface IFacebookAuthService
    {
        public Task<FacebookTokenValidationResult?> ValidateAccessTokenAsync(string debugingToken,string accessToken);
        public Task<FacebokUserInfo?> GetUserInfo(string id,string accessToken);

    }
}
