using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CNController
    {
        private List<string> cacheCategories = new List<string>();

        public List<string> CacheCategories { get => cacheCategories; set => cacheCategories = value; }

        public CNController()
        {
        }

        /// <summary>
        /// Function retruning the list of Categories
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetCategories()
        {
            if (CacheCategories.Count == 0)
            {
                var jsonFeed = new JsonFeed("https://api.chucknorris.io");
                CacheCategories = (await jsonFeed.GetCategoriesAsync().ConfigureAwait(false)).ToList();
            }
            return CacheCategories;
        }

        /// <summary>
        /// Getting random jokes
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="category"></param>
        /// <param name="noOfJokes"></param>
        /// <returns></returns>
        public async Task<string[]> GetRandomJokes(string firstname, string lastname, string category, int noOfJokes)
        {
            var jsonFeed = new JsonFeed("https://api.chucknorris.io");
            return await jsonFeed.GetRandomJokesAsync(firstname, lastname, category, noOfJokes).ConfigureAwait(false);
        }

        /// <summary>
        /// Getting random names 
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<string, string>> GetNames()
        {
            var jsonFeed = new JsonFeed("https://www.names.privserv.com/api/");
            dynamic result = await jsonFeed.GetNamesAsync().ConfigureAwait(false);
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
