using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Tweetinvi.Events;
using TwitterLibrary;

namespace TwitterEngineTests
{
    [TestClass]
    public class TwitterEngineTests
    {
        int i = 0;
        ITwitterEngine engine { get; set; }

        /// <summary>
        /// While normally, we would not want to do all the tests in one statement, we are limited in the number of connections we can make to Twitter.
        /// This is also a live test.  Furthermore, Start is an infinite loop, so 
        /// </summary>
        [TestMethod]
        public async Task TwitterEngineAcceptsStopRestartStartPauseStatusCommands()
        {
            var config = AddConfigFiles();
            engine = new TwitterEngine(config["ConsumerKey"], config["ConsumerSecret"], config["AccessToken"], config["AccessSecret"]);
            var engineStarted = await engine.StartSampleStream(IteratingTweetsPauseAndStopWorks);
            Assert.IsTrue(engineStarted);
        }

        private void IteratingTweetsPauseAndStopWorks(object sender, TweetReceivedEventArgs eventArgs)
        {
            i++;
            var data = eventArgs.Json;
            Assert.IsNotNull(data);
            Assert.IsTrue(i > 0);
            if (i == 5)
            {
                engine.PauseSampleStream();
                var streamState = engine.GetStreamState();
                Assert.AreEqual("Pause", streamState);
                engine.RestartSampleStream();
            }
            if (i > 5)
            {
                var streamState = engine.GetStreamState();
                Assert.AreEqual("Running", streamState);
                engine.StopSampleStream();
                streamState = engine.GetStreamState();
                Assert.AreEqual("Stop", streamState);
            }
        }
        private IConfigurationRoot AddConfigFiles() 
        {
            return new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
        }
    }
}
