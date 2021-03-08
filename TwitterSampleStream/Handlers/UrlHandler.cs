using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TwitterSampleStreamAPI.Handlers
{
    public class UrlHandler : IUrlHandler
    {
        public List<string> GetUrlFromText(string text)
        {
            var parser = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<string> domains = new List<string>();
            foreach (Match m in parser.Matches(text))
            {
                Uri myURI = new Uri(m.Value);
                domains.Add(myURI.Host);
            }
            return domains;
        }
    }
}
