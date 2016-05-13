﻿using System;
using System.IO;
using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CozyRSS.Syndication.Tester.Parser {

    [TestClass()]
    public class RssFormatterTests {

        private RssFormatter formatter;

        [TestInitialize]
        public void Init() {
            formatter = new RssFormatter();
        }

        [TestMethod()]
        public void ParseItemTest()
        {
            var result = formatter.Formatter("http://www.peise.net/rss.php?rssid=32");
            Assert.IsTrue(result.items.Count > 0);
        }
    }
}