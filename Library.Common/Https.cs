using System.Net.Http.Json;

namespace Library.Common
{
    public static class Https
    {
        public static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            return httpClient;
        }

        public static string AuthController = "https://localhost:7114/Authenticate";

        public static async Task<string?> VerifyToken(string token)
        {
            var httpClient = CreateHttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthController + "/verify-token");
            requestMessage.Content = JsonContent.Create(token);
            var response = httpClient.Send(requestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<CodeResponse?> GenerateCode(string token)
        {
            var httpClient = CreateHttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthController + "/generate-code");
            requestMessage.Content = JsonContent.Create(token);
            var response = httpClient.Send(requestMessage);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CodeResponse>();
        }

        public static async Task<bool> Login(string userName, string password)
        {
            var httpClient = CreateHttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthController + "/login");
            requestMessage.Content = JsonContent.Create(new LoginRequest(userName, password));
            var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false); ;
            return response.IsSuccessStatusCode;

        }

        public static async Task<HttpResponseMessage?> VerifyLogin(string userName, string password, string code)
        {
            var httpClient = CreateHttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthController + "/verify-login");
            requestMessage.Content = JsonContent.Create(new VerifyLoginRequest(userName, password, code));
            var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false); ;
            return response;
        }

    }
    public record CodeResponse(string Code, DateTime CodeExpireTime);
    public record LoginRequest(string UserName, string Password);
    public record VerifyLoginRequest(string UserName, string Password, string Code);
}