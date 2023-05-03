using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;
using System.Security.Principal;
using Wordle.Areas.Identity.Data;
using Wordle.Controllers;
using Wordle.Data;
using static Wordle.Models.ArrayRequest.WordsArray;

namespace Wordle.Models.Game
{
    public class Casual : GameMaster
    {
        protected readonly IMemoryCache _memoryCache;

        public Random rankedRandom { get; private set; }
        public int rounds { get; set; }
        public int currentRound { get; set; }

        private double expiration { get; set; }

        public Casual(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            rankedRandom = new Random((int)this.time.Ticks);
            gameMode = "Casual";
            currentRound = 0;
            expiration = 1440;
            wordInfo = GetWordInfo();

        }

        protected override WordInfo GetWordInfo()
        {
            WordInfo wordInfo;
            string key = ToString() + currentRound.ToString();
            string get = _memoryCache.Get<string>(key);
            if (get == null)
            {
                wordInfo = new WordInfo(randomWord(currentRound));
                _memoryCache.Set(key, wordInfo.word, TimeSpan.FromMinutes(expiration) - DateTime.Now.TimeOfDay);
            }
            wordInfo = new WordInfo(get);
            return wordInfo;
        }
        public int getWordLength()
        {
            return wordInfo.word.Length;
        }
        public int nextRound()
        {
            currentRound++;
            return currentRound;
        }

        public List<List<bool>> Play(string querry)
        {
            List<List<bool>> list = new List<List<bool>>();
            list.Add(letterPresence(querry));
            list.Add(letterOccurrence(querry));
            return list;
        }


        private string randomWord(int round)
        {
            string[] result;
            WordsApi api = new WordsApi();
            Task<string[]> task = api.GetRandomWords();
            task.Wait();
            result = task.Result;
            Console.WriteLine(result[0]);
            return result[0];
        }

        private List<bool> letterOccurrence(string querry)
        {
            List<bool> result = new List<bool>();
            string model = wordInfo.word;
            int modelLength = model.Length;
            for (int i = 0; i < modelLength; i++)
            {
                if (model[i] == querry[i])
                {
                    result.Add(true);
                }
                else
                {
                    result.Add(false);
                }
            }

            return result;
        }
        private List<bool> letterPresence(string querry)
        {
            List<bool> result = new List<bool>();
            string model = wordInfo.word;
            int querryLength = querry.Length;
            for (int i = 0; i < querryLength; i++)
            {
                result.Add(model.Contains(querry[i]));
            }
            return result;
        }
    }

}
