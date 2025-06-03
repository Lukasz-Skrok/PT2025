using LogicLayer;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {
        [TestMethod]
        public void LoginTest()
        {
            var fake = new FakeEvents();
            fake.Add2Users(21, "Supplier");
            var logger = new Login_class(fake);
            char persona = logger.Login(21);
            bool supp = logger.CanSupply(persona);
            bool cust = logger.CanBuy(persona);
            Assert.AreEqual('s', persona);
            Assert.IsTrue(supp);
            Assert.IsTrue(!cust);
        }

        [TestMethod]
        public void BuyTest()
        {
            var fakeState = new FakeState();
            var fakeEvents = new FakeEvents();
            var buyer = new Buy_class(fakeState, fakeEvents);
            fakeEvents.Add2State("Apple", 2);
            fakeEvents.Add2State("Banana", 1);
            fakeEvents.Add2Cat("Apple", (float)2.50);
            fakeEvents.Add2Cat("Banana", (float)3.20);
            bool apel = buyer.Buy("Apple", 1);
            bool bana = buyer.Buy("Banana", 2);
            Assert.IsTrue(apel);
            Assert.IsTrue(bana);
        }

        [TestMethod]
        public void SupplyTest()
        {
            var fakeState = new FakeState();
            var fakeEvents = new FakeEvents();
            var supplier = new Supply_class(fakeState, fakeEvents);
            fakeEvents.Add2State("Apple", 1);
            fakeEvents.Add2State("Television", 0);
            fakeEvents.Add2Cat("Apple", (float)5.00);
            fakeEvents.Add2Cat("Television", (float)100.00);
            bool apel = supplier.Supply("Apple", 5);
            bool tele = supplier.Supply("Television", 1);
            Assert.IsTrue(apel);
            Assert.IsTrue(tele);
        }
    }
}