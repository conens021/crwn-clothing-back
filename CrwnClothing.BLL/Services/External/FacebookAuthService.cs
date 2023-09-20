using CrwnClothing.BLL.External.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CrwnClothing.BLL.Services.External
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly IHttpClientFactory _httpClient;

        private const string FACEBOOK_TOKEN_VALIDATION_URL = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}";
        private const string FACEBOOK_USER_INFO_URL =
            "https://graph.facebook.com/{0}?access_token={1}&fields=id,name,email,picture,first_name,last_name";

        
        public FacebookAuthService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<FacebokUserInfo?> GetUserInfo(string userId, string accessToken)
        {
            string formatedUrl = string.Format(FACEBOOK_USER_INFO_URL, userId, accessToken);

            var result = await _httpClient.CreateClient().GetAsync(formatedUrl);

            //if its not success it will throw an exception
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadAsStringAsync();

            if (response == null) throw new Exception("Network error");


            return JsonConvert.DeserializeObject<FacebokUserInfo?>(response);
        }

        public async Task<FacebookTokenValidationResult?> ValidateAccessTokenAsync(string debugingToken, string accessToken)
        {

            string debugUrl = string.Format(FACEBOOK_TOKEN_VALIDATION_URL, debugingToken, accessToken);

            var result = await _httpClient.CreateClient().GetAsync(debugUrl);

            //if its not success it will throw an exception
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadAsStringAsync();

            if (response == null) throw new Exception("Network error");


            return JsonConvert.DeserializeObject<FacebookTokenValidationResult?>(response);
        }
    }
}
