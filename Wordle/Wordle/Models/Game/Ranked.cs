using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace Wordle.Models.Game
{
    public class Ranked : GameMaster
    {
        protected readonly IMemoryCache _memoryCache;
        
        public Random rankedRandom { get; private set; }
        public int rounds { get; set; }
        public int currentRound { get; set; }

        private DateTime expiration { get; set; }

        public Ranked(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
            rankedRandom = new Random((int)this.time.Ticks);
            gameMode = "Ranked";
            rounds = 5;
            currentRound = 0;
            expiration.AddMinutes(5);
            wordInfo = GetWordInfo();
        }

        protected override WordInfo GetWordInfo()
        {
            WordInfo wordInfo;
            string key = ToString() + currentRound.ToString();
            wordInfo = _memoryCache.Get<WordInfo>(key);
            if (wordInfo == null)
            {
                wordInfo.word = randomWord(currentRound);
                for (int i = currentRound+1; i < rounds; i++)
                {
                    string stored = randomWord(i);
                    key = ToString() + i;
                    _memoryCache.Set(key, stored, TimeSpan.FromTicks(expiration.Ticks));
                }
            }

            return wordInfo;
        }
        public List<List<bool>> Play(string querry)
        {
            List<List<bool>> list = new List<List<bool>>();
            list[0] = letterPresence(querry);
            list[1] = letterOccurrence(querry);
            return list;

        }
        private string randomWord(int round)
        {
            string result;
            int random = rankedRandom.Next(round)%143888;// 143888 number of words in dictionary
            using (var reader = new StreamReader(@"Dictionary\english.txt"))
            {
                for (int i = 0; i < random; i++)
                {
                    reader.ReadLine();
                }
                result = reader.ReadLine();
                if(result == null)
                {
                    return "\0";
                }
            }
            return result;
        }

        private List<bool> letterOccurrence(string querry)
        {
            List<bool> result = new List<bool>();
            string model = wordInfo.word;
            int modelLength = model.Length;
            for (int i = 0;i<modelLength;i++)
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
