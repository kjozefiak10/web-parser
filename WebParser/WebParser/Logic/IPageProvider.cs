namespace WebParser.Logic
{
    public interface IPageProvider
    {
        string GetPageContent(string url);
    }
}