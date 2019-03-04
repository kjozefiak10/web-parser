using System.Collections.Generic;

namespace WebParser.Logic
{
    public interface IKeywordsFinder
    {
        IDictionary<string, int> FindKeywords(string pageContent);
    }
}