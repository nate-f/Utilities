using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Tests
{
    [TestClass()]
    public class LinkedListTests
    {
        private LinkedList<int> list;
        [TestInitialize]
        public void InitializeList()
        {
            list = new LinkedList<int>();
            for (int i = 1; i < 1001; i++)
            {
                list.Add(i);
            }
        }
        [TestMethod()]
        public void LinkedListGetEnumeratorTest()
        {
            int count = 0;
            foreach (var n in list)
            {
                count++;
            }
            Assert.IsTrue(count == 1000);
        }

        [TestMethod()]
        public void LinkedListAddTest()
        {
            list.Add(243);
        }

        [TestMethod()]
        public void LinkedListClearTest()
        {
            list.Clear();
            Assert.IsTrue(list.Count() == 0);
        }

        [TestMethod()]
        public void LinkedListContainsTest()
        {
            Assert.IsTrue(list.Contains(4));
            Assert.IsFalse(list.Contains(-33));
        }

        [TestMethod()]
        public void LinkedListCopyToTest()
        {
            var array = new int[1000];
            list.CopyTo(array, 0);
            foreach (var i in array)
            {
                Assert.IsTrue(i != 0);
            }
        }

        [TestMethod()]
        public void LinkedListRemoveTest()
        {
            list.Remove(64);
            Assert.IsTrue(list.Count() == 999);
            Assert.IsFalse(list.Contains(64));
        }

        [TestMethod()]
        public void LinkedListCountTest()
        {
            Assert.IsTrue(list.Count() == 1000);
        }

        [TestMethod()]
        public void LinkedListIndexOfTest()
        {
            Assert.IsTrue(list.IndexOf(49) == 49);
        }

        [TestMethod()]
        public void LinkedListInsertTest()
        {
            list.Insert(64, 5555);
            Assert.IsTrue(list.Contains(5555));
            Assert.IsTrue(list[64] == 5555);
            list.Insert(0, -1);
            Assert.IsTrue(list[0] == -1);
        }

        [TestMethod()]
        public void LinkedListRemoveAtTest()
        {
            list.RemoveAt(64);
            Assert.IsTrue(list.Count() == 999);
            Assert.IsFalse(list.Contains(936));
        }

        [TestMethod]
        public void LinkedListSpeedTest()
        {
            var s = new Stopwatch();
            
            var library = new List<int>();
            library.Add(2);

            s.Start();
            library = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                library.Add(i);
            }
            int count = 0;
            foreach (var i in library)
            {
                count++;
            }
            long lib = s.ElapsedTicks;

            s.Restart();
            var local = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                local.Add(i);
            }
            count = 0;
            foreach (var i in local)
            {
                count++;
            }
            long loc = s.ElapsedTicks;

            Assert.IsTrue(lib > loc);
        }
    }
}