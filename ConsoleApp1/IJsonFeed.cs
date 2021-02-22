using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IJsonFeed
    {
        Task<string[]> GetCategoriesAsync();
        Task<dynamic> GetNamesAsync();
        Task<string[]> GetRandomJokesAsync(string firstname, string lastname, string category, int noOfJokes);
    }
}