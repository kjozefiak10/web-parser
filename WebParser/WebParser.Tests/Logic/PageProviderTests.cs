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
    public class PageProviderTests
    {
        private PageProvider _pageProvider;
        private string _expectedContent = "sampleText";

        [TestInitialize]
        public void TestInit()
        {
            IHtmlWebWrapper htmlWeb = Substitute.For<IHtmlWebWrapper>();
            htmlWeb.Load(Arg.Any<Uri>()).Returns(_expectedContent);
            _pageProvider = new PageProvider(htmlWeb);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPageContent_NullUrl_ThrowArgumentNullExcpetion()
        {
            _pageProvider.GetPageContent(null);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("www.wp.pl")]
        [DataRow("wp.pl")]
        [ExpectedException(typeof(UriFormatException))]
        public void GetPageContent_InvalidUrl_ThrowUriFormatException(string url)
        {
            _pageProvider.GetPageContent(url);
        }

        [TestMethod]
        [DataRow("http://www.wp.pl")]
        [DataRow("http://wp.pl")]
        public void GetPageContent_ValidUrl_ReturnContent(string url)
        {
            string content = _pageProvider.GetPageContent(url);


            content.Should().Be(_expectedContent);
        }
    }
}
