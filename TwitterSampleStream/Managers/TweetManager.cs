using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Handlers;
using TwitterSampleStreamAPI.Models;

namespace TwitterSampleStreamAPI.Managers
{
    public class TweetManager : ITweetManager
    {
        public async Task<decimal> GetAverageTweets(TimeEnum TimeName = TimeEnum.Minute)
        {
            var data = await TwitterDataHandler.Storage.GetAllData();
            var listOfTweetsReceived = data.Select(x => x.TweetsReceived);
            long tweetsReceived = 0;
            foreach (var item in listOfTweetsReceived)
            {
                tweetsReceived += item;
            }
            var startTime = data.FirstOrDefault().StartTime.AddSeconds(data.FirstOrDefault().StartSeconds);
            var endTime = data.LastOrDefault().EndTime;
            var timeSpan = endTime - startTime;

            if (TimeName == TimeEnum.Second) {
                var average = (double)tweetsReceived / timeSpan.TotalSeconds;
                return (decimal)average;
            }
            if (TimeName == TimeEnum.Minute) {
                var minutes = Math.Ceiling(timeSpan.TotalMinutes);
                var average = (double)tweetsReceived / minutes;
                return (decimal)average;
            }
            if (TimeName == TimeEnum.Hour)
            {
                var hours = Math.Ceiling(timeSpan.TotalHours);
                var average = (double)tweetsReceived / hours;
                return (decimal)average;
            }
            return 0;
        }
        public async Task<long> GetTotalTweets()
        {
            var data = await TwitterDataHandler.Storage.GetAllData();
            var listOfTweetsReceived = data.Select(x => x.TweetsReceived);
            long tweetsReceived = 0;
            foreach (var item in listOfTweetsReceived)
            {
                tweetsReceived += item;
            }
            return tweetsReceived;
        }

        public async Task<List<Hashtag>> GetTrendingHashtags(DateTime StartTime, int NumberOfTopHashtags = 1)
        {
            var hashList = new List<Hashtag>();
            var data = await TwitterDataHandler.Storage.GetAllData();
            var hashtags = data.Where(x => x.StartTime >= StartTime)?.Select(x => x.Hashtags);
            foreach (var item in hashtags)
            {
                foreach (var em in item)
                {
                    if (hashList.Any(x => x.Name == em.Name))
                    {
                        long num = em.NumberReceived;
                        hashList.FirstOrDefault(x => x.Name == em.Name).NumberReceived += num;
                    }
                    else
                    {
                        hashList.Add(new Hashtag()
                        {
                            Name = em.Name,
                            NumberReceived = em.NumberReceived
                        });
                    }
                }
            };

            return hashList.OrderByDescending(x => x.NumberReceived)?.Take(NumberOfTopHashtags).ToList();
        }
    }
}
