using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Handlers;

namespace TwitterSampleStreamAPI.Managers
{
    public class EmojiManager : IEmojiManager
    {
        public async Task<List<Emoji>> GetCurrentlyTrendingEmojis(int NumberOfTopEmojis = 1) {
            var data = await TwitterDataHandler.Storage.GetCurrentData();
            return data.Emojis?.OrderByDescending(x => x.NumberReceived)?.Take(NumberOfTopEmojis)?.ToList();
        }
        public async Task<List<Emoji>> GetEmojiPastTrends(DateTime StartTime, int NumberOfTopEmojis = 1) {
            var emojiList = new List<Emoji>();
            var data = await TwitterDataHandler.Storage.GetAllData();
            var emojis = data.Where(x => x.StartTime >= StartTime)?.Select(x => x.Emojis); //?.ToList();
            foreach (var item in emojis) {
                foreach (var em in item) {
                    if (emojiList.Any(x => x.Name == em.Name))
                    {
                        long num = em.NumberReceived;
                        emojiList.FirstOrDefault(x => x.Name == em.Name).NumberReceived += num;
                    }
                    else {
                        emojiList.Add(new Emoji() { 
                            Name = em.Name,
                            NumberReceived = em.NumberReceived
                        });
                    }
                }
            };

            return emojiList.OrderByDescending(x => x.NumberReceived)?.Take(NumberOfTopEmojis).ToList();
        }
        public async Task<decimal> GetPercentageOfTweetsWithEmojis()
        {
            var data = await TwitterDataHandler.Storage.GetAllData();
            var listOfTweetsReceived = data.Select(x => x.TweetsReceived);
            decimal tweetsReceived = 0;
            foreach (var item in listOfTweetsReceived) {
                tweetsReceived += item;
            }

            var listOfEmojisReceived = data.Select(x => x.EmojisReceived);
            decimal emojisReceived = 0;
            foreach (var item in listOfEmojisReceived) { 
                emojisReceived += item;
            }

            var percent = emojisReceived / tweetsReceived;

            return Decimal.Round(percent*100, 2);
        }

    }
}
