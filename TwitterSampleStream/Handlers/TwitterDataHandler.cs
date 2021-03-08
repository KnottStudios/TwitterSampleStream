using System.Collections.Generic;
using Tweetinvi.Events;
using TwitterDataBase;
using TwitterLibrary;

namespace TwitterSampleStreamAPI.Handlers
{
    public class TwitterDataHandler : ITwitterDataHandler
    {
        public static ITwitterLocalStorage Storage { get; set; }
        public IImageHandler ImageHandler { get; set; }
        public IEmojiHandler EmojiHandler {get; set;}

        public IUrlHandler UrlHandler { get; set; }
        /**/
        public TwitterDataHandler(IImageHandler imageHandler, IUrlHandler urlHandler, IEmojiHandler emojiHandler, ITwitterLocalStorage twitterStorage)
        {
            ImageHandler = imageHandler;
            UrlHandler = urlHandler;
            EmojiHandler = emojiHandler;
            Storage = twitterStorage;
        }
        public TwitterDataHandler(IImageHandler imageHandler, IUrlHandler urlHandler, IEmojiHandler emojiHandler)
        {
            ImageHandler = imageHandler;
            UrlHandler = urlHandler;
            EmojiHandler = emojiHandler;
        }



        public void HandleTweet(object sender, TweetReceivedEventArgs eventArgs) 
        {
            List<string> Emojis = EmojiHandler.GetEmojisFromText(eventArgs.Tweet.FullText);         

            var hashList = new List<string>();
            foreach (var hashtag in eventArgs.Tweet.Hashtags) {
                hashList.Add(hashtag.Text);
            }

            bool HasPic = ImageHandler.TweetHasPicture(eventArgs.Tweet);

            var domains = UrlHandler.GetUrlFromText(eventArgs.Tweet.FullText);

            Storage.SetReceivedTweet(Emojis, hashList, domains, HasPic);  
        }



    }
}
