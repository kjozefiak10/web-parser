using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebParser.Logic
{
    /// <summary>
    /// Keywords are searchend only from body!
    /// </summary>
    public class KeywordsFinder : IKeywordsFinder
    {
        private const string MetaXPath = "//meta[@name='keywords']";
        private const string BodyXPath = "//body";
        private const string ContentAttribute = "content";

        private readonly IHtmlWebWrapper _htmlWeb;

        public KeywordsFinder()
            :this(new HtmlWebWrapper())
        { }

        public KeywordsFinder(IHtmlWebWrapper htmlWeb)
        {
            _htmlWeb = htmlWeb ?? throw new ArgumentNullException(nameof(htmlWeb));
        }

        public IDictionary<string, int> FindKeywords(string pageContent)
        {
            if (string.IsNullOrEmpty(pageContent))
                return new Dictionary<string, int>();

            string keywordsContent = _htmlWeb.SelectNodeAttribute(pageContent, MetaXPath, ContentAttribute);
            if (string.IsNullOrEmpty(keywordsContent))
                return new Dictionary<string, int>();

            IEnumerable<string> keywords = keywordsContent.Split(',')
                .Select(keyword => keyword[0] == ' ' ? keyword.Remove(0, 1) : keyword)
                .Distinct();

            string body = _htmlWeb.SelectNode(pageContent, BodyXPath);
            if (string.IsNullOrEmpty(body))
                return new Dictionary<string, int>();

            return keywords
                .Select(key => new { Key = key, Regex.Matches(body, key, RegexOptions.IgnoreCase | RegexOptions.Compiled).Count })
                .OrderByDescending(key => key.Count)
                .ToDictionary(k => k.Key, k => k.Count);
        }
    }
}
