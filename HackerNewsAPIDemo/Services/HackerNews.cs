using HackerNewsAPIDemo.Models;
using Newtonsoft.Json;
using System.Net;

namespace HackerNewsAPIDemo.Services
{
    public class HackerNews : IHackerNews
    {
        private readonly string strHackerNewsBaseURL = "https://hacker-news.firebaseio.com/v0/";
        private static readonly HttpClient httpClient = new HttpClient();
        public HackerNews()
        {
        }

        public async Task<List<int>> GetTopNews()
        {
            List<int> hackerNews = new List<int> { 0 };
            try
            {
                var response = await httpClient.GetAsync($"{strHackerNewsBaseURL}/newstories.json?print=pretty");
                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    hackerNews = JsonConvert.DeserializeObject<List<int>>(responseContent, 
                                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return hackerNews;
        }

        public async Task<List<HackerNewsDetailModel>> GetNewsDetail(List<int> topnewsList)
        {
            if(topnewsList?.Count == 0)
            {
                return null;
            }
            try
            {
                var news = topnewsList.Select(GetNewsbyId);
                List<HackerNewsDetailModel> newsList = (await Task.WhenAll(news)).ToList();
                return newsList;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<List<HackerNewsDetailModel>> GetNewsDetailWithURL(List<int> topnewsList)
        {
            if (topnewsList?.Count == 0)
            {
                return null;
            }
            try
            {
                List<HackerNewsDetailModel> newsList = await GetNewsDetail(topnewsList);
                return newsList.FindAll(x=>(x.url!=string.Empty));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<HackerNewsDetailModel> GetNewsbyId(int id)
        {
            HackerNewsDetailModel newsbyid = new HackerNewsDetailModel();

            if (id < 0)
            {
                return newsbyid;
            }
            try
            {
                var responseById = await httpClient.GetAsync($"{strHackerNewsBaseURL}item/{id}.json?print=pretty");
                if(responseById?.StatusCode == HttpStatusCode.OK)
                {
                    var responseByIdContent = responseById.Content.ReadAsStringAsync().Result;
                    newsbyid = JsonConvert.DeserializeObject<HackerNewsDetailModel>(responseByIdContent);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return newsbyid;
        }
    }
}
