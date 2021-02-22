using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class JsonFeed : IJsonFeed
    {
        static string _url = "";

        public JsonFeed() { }
        public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }

        /// <summary>
        /// Getting Random Jokes 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string[] GetRandomJokes(string firstname, string lastname, string category)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string url = "jokes/random";
                    if (category != null)
                    {
                        if (url.Contains('?'))
                            url += "&";
                        else url += "?";
                        url += "category=";
                        url += category;
                    }

                    string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

                    if (firstname != null && lastname != null)
                    {
                        int index = joke.IndexOf("Chuck Norris");
                        string firstPart = joke.Substring(0, index);
                        string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                        joke = firstPart + " " + firstname + " " + lastname + secondPart;
                    }

                    return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching Jokes is not working" };
            }
        }

        /// <summary>
        /// Async version of getting Random jokes
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="category"></param>
        /// <param name="noOfJokes"></param>
        /// <returns></returns>
        public async Task<string[]> GetRandomJokesAsync(string firstname, string lastname, string category, int noOfJokes)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string[] jokes = new string[noOfJokes];
                    client.BaseAddress = new Uri(_url);
                    string url = "jokes/random";
                    if (category != null)
                    {
                        if (url.Contains('?'))
                            url += "&";
                        else url += "?";
                        url += "category=";
                        url += category;
                    }

                    for (int i = 1; i <= noOfJokes; i++)
                    {
                        string joke = (JsonConvert.DeserializeObject<dynamic>(await client.GetStringAsync(url).ConfigureAwait(false))).value;

                        if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
                        {
                            joke = joke.Replace("Chuck", firstname).Replace("Norris", lastname);
                        }
                        jokes[i - 1] = joke;
                    }
                    return jokes;
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching Jokes is not working" };
            }
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic Getnames()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    var result = client.GetStringAsync("").Result;
                    return JsonConvert.DeserializeObject<dynamic>(result);
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching names is not working" };
            }
        }

        /// <summary>
        /// Async version of getting Names
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetNamesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    var result = await client.GetStringAsync("").ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<dynamic>(result);
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching names is not working" };
            }
        }

        /// <summary>
        /// Getting categories 
        /// </summary>
        /// <returns></returns>
        public static string[] GetCategories()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string url = "jokes/categories";
                    return new string[] { Task.FromResult(client.GetStringAsync(url).Result).Result };
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching categories is not working" };
            }
        }

        /// <summary>
        /// Async version of getting categories 
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetCategoriesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string url = "jokes/categories";
                    return JsonConvert.DeserializeObject<string[]>(await client.GetStringAsync(url).ConfigureAwait(false));
                }
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, "Please try again after sometime. Fetching categories is not working" };
            }
        }
    }
}
