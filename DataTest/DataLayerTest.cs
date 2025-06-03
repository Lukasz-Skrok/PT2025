using DataLayer;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {
        [TestMethod]
        public void TestUserIdentification()
        {
            var events = new Events_class();
            events.Add2Users(21, "Supplier");
            events.Add2Users(38, "Customer");
            string user21 = events.FindUser(21);
            string user38 = events.FindUser(38);
            Assert.AreEqual(user21, "Supplier");
            Assert.AreEqual(user38, "Customer");
        }

        [TestMethod]
        public void TestCatalog()
        {
            var events = new Events_class();
            events.Add2Cat("Apple", (float)2.50);
            events.Add2Cat("Banana", (float)3.40);
            float applemoney = events.GetPrice("Apple");
            float bananamoney = events.GetPrice("Banana");
            Assert.AreEqual(applemoney, 2.5, 2);
            Assert.AreEqual(bananamoney, 3.4, 2);
        }

        [TestMethod]
        public void TestCurrentState()
        {
            var events = new Events_class();
            events.Add2State("Apple", 5);
            events.Add2State("Banana", 8);
            events.AddMoney((float)98.0);
            events.AddStock("Apple", 2);
            events.AddStock("Banana", 2);
            bool applehave = events.CheckStock("Apple", 6);
            bool bananahave = events.CheckStock("Banana", 9);
            bool rich = events.CheckMoney((float)69.0);
            Assert.IsTrue(applehave);
            Assert.IsTrue(bananahave);
            Assert.IsTrue(rich);
        }
    }
}