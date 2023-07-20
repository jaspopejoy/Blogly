namespace Blogly.Services
{
    public interface ISlugService
    {
        string UrlFriendly(string Title);
        bool IsUnique(string slug);
    }
}
