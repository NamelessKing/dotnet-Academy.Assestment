using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Assestment.Main
{
    class Program
    {

        //Test();
        //BusinessLogic = new UserManager();

        //while (true)
        //{
        //    Console.Clear();
        //    Console.Write("Enter username: ");
        //    var username = Console.ReadLine();

        //    Console.Write("Enter password: ");
        //    var password = Console.ReadLine();

        //    try
        //    {
        //        var info = BusinessLogic.Login(username, password);
        //        Console.WriteLine($"L'utente {username} si chiama {info.FirstName}");
        //    }
        //    catch (UserUnknownException)
        //    {
        //        Console.WriteLine($"L'utente {username} non esiste oppure la password è sbagliata");
        //    }

        //    Console.WriteLine("Premi un pulsante per continuare");
        //    Console.ReadLine();
        //}




        static void Main(string[] args)
        {



            while (true)
            {
                Console.Clear();
                Console.Write("Enter username: ");
                var username = Console.ReadLine();

                Console.Write("Enter password: ");
                var password = Console.ReadLine();


                JObject loginResult = LoginResult(username, password);
                if (IsRequestSucceeded(loginResult))
                {
                    //Console.WriteLine(loginResult.ToString() + "\n\n\n");
                    string token = GetToken(loginResult);
                    JObject tokenResult = GetResultWithToken(token);


                    if (!IsRequestSucceeded(tokenResult))
                    {
                        Console.WriteLine("Token not valid.");
                    }
                    else
                    {
                        Console.WriteLine($"\n\n======================LOGIN SUCCEEDED======================\n\n");
                        Console.WriteLine($"Username : {username}");
                        Console.WriteLine($"\nMESSAGE : {GetMessage(tokenResult)}");

                        Console.WriteLine("\n\nPress ENTER to continue...");
                    }


                }
                else
                {
                    Console.WriteLine("\n\nWrong username or password.");
                    Console.WriteLine("\nPress ENTER to try again...");
                }
                Console.ReadLine();


            }

        }

        private static string GetMessage(JObject tokenResult)
        {
            return tokenResult["result"].ToString();
        }

        private static bool IsRequestSucceeded(JObject requestResult)
        {
            return requestResult.GetValue("hasErrors").ToObject<bool>() == false;
        }

        private static JObject GetResultWithToken(string token)
        {
            return JObject.Parse(RequestWithToken(token).Result);
        }

        private static JObject LoginResult(string username, string password)
        {
            return JObject.Parse(RequestLogin(username, password).Result);
        }

        private static string GetToken(JObject result)
        {
            return result["result"]["token"].ToString();
        }

        private static async Task<string> RequestLogin(string username,string password)
        {
            HttpClient client = new HttpClient();

            var data = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\" }}", Encoding.UTF8, "application/json");

            var result = await client.PostAsync(
                "http://dbacaw.westeurope.azurecontainer.io/Assestment/Login", data);

            var content = await result.Content.ReadAsStringAsync();
            return content;
        }



        private static async Task<string> RequestWithToken(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            var data = new StringContent("", Encoding.UTF8, "application/json");

            var result = await client.PostAsync(
                "http://dbacaw.westeurope.azurecontainer.io/Assestment/get-code",data);

            var content = await result.Content.ReadAsStringAsync();
            return content;
        }



    }
}

