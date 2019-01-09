using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class Sqlite_UnitTest
    {
        [TestInitialize]
        public void init()
        {
            string path = @"D:\HoweDesktop\test_sqlite.db";
            Client.Common.StaticInfo.Init(new Client.Common.StaticInfoInitArgs()
            {
                ExternalSQLiteConnStr = path,
            });
        }

        [TestMethod]
        public void TestMethod1()
        {
            var l = Client.Common.StaticInfo.ExternalSQLiteDB.BuBuGao_rQuestionList();
            Assert.AreEqual<int>(5, l.Count);            
        }
    }
}
