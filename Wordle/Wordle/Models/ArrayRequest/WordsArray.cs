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
        public class WordsApi
        {
            public async Task<string[]> GetRandomWords()
            {
                var client = new RestClient("https://random-word-api.herokuapp.com");
                var request = new RestRequest("/word?length=5", Method.Get);

                var response = await client.ExecuteAsync(request);
                var words = JArray.Parse(response.Content).ToObject<string[]>();

                return words;
            }
        }

        public class DictionaryApi
        {
            public async Task<string> GetDefinition(string word)
            {
                var client = new RestClient("https://api.dictionaryapi.dev");
                var request = new RestRequest($"/api/v2/entries/en/{word}", Method.Get);

                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "word does not exist in diciotionary API";
                }

                var responseContent = JArray.Parse(response.Content);

                if (responseContent[0]["meanings"][0]["definitions"][0]["definition"] != null)
                {
                    var definition = (string)responseContent[0]["meanings"][0]["definitions"][0]["definition"];
                    return definition;
                }
                else
                {
                    return "No definitions found";
                }
            }
        }

        class Program
        {
            static async Task Main(string[] args)
            {
                var wordsApi = new WordsApi();
                var words = await wordsApi.GetRandomWords();

                Console.WriteLine("Słowo:");
                foreach (var word in words)
                {
                    Console.WriteLine(word);

                    var dictionaryApi = new DictionaryApi();
                    var definition = await dictionaryApi.GetDefinition(word);

                    Console.WriteLine("\nDefinicja słowa:");
                    Console.WriteLine(definition);
                }
            }
        }
    }

}
