using Tweetinvi.Events;

namespace TwitterLibrary
{
    public interface ITwitterDataHandler
    {
        public void HandleTweet(object sender, TweetReceivedEventArgs eventArgs);

    }
}
