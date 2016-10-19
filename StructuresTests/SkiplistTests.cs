using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Tests
{
    [TestClass()]
    public class SkiplistTests
    {
        [TestMethod()]
        public void SkipListAddTest()
        {
            var skiplist = new Skiplist<int>();
            skiplist.Add(5);
            skiplist.Add(50);
            var str = skiplist.ToString();
            skiplist.Add(500);
            skiplist.Add(5000);
            skiplist.Add(51);
            skiplist.Add(501);
            Assert.AreEqual("0 5 50 51 500 501 5000 0 ", skiplist.ToString());
        }
    }
}