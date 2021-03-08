using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterDataBase;

namespace TwitterSampleStreamAPI.Managers
{
    public interface IUrlManager
    {
        public Task<decimal> GetPercentageOfTweetsWithUrls();
        public Task<decimal> GetPercentageOfTweetsWithPhotoUrls();
        public Task<List<Domain>> GetDomainTrends(DateTime StartTime, int NumberOfTopDomains = 1);
    }
}
