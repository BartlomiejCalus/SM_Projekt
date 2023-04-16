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
                wordInfo.word = "test";

                _memoryCache.Set(key, wordInfo, TimeSpan.FromTicks(expiration.Ticks));
            }

            return wordInfo;
        }

        public List<bool> Guess(string querry)
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
    }
    
}
