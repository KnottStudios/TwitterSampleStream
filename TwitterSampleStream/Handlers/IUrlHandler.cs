using System.Collections.Generic;

namespace TwitterSampleStreamAPI.Handlers
{
    public interface IUrlHandler
    {
        public List<string> GetUrlFromText(string text);
    }
}
