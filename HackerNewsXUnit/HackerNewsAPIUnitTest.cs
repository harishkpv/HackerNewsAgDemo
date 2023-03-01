using HackerNewsAPIDemo.Controllers;
using HackerNewsAPIDemo.Models;
using HackerNewsAPIDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;

namespace HackerNewsXUnit
{
    public class HackerNewsAPIUnitTest
    {

        [Fact]
        public async Task TestTopNews()
        {
            var logger = NullLogger<HackerNewsAPIController>.Instance;

            IHackerNews hackernews = new HackerNews();
            var controller = new HackerNewsAPIController(hackernews, logger);
            IActionResult result = await controller.GetTopNews();
            
            var okObjectResult = result as OkObjectResult;
            
            Assert.NotNull(okObjectResult);

            var count = ((List<int>)okObjectResult.Value).Count;
            Assert.Equal(500, count);

            var statuscode = okObjectResult.StatusCode;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)statuscode);

        }


        [Fact]
        public async Task TestGetStoryList()
        {
            var logger = NullLogger<HackerNewsAPIController>.Instance;

            IHackerNews hackernews = new HackerNews();
            var controller = new HackerNewsAPIController(hackernews, logger);

            IActionResult result = await controller.GetStoryList();

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            var statuscode = okObjectResult.StatusCode;
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)statuscode);
        }

        [Fact]
        public async Task TestGetStoryListWithURLOnly()
        {
            var logger = NullLogger<HackerNewsAPIController>.Instance;

            IHackerNews hackernews = new HackerNews();
            var controller = new HackerNewsAPIController(hackernews, logger);

            IActionResult result = await controller.GetStoryListWithURLOnly();

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            var statuscode = okObjectResult.StatusCode;
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)statuscode);
        }

            [Theory]
        [InlineData (1)]
        public async Task TestGetNewsByPositiveId(int id)
        {
            var logger = NullLogger<HackerNewsAPIController>.Instance;
            
            IHackerNews hackernews = new HackerNews();
            var controller = new HackerNewsAPIController(hackernews, logger);

            IActionResult result =  await controller.GetStorybyId(id);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            var statuscode = okObjectResult.StatusCode;
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)statuscode);

            HackerNewsDetailModel hackerNewsDetailModel = (HackerNewsDetailModel)okObjectResult.Value;

            Assert.Equal(id, hackerNewsDetailModel.id);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task TestGetNewsByNegativeId(int id)
        {
            var logger = NullLogger<HackerNewsAPIController>.Instance;

            IHackerNews hackernews = new HackerNews();
            var controller = new HackerNewsAPIController(hackernews, logger);

            IActionResult result = await controller.GetStorybyId(id);
            var objectResult = result as BadRequestResult;

            Assert.NotNull(objectResult);
            var statuscode = objectResult.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)statuscode);

        }

    }
}