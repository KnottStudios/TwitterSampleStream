using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Models;

namespace TwitterSampleStreamAPI.Managers
{
    public interface ITweetManager
    {
        public Task<long> GetTotalTweets();
        public Task<decimal> GetAverageTweets(TimeEnum TimeName = TimeEnum.Minute);
        public Task<List<Hashtag>> GetTrendingHashtags(DateTime StartTime, int NumberOfTopHashtags = 1);
    }
}
