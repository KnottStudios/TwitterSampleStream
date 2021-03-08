using System;
using System.Threading.Tasks;
using Tweetinvi.Events;

namespace TwitterLibrary
{
    public interface ITwitterEngine
    {
        public Task<bool> StartSampleStream(Action<object, TweetReceivedEventArgs> TweetsReceivedDelegate, int Retries = 100);
        public bool PauseSampleStream();
        public string GetStreamState();
        public bool StopSampleStream();
        public Task RestartSampleStream();
    }
}
