using System;

namespace WebParser.Logic
{
    public interface IHtmlWebWrapper
    {
        string Load(Uri uri);
        string SelectNode(string pageContent, string xPath);
        string SelectNodeAttribute(string pageContent, string xPath, string attribute);
    }
}