using System;
using System.Collections.Generic;

namespace TwitterDataBase
{
    public class DataArchive
    {
        public long TweetsReceived { get; set; }
        public long EmojisReceived { get; set; }
        public List<Emoji> Emojis { get; set; }
        public long HashTagsReceived { get; set; }
        public List<Hashtag> Hashtags { get; set; }
        public long UrlsReceived { get; set; }
        public long PhotoUrlsReceived { get; set; }
        public List<Domain> Domains { get; set; }
        public DateTime StartTime { get; set; }
        public int StartSeconds { get; set; }
        public DateTime EndTime { get; set; }

    }
}
