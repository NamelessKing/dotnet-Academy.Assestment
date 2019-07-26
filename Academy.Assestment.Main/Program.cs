using Academy.Assestment.Main.Utilities;
using Newtonsoft.Json.Linq;
using System;

namespace Academy.Assestment.Main
{
    class Program
    {

        static void Main(string[] args)
        {

            ApiHelper.InitializeClient();

            while (true)
            {
                Console.Clear();
                Console.Write("Enter username: ");
                var username = Console.ReadLine();

                Console.Write("Enter password: ");
                var password = Console.ReadLine();


                JObject loginResult = ApiRequestHelper.LoginResult(username, password);
                if (ApiResponseHelper.IsRequestSucceeded(loginResult))
                {
                    //Console.WriteLine(loginResult.ToString() + "\n\n\n");
                    string token = ApiResponseHelper.GetToken(loginResult);
                    JObject tokenResult = ApiRequestHelper.GetResultWithToken(token);


                    if (!ApiResponseHelper.IsRequestSucceeded(tokenResult))
                    {
                        Console.WriteLine("Token not valid.");
                    }
                    else
                    {
                        Console.WriteLine($"\n\n======================LOGIN SUCCEEDED======================\n\n");
                        Console.WriteLine($"Username : {username}");
                        Console.WriteLine($"\nMESSAGE : {ApiResponseHelper.GetMessage(tokenResult)}");

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

    }
}

