using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterDataBase
{
    public interface ITwitterLocalStorage
    {
        public void SetReceivedTweet(List<string> Emoji = null, List<string> Hashtags = null, List<string> Urls = null, bool HasPicURL = false);
        public void SetReceivedEmoji(List<string> EmojiTexts);
        public void SetReceivedHashTag(string HashtagText);
        public void SetReceivedUrl(string url);
        public void SetReceivedPhotoUrl(bool url = false);
        public void SetArchiveTrigger(int minutes = 1);
        public Task<DataArchive> GetCurrentData();
        public Task<IEnumerable<DataArchive>> GetArchivedData(DateTime StartDate, DateTime EndDate);
        public Task<IEnumerable<DataArchive>> GetAllData();

        public bool ArchiveData();
    }
}
