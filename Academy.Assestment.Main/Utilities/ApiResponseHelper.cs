using Newtonsoft.Json.Linq;

namespace Academy.Assestment.Main.Utilities
{
    public static class ApiResponseHelper
    {
        public static string GetMessage(JObject tokenResult)
        {
            return tokenResult["result"].ToString();
        }

        public static bool IsRequestSucceeded(JObject requestResult)
        {
            return requestResult.GetValue("hasErrors").ToObject<bool>() == false;
        }

        public static string GetToken(JObject result)
        {
            return result["result"]["token"].ToString();
        }
    }
}
