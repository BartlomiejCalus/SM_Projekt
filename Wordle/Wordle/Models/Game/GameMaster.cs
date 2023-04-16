using Microsoft.Extensions.Caching.Memory;

namespace Wordle.Models.Game
{
    public abstract class GameMaster
    {
        public string gameMode { get; protected set; }

        public WordInfo wordInfo { get; protected set; }

        public DateTime time { get; private set; }

        public GameMaster() { 
            time = DateTime.Now;

        }

        protected abstract WordInfo GetWordInfo();

        public override string ToString() => gameMode;
    }
}
