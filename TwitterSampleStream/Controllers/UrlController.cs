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
    public class UrlController : ControllerBase
    {
        IUrlManager UrlManager { get; set; }
        public UrlController(IUrlManager manager)
        {
            UrlManager = manager;
        }
        /// <summary>
        /// Gets the Percent of Tweets with a URL while app has been running.  
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithURLs()
        {
            var percentage = await UrlManager.GetPercentageOfTweetsWithUrls();
            return percentage;
        }
        /// <summary>
        /// Gets the Percent of Tweets with a PhotoURL while app has been running.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<decimal> GetPercentOfTweetsWithPhotoUrls()
        {
            var percentage = await UrlManager.GetPercentageOfTweetsWithPhotoUrls();
            return percentage;
        }
        /// <summary>
        /// Gets the Top Domains over a set period of time.
        /// </summary>
        /// <param name="StartTime">Hour, Minute, or Second, defaults To Minute</param>
        /// <param name="NumberOfTopDomains">Defaults to 1.</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<Domain>> GetTrendingDomains(DateTime? StartTime = null, int NumberofTopDomains = 1)
        {
            var trendingDomains = await UrlManager.GetDomainTrends(StartTime ?? DateTime.Now.AddHours(-1), NumberofTopDomains);
            return trendingDomains;
        }
    }
}
