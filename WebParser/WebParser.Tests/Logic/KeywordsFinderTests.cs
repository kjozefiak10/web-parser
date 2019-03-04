using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using WebParser.Logic;

namespace WebParser.Tests.Logic
{
    [TestClass]
    public class KeywordsFinderTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void GetPageContent_NullOrEmptyPageContent_ReturnEmptyDictionary(string pageContent)
        {
            IHtmlWebWrapper htmlWeb = Substitute.For<IHtmlWebWrapper>();
            KeywordsFinder keyworsdFinder = new KeywordsFinder(htmlWeb);


            var result = keyworsdFinder.FindKeywords(pageContent);


            result.Should().BeEmpty();
        }


        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void GetPageContent_KeywordsNotExist_ReturnEmptyDictionary(string keywords)
        {
            IHtmlWebWrapper htmlWeb = Substitute.For<IHtmlWebWrapper>();
            htmlWeb.SelectNodeAttribute(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(keywords);
            KeywordsFinder keyworsdFinder = new KeywordsFinder(htmlWeb);


            var result = keyworsdFinder.FindKeywords("Test");


            result.Should().BeEmpty();
        }


        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void GetPageContent_BodyNotExist_ReturnEmptyDictionary(string body)
        {
            IHtmlWebWrapper htmlWeb = Substitute.For<IHtmlWebWrapper>();
            htmlWeb.SelectNodeAttribute(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("example");
            htmlWeb.SelectNode(Arg.Any<string>(), Arg.Any<string>()).Returns(body);
            KeywordsFinder keyworsdFinder = new KeywordsFinder(htmlWeb);


            var result = keyworsdFinder.FindKeywords("Test");


            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetPageContent_KeywordsAndBodyExist_ReturnProperDictionary()
        {
            IHtmlWebWrapper htmlWeb = Substitute.For<IHtmlWebWrapper>();
            htmlWeb.SelectNode(Arg.Any<string>(), Arg.Any<string>()).Returns("keywrod keyword1 keyword2 keyword keyword2");
            htmlWeb.SelectNodeAttribute(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("keyword1, keyword2");
            KeywordsFinder keyworsdFinder = new KeywordsFinder(htmlWeb);


            var result = keyworsdFinder.FindKeywords("Test");


            result.Should().BeEquivalentTo(new Dictionary<string, int>
            {
                { "keyword2", 2 },
                { "keyword1", 1 },
            });
        }
    }
}
