using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationLayer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PresentationTest
{
    [TestClass]
    public class PresentationLayerTest
    {
        //robione na fakeu tak jak logic z data
        private FakeService _fakeService;
        private UsersViewModel _usersViewModel;
        private CatalogViewModel _catalogViewModel;
        private StateViewModel _stateViewModel;

        //ViewModel tested separately jak w wymaganiach

        [TestInitialize]
        public void Setup()
        {
            _fakeService = new FakeService();
            _usersViewModel = new UsersViewModel(_fakeService);
            _catalogViewModel = new CatalogViewModel(_fakeService);
            _stateViewModel = new StateViewModel(_fakeService);
        }

        [TestMethod]
        public void UsersViewModelTests()
        {
            _fakeService.AddUser(1, "Admin");
            _fakeService.AddUser(2, "User");

            _usersViewModel.RefreshUsers();

            Assert.AreEqual(2, _usersViewModel.Users.Count);
            Assert.IsTrue(_usersViewModel.Users.Any(u => u.id == 1 && u.type == "Admin"));
            Assert.IsTrue(_usersViewModel.Users.Any(u => u.id == 2 && u.type == "User"));
        }

        [TestMethod]
        public void CatalogViewModelTests()
        {
            _fakeService.AddProduct("Product1", 10.0f);
            _fakeService.AddProduct("Product2", 20.0f);

            _catalogViewModel.RefreshProducts();

            Assert.AreEqual(2, _catalogViewModel.Products.Count);
            Assert.IsTrue(_catalogViewModel.Products.Any(p => p.name == "Product1" && p.price == 10.0f));
            Assert.IsTrue(_catalogViewModel.Products.Any(p => p.name == "Product2" && p.price == 20.0f));
        }

        [TestMethod]
        public void StateViewModelTests()
        {
            _fakeService.AddToState("Product1", 5);
            _fakeService.AddToState("Product2", 10);

            _stateViewModel.RefreshState();

            Assert.AreEqual(2, _stateViewModel.Inventory.Count);
            Assert.IsTrue(_stateViewModel.Inventory.Any(s => s.product == "Product1" && s.amount == 5));
            Assert.IsTrue(_stateViewModel.Inventory.Any(s => s.product == "Product2" && s.amount == 10));
        }
    }
}
