using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ByteMeAPI.IDP
{
    public class AuthenticationToken : IAuthenticationToken
    {
        const string BASE_URL_AUTHENTICATION = "https://testtrunk.elitewealth.biz/EliteWealth/TestTrunkDemo/restApiAuthentication";
        const string API_KEY = "Q3NuWXrjNnB8szjXd7y8bt2TR4-DgFdkKQnj45jKc8xmMVbY6frwRkZXmYsqYrCcwQL";
        const string USER_NAME = "WebServiceDemo";
        const string PASSWORD = "Dem@nstrate13";

        const string BASE_URL_DATA = "https://testtrunk.elitewealth.biz/EliteWealth/TestTrunkDemo/restApiData";

        public AuthenticationToken()  
        {
            Console.WriteLine("AuthenticationToken");

            var authenticationResponseModel = GetAuthenticationToken();

            while (!Console.KeyAvailable)
            {
                var result = GetClientInfo(ref authenticationResponseModel);
                Console.WriteLine(result);

                Thread.Sleep(1000);
            }
        }

        private static AuthenticationResponseModel GetAuthenticationToken()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL_AUTHENTICATION + "/RestApiAccessToken")
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("api-key", API_KEY);

            var authenticationRequestModel = new AuthenticationRequestModel
            {
                Username = USER_NAME,
                Password = PASSWORD
            };

            var jsonRequest = JsonConvert.SerializeObject(authenticationRequestModel);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("", httpContent).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            var authenticationResponseModel = JsonConvert.DeserializeObject<AuthenticationResponseModel>(jsonResponse);
            Console.WriteLine($"Received access token '{authenticationResponseModel.AccessToken}'");

            return authenticationResponseModel;
        }

        private static AuthenticationResponseModel RefreshAuthenticationToken(AuthenticationResponseModel authenticationResponseModel)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL_AUTHENTICATION + "/RestApiRefreshToken")
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("api-key", API_KEY);

            var refreshAccessTokenModel = new RefreshAccessTokenModel
            {
                AccessToken = authenticationResponseModel.AccessToken,
                RefreshToken = authenticationResponseModel.RefreshToken
            };

            var jsonRequest = JsonConvert.SerializeObject(refreshAccessTokenModel);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("", httpContent).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            var authenticationResponseModelNew = JsonConvert.DeserializeObject<AuthenticationResponseModel>(jsonResponse);
            Console.WriteLine($"Renewed access token '{authenticationResponseModelNew.AccessToken}'");

            return authenticationResponseModelNew;
        }

        private static string GetClientInfo(ref AuthenticationResponseModel authenticationResponseModel)
        {
            var httpClient = GetClientInfoHttpClient(authenticationResponseModel);

            var response = httpClient.GetAsync("").Result;

            //It's possible that access token has expired, try to renew it
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                authenticationResponseModel = RefreshAuthenticationToken(authenticationResponseModel);
                httpClient = GetClientInfoHttpClient(authenticationResponseModel);
                response = httpClient.GetAsync("").Result;
            }

            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content.ReadAsStringAsync().Result;

            throw new Exception("API called failed with HTTP " + response.StatusCode + " status code.");
        }

        private static HttpClient GetClientInfoHttpClient(AuthenticationResponseModel authenticationResponseModel)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL_DATA + "/RestApiMoreboPortfolio")
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authenticationResponseModel.AccessToken);
            return httpClient;
        }
    }
}
