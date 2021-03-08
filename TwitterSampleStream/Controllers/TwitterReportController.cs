using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Managers;
using TwitterSampleStreamAPI.Models;

namespace TwitterSampleStreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterReportController : ControllerBase
    {
        ITweetManager Manager { get; set; }
        public TwitterReportController(ITweetManager manager)
        {
            Manager = manager;
        }
        /// <summary>
        /// Gets the Total Number of Tweets received while app has been running.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<long> GetTotalNumberOfTweets() {
            return await Manager.GetTotalTweets();
        }
        /// <summary>
        /// Gets the average number of tweets while app has been running.  Defaults to Minute.
        /// Each Time level Rounds Up to the nearest whole number.  
        /// </summary>
        /// <param name="TimeName">Hour, Minute, or Second</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetAverageTweets(TimeEnum TimeName = TimeEnum.Minute) {
            return await Manager.GetAverageTweets(TimeName);
        }
        /// <summary>
        /// Gets the trending hashtags over a set period of time.  
        /// </summary>
        /// <param name="StartTime">Defaults to an Hour ago</param>
        /// <param name="NumberofTopHashtags">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<Hashtag>> GetTrendingHashtags(DateTime? StartTime = null, int NumberofTopHashtags = 1) {
            return await Manager.GetTrendingHashtags(StartTime ?? DateTime.Now.AddHours(-1), NumberofTopHashtags); 
        }
    }
}
