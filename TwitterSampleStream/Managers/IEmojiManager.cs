using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;

namespace TwitterSampleStreamAPI.Managers
{
    public interface IEmojiManager
    {
        public Task<List<Emoji>> GetCurrentlyTrendingEmojis(int NumberOfTopEmojis = 1);
        public Task<List<Emoji>> GetEmojiPastTrends(DateTime StartTime, int NumberOfTopEmojis = 1);
        public Task<decimal> GetPercentageOfTweetsWithEmojis();
    }
}
