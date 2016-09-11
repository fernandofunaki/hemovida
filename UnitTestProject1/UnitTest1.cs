using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<bool> ret = new List<bool>();
            ret.Add(false);
            ret.Add(false);
            bool result = ret.All(i => i == true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<bool> ret = new List<bool>();
            bool result = ret.All(i => i == true);
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<bool> ret = new List<bool>();
            bool result = !ret.All(i => i == false);
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<bool> ret = new List<bool>();
            ret.Add(true);
            ret.Add(true);
            bool result = !ret.Contains(false);
        }
        [TestMethod]
        public void TestMethod5()
        {
            List<bool> ret = new List<bool>();
            bool result = !ret.Contains(false);
        }
    }
}
