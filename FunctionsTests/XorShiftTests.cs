using Microsoft.VisualStudio.TestTools.UnitTesting;
using Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Tests
{
    [TestClass()]
    public class XorShiftTests
    {
        [TestMethod()]
        public void Shift128Test()
        {
            var generated = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                int a = WeakRandom.XorShift128();
                generated.Add(a);
            }
            foreach (int i in generated)
            {
                if (generated.Count(q => q == i) > 2)
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void Shift128Test2()
        {
            var generated = new List<int>();
            for (int i = 0; i < 100000; i++)
            {
                int a = WeakRandom.XorShift128();
                generated.Add(a);
            }
            int count = generated.Count(q => q%2 == 0);
            Assert.IsTrue(count > 49500 && count < 50500);
            count = generated.Count(q => q > Int32.MaxValue/2);
            Assert.IsTrue(count > 49500 && count < 50500);
        }

        [TestMethod]
        public void Shift128ModTest()
        {
            int count = 0;
            for (int i = 0; i < 100000; i++)
            {
                int a = WeakRandom.XorShift128(2);
                if (a == 0) count++;
            }
            Assert.IsTrue(count > 49500 && count < 50500);
        }

        [TestMethod]
        public void Shift128SpeedTest()
        {
            Random r = new Random();
            r.Next();
            WeakRandom.XorShift128();


            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 1000000; i++)
            {
                r.Next();
            }
            long libraryTime = s.ElapsedTicks;
            s.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                WeakRandom.XorShift128();
            }
            long xorTime = s.ElapsedTicks;
            Assert.IsTrue(xorTime < libraryTime);
        }
    }
}