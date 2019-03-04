using HtmlAgilityPack;
using System;

namespace WebParser.Logic
{
    public class HtmlWebWrapper : IHtmlWebWrapper
    {
        public string Load(Uri uri)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            return htmlWeb.Load(uri).Text;
        }

        public string SelectNode(string pageContent, string xPath)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);
            return doc.DocumentNode.SelectSingleNode(xPath).InnerText;
        }

        public string SelectNodeAttribute(string pageContent, string xPath, string attribute)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);
            return doc.DocumentNode.SelectSingleNode(xPath)?.Attributes[attribute]?.Value;
        }
    }
}
