using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationLayer;
using LogicLayer;
using DataLayer;

namespace PresentationLayerTests
{
    [TestClass]
    public class PresentationTests
    {
        [TestMethod]
        public void AddUserCommand_AddsUserToLog()
        {
            // Arrange
            var logic = LogicFactory.Create();
            var viewModel = new UserViewModel(logic);
            viewModel.UserId = "123";
            viewModel.SelectedUserType = UserType.Customer;

            // Act
            viewModel.AddUserCommand.Execute(null);

            // Assert
            Assert.IsTrue(viewModel.Users.Count > 0, "No log entries were added. Log contents: " + string.Join(", ", viewModel.Users));
            Assert.IsTrue(viewModel.Users.Any(u => u.Contains("Added User")), "No 'Added User' entry found. Log contents: " + string.Join(", ", viewModel.Users));
        }
    }
}