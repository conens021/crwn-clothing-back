using CrwnClothing.BLL.External.Contracts;
using DroneDropshipping.BLL.Exceptions;
using Newtonsoft.Json;

namespace CrwnClothing.BLL.Services.External
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IHttpClientFactory _httpClient;

        private const string GOOGLE_TOKEN_VALIDATION_URL = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}";
        private const string GOOGLE_USER_INFO_URL = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

        public GoogleAuthService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GoogleUserInfo?> GetUserInfo(string accessToken)
        {
            try
            {
                string formatedUrl = string.Format(GOOGLE_USER_INFO_URL, accessToken);

                var result = await _httpClient.CreateClient().GetAsync(formatedUrl);

                //if its not success it will throw an exception
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadAsStringAsync();


                return JsonConvert.DeserializeObject<GoogleUserInfo?>(response);
            }
            catch (Exception)
            {
                throw new BusinessException("Error while obtaining user info", 401);
            }
        }

        public async Task<GoogleTokenValidationResult?> ValidateAccessTokenAsync(string debugingToken)
        {
            try
            {
                string formatedUrl = string.Format(GOOGLE_TOKEN_VALIDATION_URL, debugingToken);

                var result = await _httpClient.CreateClient().GetAsync(formatedUrl);

                //if its not success it will throw an exception
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadAsStringAsync();


                return JsonConvert.DeserializeObject<GoogleTokenValidationResult?>(response);
            }
            catch (Exception)
            {
                throw new BusinessException("Error while validating user token", 401);
            }

        }
    }
}
