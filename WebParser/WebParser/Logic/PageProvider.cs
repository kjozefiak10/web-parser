using HtmlAgilityPack;

namespace WebParser.Logic
{
    public class PageProvider : IPageProvider
    {
        public string GetPageContent(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url).Text;
        }
    }
}
