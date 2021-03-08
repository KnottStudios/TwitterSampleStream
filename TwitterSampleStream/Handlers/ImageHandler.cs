using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

namespace TwitterSampleStreamAPI.Handlers
{
    public class ImageHandler : IImageHandler
    {
        public bool TweetHasPicture(ITweet Tweet) 
        {
            bool HasPic = (TextHasTwitterPic(Tweet.FullText)) ? true : TextHasInstagramPic(Tweet.FullText);
            return (HasPic) ? true : MediaHasPic(Tweet.Media);
        }

        public bool MediaHasPic(List<IMediaEntity> Media)
        {
            if (Media.Count > 1 && Media.Any(x => x.MediaType == "photo"))
            {
                return true;
            }
            return false;
        }

        public bool TextHasTwitterPic(string text)
        {
            if (text.Contains("pic.twitter.com"))
            {
                return true;
            }
            return false;
        }
        public bool TextHasInstagramPic(string text)
        {
            if (text.Contains("instagram.com/p/"))
            {
                return true;
            }
            return false;
        }
    }
}
