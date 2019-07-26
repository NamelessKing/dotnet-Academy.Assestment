using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Assestment.Main.Utilities
{
    public static class ApiProcessor
    {

        private static readonly string LoginUrl = "http://dbacaw.westeurope.azurecontainer.io/Assestment/Login";
        private static readonly string GetCodeUrl = "http://dbacaw.westeurope.azurecontainer.io/Assestment/get-code";

        public static async Task<string> RequestLogin(string username, string password)
        {
           
            var data = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\" }}", Encoding.UTF8, "application/json");

            var result = await ApiHelper.ApiClient.PostAsync(LoginUrl, data);

            var content = await result.Content.ReadAsStringAsync();

            return content;
        }

        public static async Task<string> RequestWithToken(string token)
        {

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            var data = new StringContent("", Encoding.UTF8, "application/json");

            var result = await ApiHelper.ApiClient.PostAsync(GetCodeUrl, data);

            var content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }
}
