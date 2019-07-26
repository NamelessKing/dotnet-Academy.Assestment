using Newtonsoft.Json.Linq;

namespace Academy.Assestment.Main.Utilities
{
    public static class ApiRequestHelper
    {

        public static JObject GetResultWithToken(string token)
        {
            return JObject.Parse(ApiProcessor.RequestWithToken(token).Result);
        }

        public static JObject LoginResult(string username, string password)
        {
            return JObject.Parse(ApiProcessor.RequestLogin(username, password).Result);
        }

    }
}
