using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class LinqExtension_UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> t1 = new List<string>();
            t1.Add("1");
            t1.Add("2");
            t1.Add("3");
            t1.Add("4");
            t1.Add("5");

            var t2 = t1.OrderByRandom().ToList();
            Assert.AreNotSame(t1, t2);


            List<Carton> t3 = new List<Carton>();
            t3.Add(new Carton() { ID = 1, Name = "1" });
            t3.Add(new Carton() { ID = 2, Name = "2" });
            t3.Add(new Carton() { ID = 3, Name = "3" });
            t3.Add(new Carton() { ID = 4, Name = "4" });
            t3.Add(new Carton() { ID = 5, Name = "5" });

            var t4 = t3.OrderByRandom().ToList();
            Assert.AreNotSame(t3, t4);
        }
    }


    public class Carton
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}