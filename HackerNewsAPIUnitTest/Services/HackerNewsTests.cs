using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsAPIDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNewsAPIDemo.Controllers;
using HackerNewsAPIDemo.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Castle.Core.Logging;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAPIDemo.Services.Tests
{
    [TestClass()]
    public class HackerNewsTests
    {
        [TestMethod()]
        public async void GetNewsbyIdTest()
        {
            
            Mock<IHackerNews> moqHackerNews = new Mock<IHackerNews>();
            
            Mock<ILogger<HackerNewsAPIController>> moqLogger =  new Mock<ILogger<HackerNewsAPIController>>();
            
            //moqHackerNews.Setup(f => f.GetNewsbyId(1)).ReturnsAsync(model);
            var controller = new HackerNewsAPIController(moqHackerNews.Object, moqLogger.Object);

            /*var result = await controller.GetStorybyId(model.id);
            var actualresult = result.;

            Assert.Equals(model.id, ((HackerNewsDetailModel)actualresult).id);*/
           

            IActionResult actionResult = (IActionResult)controller.GetStorybyId(1);
            var contentResult = actionResult as OkNegotiatedContentResult<HackerNewsDetailModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.id);
        }

    }
}