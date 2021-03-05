using System;

namespace TwitterLibrary
{
    public interface ITwitterEngine
    {
        public void StartSampleStream();
        public void GetTweets();
        public void GetTweetCount();
        public void SaveTweets();

    }
}
