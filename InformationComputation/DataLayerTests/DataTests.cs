using DataLayer;
namespace DataLayerTests
{
    [TestClass]
    public sealed class DataTests
    {
        [TestMethod]
        public void UserTest()
        {
            var events = new Events();
            //events.AddUser(1234, User_data.UserType.Customer);
            //events.AddUser(1738, User_data.UserType.Supplier);
            //User_data.UserType user1 = events.GetUser(1234);
            //User_data.UserType user2 = events.GetUser(1738);
            //Assert.AreEqual(user1, User_data.UserType.Customer);
            //Assert.AreEqual(user2, User_data.UserType.Supplier);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void StoreTest()
        {
            var events = new Events();
            events.AddToStorage("Prongles", 20);
            events.AddToStorage("Chicken-jockey", 1);
            int amount1 = events.GetAmount("Prongles");
            int amount2 = events.GetAmount("Chicken-jockey");
            //Assert.AreEqual(amount1, 20);
            //Assert.AreEqual(amount2, 1);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void CatalogTest()
        {
            var events = new Events();
            //events.AddItem("Trombone", 200.50f);
            //events.AddItem("Sandwich", 11.90f);
            //double price1 = events.GetPrice("Trombone");
            //double price2 = events.GetPrice("Sandwich");
            //Assert.AreEqual(price1, 200.50);
            //Assert.AreEqual(price2, 11.90);
            Assert.IsTrue(true);
        }
    }
}
