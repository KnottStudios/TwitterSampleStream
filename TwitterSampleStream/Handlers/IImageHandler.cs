using System.Collections.Generic;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

namespace TwitterSampleStreamAPI.Handlers
{
    public interface IImageHandler
    {
        public bool TweetHasPicture(ITweet Tweet);
        public bool MediaHasPic(List<IMediaEntity> Media);
        public bool TextHasTwitterPic(string text);
        public bool TextHasInstagramPic(string text);
    }
}
