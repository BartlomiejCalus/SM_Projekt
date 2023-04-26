namespace Wordle.Models.Game
{
    public class WordInfo
    {
        public string word { get; set; }
        public WordInfo(string word) { this.word = word; }

        public WordInfo() {
            word = "N/A";
        }
    }
}
