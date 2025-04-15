using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public sealed class LogicTests
    {
        [TestMethod]
        public void Shipment_Successful_WhenItemExists()
        {
            var temp = new TestEvents();
            var shipment = new Shipment_logic(temp);
            temp.AddItem("Ball", 2.0f);
            bool result = shipment.Shipment("Ball", 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Purchase_Fails_WhenStockIsInsufficient()
        {
            var temp = new TestEvents();
            var buyer = new Purchase_logic(temp);
            temp.AddToStorage("Ball", 1);
            temp.AddItem("Ball", 2.0f);
            bool result = buyer.Purchase("Ball", 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Shipment_Fail()
        {
            var temp = new TestEvents();
            var shipment = new Shipment_logic(temp);
            temp.AddItem("Ball", 2.0f);
            bool result = shipment.Shipment("Ball", 100);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Shipment_Fail_No_Item()
        {
            var temp = new TestEvents();
            var shipment = new Shipment_logic(temp);
            bool result = shipment.Shipment("Ball", 1);
            Assert.IsFalse(result);
        }
    }
}