using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDataBase;

namespace TwitterSampleStreamAPI.Database
{
    public class TwitterLocalStorage : DataArchive, ITwitterLocalStorage
    {
        private long _archiveTimeTrigger; 
        public List<DataArchive> Archives;
        public TwitterLocalStorage()
        {
            SetArchiveTrigger(1);
            Archives = new List<DataArchive>();
            SetProperties();
        }
        private void SetProperties()
        {
            DateTime dt = DateTime.Now;
            StartTime = dt.Date + new TimeSpan(dt.TimeOfDay.Hours, dt.TimeOfDay.Minutes, 0);
            StartSeconds = dt.Second;
            Emojis = new List<Emoji>();
            Hashtags = new List<Hashtag>();
            Domains = new List<Domain>();
            TweetsReceived = 0;
            EmojisReceived = 0;
            HashTagsReceived = 0;
            UrlsReceived = 0;
            PhotoUrlsReceived = 0;
        }
        /// <summary>
        /// TBH, the await probably doesn't do anything at all here...
        /// </summary>
        /// <returns></returns>
        public async Task<DataArchive> GetCurrentData() {
            DataArchive record = new DataArchive();
            var t = Task.Run(() => { 
                record = new DataArchive()
                {
                    TweetsReceived = TweetsReceived,
                    EmojisReceived = EmojisReceived,
                    Emojis = Emojis,
                    Hashtags = Hashtags,
                    UrlsReceived = UrlsReceived,
                    PhotoUrlsReceived = PhotoUrlsReceived,
                    Domains = Domains,
                    StartTime = StartTime,
                    StartSeconds = StartSeconds,
                    EndTime = DateTime.Now
                };
            });
            await t;
            return record;
        }
        /// <summary>
        /// TBH, the await probably doesn't do anything at all here...
        /// Gets Data older than the Archival Time Frame.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataArchive>> GetArchivedData(DateTime StartDate, DateTime EndDate) {
            var records = new List<DataArchive>();
            var t = Task.Run(() =>
            {
                var startTime = StartDate.Date + new TimeSpan(StartDate.TimeOfDay.Hours, StartDate.TimeOfDay.Minutes, 0);
                var endTime = EndDate.Date + new TimeSpan(EndDate.TimeOfDay.Hours, EndDate.TimeOfDay.Minutes, 0);
                if (!Archives.Any(x => x.EndTime <= EndDate && x.StartTime >= StartDate))
                {
                    records = Archives.Where(x => x.EndTime <= EndDate && x.StartTime >= StartDate)?.ToList();
                }
            });
            await t;
            return records;
        }
        /// <summary>
        /// TBH, the await probably doesn't do anything at all here...
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DataArchive>> GetAllData() {
            var dataList = new List<DataArchive>();
            var currentData = await GetCurrentData();
            var t = Task.Run(() => { 
                dataList.AddRange(Archives);
                dataList.Add(currentData);
            });
            await t;
            return dataList;
        }

        public void SetReceivedTweet(List<string> Emoji = null, List<string> Hashtags = null, List<string> Urls = null, bool HasPicURL = false) {
            if (StartTime <= DateTime.Now.AddMinutes(-_archiveTimeTrigger))
            {
                ArchiveData();
                SetProperties();
            }
            TweetsReceived++;
            SetReceivedEmoji(Emoji);
            foreach (var hash in Hashtags)
            {
                SetReceivedHashTag(hash);
            }
            foreach (var url in Urls)
            {
                SetReceivedUrl(url);
            }
            SetReceivedPhotoUrl(HasPicURL);
        }
        public void SetReceivedEmoji(List<string> EmojiTexts) {
            if (EmojiTexts == null || !EmojiTexts.Any()) {
                return;
            }
            foreach (var emojiText in EmojiTexts)
            {
                EmojisReceived++;
                if (Emojis.Any() && Emojis.FirstOrDefault(x => x.Name == emojiText) != null)
                {
                    Emojis.FirstOrDefault(x => x.Name.Contains(emojiText)).NumberReceived++;
                }
                else
                {
                    Emojis.Add(new Emoji() { Name = emojiText, NumberReceived = 1 });
                }
            }
        }
        public void SetReceivedHashTag(string HashtagText)
        {
            if (string.IsNullOrWhiteSpace(HashtagText))
            {
                return;
            }
            HashTagsReceived++;
            if (Hashtags.Any() && Hashtags.FirstOrDefault(x => x.Name == HashtagText) != null)
            {
                Hashtags.FirstOrDefault(x => x.Name == HashtagText).NumberReceived++;
            }
            else
            {
                Hashtags.Add(new Hashtag() { Name = HashtagText, NumberReceived = 1 });
            }
        }
        public void SetReceivedUrl(string url) {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            UrlsReceived++;
            if (Domains.Any() && Domains.FirstOrDefault(x => x.Name == url) != null)
            {
                Domains.FirstOrDefault(x => x.Name == url).NumberReceived++;
            }
            else
            {
                Domains.Add(new Domain() { Name = url, NumberReceived = 1 });
            }
        }
        public void SetReceivedPhotoUrl(bool url = false)
        {
            if (url)
            {
                PhotoUrlsReceived++;
            }
        }

        /// <summary>
        /// Set a time trigger to initiate an archive.
        /// </summary>
        /// <param name="ArchiveNumber"></param>
        public void SetArchiveTrigger(int minutes = 1) {
            if (minutes < 1) {
                minutes = 1;
            }
            _archiveTimeTrigger = minutes;
        }
        /// <summary>
        /// Archive the data off local storage.
        /// </summary>
        /// <returns></returns>
        public bool ArchiveData() {
            var dataToArchive = this;
            Archives.Add(new DataArchive() { 
                TweetsReceived = dataToArchive.TweetsReceived,
                EmojisReceived = dataToArchive.EmojisReceived,
                Emojis = dataToArchive.Emojis,
                Hashtags = dataToArchive.Hashtags,
                UrlsReceived = dataToArchive.UrlsReceived,
                PhotoUrlsReceived = dataToArchive.PhotoUrlsReceived,
                Domains = dataToArchive.Domains,
                StartTime = dataToArchive.StartTime,
                StartSeconds = dataToArchive.StartSeconds,
                EndTime = DateTime.Now
            });
            return true;
        }
    }
}
