using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterSampleStreamAPI.Handlers;

namespace TwitterSampleStreamAPI.Managers
{
    public class UrlManager : IUrlManager
    {
        public async Task<decimal> GetPercentageOfTweetsWithUrls()
        {
            var data = await TwitterDataHandler.Storage.GetAllData();
            var listOfTweetsReceived = data.Select(x => x.TweetsReceived);
            decimal tweetsReceived = 0;
            foreach (var item in listOfTweetsReceived)
            {
                tweetsReceived += item;
            }

            var listOfUrlsReceived = data.Select(x => x.UrlsReceived);
            decimal urlsReceived = 0;
            foreach (var item in listOfUrlsReceived)
            {
                urlsReceived += item;
            }

            var percent = urlsReceived / tweetsReceived;

            return Decimal.Round(percent * 100, 2);
        }
        public async Task<decimal> GetPercentageOfTweetsWithPhotoUrls()
        {
            var data = await TwitterDataHandler.Storage.GetAllData();
            var listOfTweetsReceived = data.Select(x => x.TweetsReceived);
            decimal tweetsReceived = 0;
            foreach (var item in listOfTweetsReceived)
            {
                tweetsReceived += item;
            }

            var listOfPhotoUrlsReceived = data.Select(x => x.PhotoUrlsReceived);
            decimal photoUrlsReceived = 0;
            foreach (var item in listOfPhotoUrlsReceived)
            {
                photoUrlsReceived += item;
            }

            var percent = photoUrlsReceived / tweetsReceived;

            return Decimal.Round(percent * 100, 2);
        }
        public async Task<List<Domain>> GetDomainTrends(DateTime StartTime, int NumberofTopDomains = 1)
        {
            var domainList = new List<Domain>();
            var data = await TwitterDataHandler.Storage.GetAllData();
            var domains = data.Where(x => x.StartTime >= StartTime)?.Select(x => x.Domains);
            foreach (var item in domains)
            {
                foreach (var em in item)
                {
                    if (domainList.Any(x => x.Name == em.Name))
                    {
                        long num = em.NumberReceived;
                        domainList.FirstOrDefault(x => x.Name == em.Name).NumberReceived += num;
                    }
                    else
                    {
                        domainList.Add(new Domain()
                        {
                            Name = em.Name,
                            NumberReceived = em.NumberReceived
                        });
                    }
                }
            };

            return domainList.OrderByDescending(x => x.NumberReceived)?.Take(NumberofTopDomains).ToList();
        }
    }
}
