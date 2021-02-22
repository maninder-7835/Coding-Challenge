using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface ICNController
    {
        Task<List<string>> GetCategories();
        Task<Tuple<string, string>> GetNames();
        Task<string[]> GetRandomJokes(string firstname, string lastname, string category, int noOfJokes);
    }
}