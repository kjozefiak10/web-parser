using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebParser.Logic
{
    public class KeywordsFinder : IKeywordsFinder
    {
        private const string MetaXPath = "//meta[@name='keywords']";
        private const string BodyXPath = "//body";
        private const string ContentAttribute = "content";

        public IDictionary<string, int> FindKeywords(string pageContent)
        {
            if (string.IsNullOrEmpty(pageContent))
                return new Dictionary<string, int>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            HtmlAttribute keywordsContent = doc.DocumentNode.SelectSingleNode(MetaXPath).Attributes[ContentAttribute];

            IEnumerable<string> keywords = keywordsContent.Value.Split(',')
                .Select(keyword => keyword[0] == ' ' ? keyword.Remove(0, 1) : keyword)
                .Distinct();

            string body = doc.DocumentNode.SelectSingleNode(BodyXPath).InnerText;

            return keywords
                .Select(key => new { Key = key, Regex.Match(body, key, RegexOptions.IgnoreCase | RegexOptions.Compiled).Length })
                .ToDictionary(k => k.Key, k => k.Length);
        }
    }
}
