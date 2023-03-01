using HackerNewsAPIDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackerNewsAPIDemo.Models;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;

namespace HackerNewsAPIDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Default300")]
    public class HackerNewsAPIController : ControllerBase
    {
        private readonly ILogger<HackerNewsAPIController> _logger;
        private readonly IHackerNews _hackerNewsAPI;

        public HackerNewsAPIController(IHackerNews hackerNewsAPI, ILogger<HackerNewsAPIController> logger)
        {
            _hackerNewsAPI = hackerNewsAPI;
            _logger = logger;

        }

        /// <summary>
        /// Gets the list of Top News. It returns id's of top 500 stories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/news/top")]
        [SwaggerOperation(Summary = "Get List of Top News")]
        public async Task<IActionResult> GetTopNews()
        {
            List<int> newsList = new List<int> { 0 };

            newsList = await _hackerNewsAPI.GetTopNews();
            return Ok(newsList);
        }

        /// <summary>
        /// Gets the detail of Top News. Top news call normally has 500 stories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/news/top/detail")]
        [SwaggerOperation(Summary = "Get List of Story Details of Top News")]
        public async Task<IActionResult> GetStoryList()
        {
            List<HackerNewsDetailModel> newsList = new List<HackerNewsDetailModel>();
            List<int> topNewsList = await _hackerNewsAPI.GetTopNews();

            if(topNewsList?.Count > 0)
            {
                newsList = await _hackerNewsAPI.GetNewsDetail(topNewsList);
                return Ok(newsList);
            }
            return BadRequest();
            
        }
        /// <summary>
        /// An extension of GetStoryList. It returns the list of stories containing only URL.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get List of Story Details of Top News and has related URL")]
        public async Task<IActionResult> GetStoryListWithURLOnly()
        {
            List<HackerNewsDetailModel> newsList = new List<HackerNewsDetailModel>();
            List<int> topNewsList = await _hackerNewsAPI.GetTopNews();

            if (topNewsList?.Count > 0)
            {
                newsList = await _hackerNewsAPI.GetNewsDetailWithURL(topNewsList);
                return Ok(newsList);
            }
            return BadRequest();

        }
        /// <summary>
        /// Gets details of individual stories.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Get Story Details by id")]
        public async Task<IActionResult> GetStorybyId(int id)
        {
            HackerNewsDetailModel newsList;
            if(id <= 0)
            {
                return BadRequest();
            }
            newsList = await _hackerNewsAPI.GetNewsbyId(id);
            return Ok(newsList);
        }
    }
}
