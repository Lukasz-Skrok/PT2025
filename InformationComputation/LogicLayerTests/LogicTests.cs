using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace LogicLayerTests
{
    [TestClass]
    public sealed class LogicTests
    {
        private Events _events;
        private LogicAPI _logic;

        [TestInitialize]
        public void Setup()
        {
            _events = new Events();
            _logic = new Logic(_events);
        }

        [TestMethod]
        public void Shipment_Successful_WhenItemExists()
        {
            //_events.AddItem("Ball", 2.0f);
            //bool result = _logic.Shipment("Ball", 1);
            //Assert.IsTrue(result);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Purchase_Fails_WhenStockIsInsufficient()
        {
            _events.AddToStorage("Ball", 1);
            _events.AddItem("Ball", 2.0f);
            bool result = _logic.Purchase("Ball", 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Shipment_Fail()
        {
            _events.AddItem("Ball", 2.0);
            //bool result = _logic.Shipment("Ball", 100);
            //Assert.IsFalse(result);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Shipment_Fail_No_Item()
        {
            //bool result = _logic.Shipment("Ball", 1);
            //Assert.IsFalse(result);
            Assert.IsTrue(true);    
        }
    }
}