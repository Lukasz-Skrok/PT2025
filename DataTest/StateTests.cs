using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Moq;

namespace DataTest
{
    [TestClass]
    public class StateTests
    {
        private Mock<IDataRepository> _mockRepository;
        private State_class _state;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository>();
            _state = new State_class(_mockRepository.Object);
        }

        [TestMethod]
        public void CheckStock_ProductExists_ReturnsTrue()
        {
            // Arrange
            var inventory = new Dictionary<string, (int quantity, double price)>
            {
                { "TestProduct", (10, 5.0) }
            };
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            var result = _state.CheckStock("TestProduct", 5);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckStock_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var inventory = new Dictionary<string, (int quantity, double price)>();
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            var result = _state.CheckStock("TestProduct", 5);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Add2State_NewProduct_AddsSuccessfully()
        {
            // Arrange
            string product = "NewProduct";
            int amount = 10;
            double price = 5.0;

            // Act
            _state.Add2State(product, amount, price);

            // Assert
            _mockRepository.Verify(r => r.UpdateInventory(product, amount, price), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add2State_ExistingProduct_ThrowsException()
        {
            // Arrange
            string product = "ExistingProduct";
            var inventory = new Dictionary<string, (int quantity, double price)>
            {
                { product, (5, 5.0) }
            };
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            _state.Add2State(product, 10, 5.0);
        }

        [TestMethod]
        public void AddStock_ValidProduct_UpdatesSuccessfully()
        {
            // Arrange
            string product = "TestProduct";
            int amount = 5;
            var inventory = new Dictionary<string, (int quantity, double price)>
            {
                { product, (10, 5.0) }
            };
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            _state.AddStock(product, amount);

            // Assert
            _mockRepository.Verify(r => r.UpdateInventory(product, 15, 5.0), Times.Once);
        }

        [TestMethod]
        public void GetPrice_ValidProduct_ReturnsCorrectPrice()
        {
            // Arrange
            string product = "TestProduct";
            double expectedPrice = 5.0;
            var inventory = new Dictionary<string, (int quantity, double price)>
            {
                { product, (10, expectedPrice) }
            };
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            var result = _state.GetPrice(product);

            // Assert
            Assert.AreEqual(expectedPrice, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPrice_InvalidProduct_ThrowsException()
        {
            // Arrange
            var inventory = new Dictionary<string, (int quantity, double price)>();
            _mockRepository.Setup(r => r.GetAllInventory()).Returns(inventory);

            // Act
            _state.GetPrice("InvalidProduct");
        }
    }
} 