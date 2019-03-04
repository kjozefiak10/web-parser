using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebParser.Logic;
using WebParser.Models;

namespace WebParser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageProvider _pageProvider;
        private readonly IKeywordsFinder _keywordsFinder;

        public HomeController(IPageProvider pageProvider, IKeywordsFinder keywordsFinder)
        {
            _pageProvider = pageProvider ?? throw new ArgumentNullException(nameof(pageProvider));
            _keywordsFinder = keywordsFinder ?? throw new ArgumentNullException(nameof(keywordsFinder));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UrlViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pageContent = _pageProvider.GetPageContent("http://wp.pl");

                return View("Keywords", new KeywordsViewModel(_keywordsFinder.FindKeywords(pageContent)));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Keywords(KeywordsViewModel model)
        {
            return View(model);
        }
    }
}
