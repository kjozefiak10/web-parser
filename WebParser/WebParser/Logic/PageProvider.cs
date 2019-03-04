using System;

namespace WebParser.Logic
{
    public class PageProvider : IPageProvider
    {
        private readonly IHtmlWebWrapper _htmlWeb;

        public PageProvider()
            :this(new HtmlWebWrapper())
        { }

        public PageProvider(IHtmlWebWrapper htmlWeb)
        {
            _htmlWeb = htmlWeb ?? throw new ArgumentNullException(nameof(htmlWeb));
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UriFormatException"></exception>
        public string GetPageContent(string url)
        {
            Uri uri = new Uri(url);
            return _htmlWeb.Load(uri);
        }
    }
}
