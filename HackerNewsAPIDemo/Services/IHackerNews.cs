using HackerNewsAPIDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsAPIDemo.Services
{
    public interface IHackerNews
    {
        /// <summary>
        /// Get Top News. Returns a list of (500) news id.
        /// </summary>
        /// <returns></returns>
        Task<List<int>> GetTopNews();
        /// <summary>
        /// Get News Detail for top news. 
        /// </summary>
        /// <param name="topnewsList"> List of News ID</param>
        /// <returns></returns>
        Task<List<HackerNewsDetailModel>> GetNewsDetail(List<int> topnewsList);
        /// <summary>
        /// An extension of GetNewsDetails. It filters News based on URL being available.
        /// </summary>
        /// <param name="topnewsList">List of News ID</param>
        /// <returns></returns>
        Task<List<HackerNewsDetailModel>> GetNewsDetailWithURL(List<int> topnewsList);
        /// <summary>
        /// Get Detail for News.
        /// For Model refer to HackerNewsDetailModel
        /// </summary>
        /// <param name="id">News ID</param>
        /// <returns></returns>
        Task<HackerNewsDetailModel> GetNewsbyId(int id);
    }
}
