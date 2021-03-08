using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwitterSampleStream.Resources;

namespace TwitterSampleStreamAPI.Handlers
{
    public class EmojiHandler : IEmojiHandler
    {
        public EmojiJsonList EmojiJsonList { get; set; }
        public List<string> EmojiPicList { get; set; }
        public EmojiHandler(string JsonFileName = @"Resources/emoji.json")
        {
            GetEmojiJson(JsonFileName);
            EmojiPicList = EmojiJsonList.Emojis.Select(x => x.Emoji)?.ToList();
        }
        public void GetEmojiJson(string FileName)
        {
            string json = File.ReadAllText(FileName);
            EmojiJsonList = JsonConvert.DeserializeObject<EmojiJsonList>(json);
        }
        public List<string> GetEmojisFromText(string TweetText) {
            if (string.IsNullOrWhiteSpace(TweetText)) {
                return null;
            }
            var tweetChars = TweetText.ToCharArray().Select(c => c.ToString()).ToList();
            if (!CheckIfTextHasEmoji(tweetChars)) {
                return null;
            };
            List<string> emojis = new List<string>();
            foreach (var item in tweetChars)
            {
                var thisEmoji = EmojiJsonList.Emojis.FirstOrDefault(x => x.Emoji == item.ToString());
                if (thisEmoji != null && !string.IsNullOrWhiteSpace(thisEmoji.Name))
                {
                    emojis.Add(thisEmoji.Name);
                }
            }
            return emojis;
        }

        public bool CheckIfTextHasEmoji(List<string> TweetChars) {
            bool hasMatch = EmojiPicList.Intersect(TweetChars).Any();
            return hasMatch;
        }
    }
}
