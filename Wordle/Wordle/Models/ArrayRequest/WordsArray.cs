using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using Method = RestSharp.Method;

namespace Wordle.Models.ArrayRequest
{
    public class WordsArray
    {
        static async Task Main()
        {
            var client = new RestClient("https://random-word-api.herokuapp.com");
            var request = new RestRequest("/word?length=6", Method.Get);

            RestResponse restResponse = await client.GetAsync(request);
            string responseContent = restResponse.Content;

            JArray jsonArray = JArray.Parse(responseContent);
            string[] wordsArray = jsonArray.ToObject<string[]>();

            Console.WriteLine("Zwrócone słowa:");
            foreach (string word in wordsArray)
            {
                Console.WriteLine(word);
            }

        }
    }
}
