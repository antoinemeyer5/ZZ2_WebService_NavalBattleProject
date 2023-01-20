using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavalWar.DTO;

namespace NavalWar.Tests
{
    [TestClass]
    public class UnitTest1
    {
        // MAP
        [TestMethod]
        public void TestContructor_Map01()
        {
            // init
            Map m = new("Map01");
            // tests
            Assert.IsNotNull(m);
            Assert.AreEqual(m.Name, "Map01");
            Assert.AreEqual(m.ColumMax, 10);
            Assert.AreEqual(m.LineMax, 10);
        }



        /*[TestMethod]
        public void TestContructor_Map02()
        {
            Map m = new Map("Map02", 15, 23);
            Assert.IsNotNull(m);
        }*/

        // PLAYER
        // todo
    }
}
