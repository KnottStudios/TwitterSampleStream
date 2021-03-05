using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterSampleStreamAPI.Models;

namespace TwitterSampleStreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterReportController : ControllerBase
    {
        public TwitterReportController()
        {

        }
        /// <summary>
        /// Gets the Total Number of Tweets received while app has been running.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<int> GetTotalNumberOfTweets() {
            return 0;
        }
        /// <summary>
        /// Gets the average number of tweets while app has been running.  Defaults to Minute.
        /// </summary>
        /// <param name="TimeName">Hour, Minute, or Second</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<int> GetAverageTweets(TimeEnum TimeName = TimeEnum.Minute) {
            return 0;
        }
        /// <summary>
        /// Gets the Top recent Emojis.
        /// </summary>
        /// <param name="TimeName">Hour, Minute, or Second, defaults To Minute</param>
        /// <param name="NumberOfTopEmojis">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<TwitterStatistic>> GetTrendingEmojis(TimeEnum TimeName = TimeEnum.Minute, int NumberOfTopEmojis = 1) {
            return new List<TwitterStatistic>();
        }
        /// <summary>
        /// Gets the percentage of tweets with emojis while app has been running.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithEmojis()
        {
            return 100;
        }
        /// <summary>
        /// Gets the trending hashtags over a set period of time.
        /// </summary>
        /// <param name="TimeName">Hour, Minute, or Second, defaults To Minute</param>
        /// <param name="NumberOfTopEmojis">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<TwitterStatistic>> GetTrendingHashtags(TimeEnum TimeName = TimeEnum.Minute, int NumberofTopHashtags = 1) {
            return new List<TwitterStatistic>();
        }
        /// <summary>
        /// Gets the Percent of Tweets with a URL while app has been running.  
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithURL() {
            return 100;
        }
        /// <summary>
        /// Gets the Percent of Tweets with a PhotoURL while app has been running.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithPhotoUrl() {
            return 100;
        }
        /// <summary>
        /// Gets the Top Domains over a set period of time.
        /// </summary>
        /// <param name="TimeName">Hour, Minute, or Second, defaults To Minute</param>
        /// <param name="NumberOfTopDomains">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<TwitterStatistic>> GetTrendingDomains(TimeEnum TimeName = TimeEnum.Minute, int NumberofTopDomains = 1) {
            return new List<TwitterStatistic>();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<string> GetTime() {
            return MyTestTimer.GetTime();
        }

    }
}
