using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterSampleStreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmojisController : ControllerBase
    {
        IEmojiManager EmojiManager { get; set; }
        public EmojisController(IEmojiManager manager)
        {
            EmojiManager = manager;
        }
        /// <summary>
        /// Gets the Top recent Emojis.  Resets every Minute on the Minute.
        /// </summary>
        /// <param name="NumberOfTopEmojis">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<Emoji>> GetCurrentlyTrendingEmojis(int NumberOfTopEmojis = 1)
        {
            var trendingEmojis = await EmojiManager.GetCurrentlyTrendingEmojis(NumberOfTopEmojis);
            return trendingEmojis;
        }

        /// <summary>
        /// Gets the Top Emojis over time.
        /// </summary>
        /// <param name="StartTime">Defaults to an Hour ago</param>
        /// <param name="NumberOfTopEmojis">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<Emoji>> GetEmojiTrendsOverTime(DateTime? StartTime = null, int NumberOfTopEmojis = 1)
        {
            var trendingEmojis = await EmojiManager.GetEmojiPastTrends(StartTime ?? DateTime.Now.AddHours(-1), NumberOfTopEmojis);
            return trendingEmojis;
        }

        /// <summary>
        /// Gets the percentage of tweets with emojis while app has been running. 
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithEmojis()
        {
            var percentage = await EmojiManager.GetPercentageOfTweetsWithEmojis();
            return percentage;
        }
    }
}
