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
    public async Task<string[]> GetWordsArray()
    {
        var client = new RestClient("https://random-word-api.herokuapp.com");
        var request = new RestRequest("/all", Method.Get);

        RestResponse restResponse = await client.GetAsync(request);
        string responseContent = restResponse.Content;

        JArray jsonArray = JArray.Parse(responseContent);
        string[] wordsArray = jsonArray.ToObject<string[]>();

        return wordsArray;
    }

    public async Task Main()
    {
        string[] wordsArray = await GetWordsArray();

        Console.WriteLine("Zwrócone słowa:");
        foreach (string word in wordsArray)
        {
            Console.WriteLine(word);
        }
    }
}

}
