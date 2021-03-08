using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Exceptions;
using Tweetinvi.Streaming;
using TwitterDataBase;

namespace TwitterLibrary
{
    public class TwitterEngine : ITwitterEngine
    {
        private TwitterClient _userClient { get; set; }

        private ISampleStream _sampleStream { get; set; }
        private ITwitterLogger _logger { get; set; }

        private bool _stop { get; set; } = false;


        public TwitterEngine(string ConsumerKey, string ConsumerSecret, string AccessToken, string AccessSecret)
        {
            _userClient = new TwitterClient(ConsumerKey, ConsumerSecret, AccessToken, AccessSecret);
            _sampleStream = _userClient.Streams.CreateSampleStream();
        }

        public string GetStreamState()
        {
            return _sampleStream.StreamState.ToString();
        }

        public bool PauseSampleStream()
        {
            _sampleStream.Pause();
            if (_sampleStream.StreamState != StreamState.Pause) {
                return false;
            }
            return true;
        }

        public bool StopSampleStream()
        {
            _stop = true;
            _sampleStream.Stop();
            if (_sampleStream.StreamState != StreamState.Stop)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This method starts the sample stream and tries to keep it alive even if it dies. 
        /// The stream will Stop on command, but sometimes on start if the stream was already started once, it will throw
        /// an exception because the stream is "already running" even though the stream isn't running.  This can be mitigated with retries.  It might also be mitigated 
        /// by creating a new Stream and that could bear research, but this works.  
        /// </summary>
        /// <param name="TweetsReceivedDelegate"></param>
        /// <returns></returns>
        public async Task<bool> StartSampleStream(Action<object, TweetReceivedEventArgs> TweetsReceivedDelegate, int Retries = 100)
        {
            _stop = false;
            var retryNum = 0;
            _sampleStream.TweetReceived += (sender, eventArgs) => TweetsReceivedDelegate(sender, eventArgs);

            _sampleStream.DisconnectMessageReceived += async (sender, eventArgs) =>
            {
                var reason = eventArgs.DisconnectMessage.Reason;
                var code = eventArgs.DisconnectMessage.Code;
                if (_sampleStream.StreamState != StreamState.Running)
                {
                    await StartSampleStream(TweetsReceivedDelegate);
                }
            };
            while (!_stop)
            {
                retryNum++;
                if (retryNum >= Retries)
                {
                    _stop = true;
                    return false;
                }
                try
                {
                    await _sampleStream.StartAsync();
                }
                catch (TwitterException ex)
                {
                    _logger.Log(ex.Message, ex);
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message, ex);
                }
            }
            return true;
        }
        public async Task RestartSampleStream() {
            try
            {
                _sampleStream.Resume();
            }
            catch (TwitterException ex)
            {
                _logger.Log(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, ex);
            }
        }
    }
}
