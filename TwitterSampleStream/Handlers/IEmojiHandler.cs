using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterSampleStreamAPI.Handlers
{
    public interface IEmojiHandler
    {
        public bool CheckIfTextHasEmoji(List<string> TweetChars);
        public List<string> GetEmojisFromText(string TweetText);
    }
}
