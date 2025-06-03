using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using System.Linq;

namespace DataTest
{
    [TestClass]
    public class DataRepositoryTests
    {
        private const string TestConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataShop.mdf;Integrated Security=True";
        private IDataRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = DataRepository.CreateNewRepository(TestConnectionString);
            _repository.ClearAll();
        }

        [TestMethod]
        public void UserTests()
        {
            _repository.AddUser(1, "Supplier");

            var user = _repository.GetUser(1);
            Assert.IsNotNull(user);
            Assert.AreEqual("Supplier", user.Type);

            var allUsers = _repository.GetAllUsers().ToList();
            Assert.AreEqual(1, allUsers.Count);

            _repository.RemoveUser(1);
            Assert.IsNull(_repository.GetUser(1));
        }

        [TestMethod]
        public void CatalogTests()
        {
            _repository.AddCatalog("Apple", 2.50f);

            var catalog = _repository.GetCatalog("Apple");
            Assert.IsNotNull(catalog);
            Assert.AreEqual(2.50f, catalog.Price);

            var allCatalogs = _repository.GetAllCatalogs().ToList();
            Assert.AreEqual(1, allCatalogs.Count);

            _repository.RemoveCatalog("Apple");
            Assert.IsNull(_repository.GetCatalog("Apple"));
        }

        [TestMethod]
        public void StateTests()
        {
            _repository.AddCatalog("Apple", 2.50f);

            _repository.AddState("Apple", 10);

            var state = _repository.GetState("Apple");
            Assert.IsNotNull(state);
            Assert.AreEqual(10, state.Amount);

            _repository.UpdateState("Apple", 15);
            state = _repository.GetState("Apple");
            Assert.AreEqual(15, state.Amount);

            var allStates = _repository.GetAllStates().ToList();
            Assert.AreEqual(1, allStates.Count);

            _repository.RemoveState("Apple");
            Assert.IsNull(_repository.GetState("Apple"));
        }
    }
} 