//using DataLayer;
using LogicLayer;
using PresentationLayer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PresentationLayer
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly LogicAPI _logic;

        public string ProductName { get; set; }
        public string ProductAmount { get; set; }

        public ICommand AddUserCommand { get; }
        public ICommand PurchaseCommand { get; }
        public ICommand ShipCommand { get; }

        public ObservableCollection<string> Users { get; } = new ObservableCollection<string>();
        public ObservableCollection<long> UserIds { get; } = new ObservableCollection<long>();
        public ObservableCollection<ProductDisplay> ProductList { get; } = new ObservableCollection<ProductDisplay>();
        public ObservableCollection<string> InventoryDisplay { get; } = new ObservableCollection<string>();
        public ObservableCollection<UserType> UserTypes { get; }

        private string _userId;
        public string UserId
        {
            get => _userId;
            set { _userId = value; OnPropertyChanged(); }
        }

        private UserType _selectedUserType;
        public UserType SelectedUserType
        {
            get => _selectedUserType;
            set { _selectedUserType = value; OnPropertyChanged(); }
        }

        private long? _selectedUserId;
        public long? SelectedUserId
        {
            get => _selectedUserId;
            set { _selectedUserId = value; OnPropertyChanged(); }
        }

        private string _shopFunds;
        public string ShopFunds
        {
            get => _shopFunds;
            set { _shopFunds = value; OnPropertyChanged(); }
        }

        public UserViewModel(LogicAPI logic)
        {
            _logic = logic;

            UserTypes = new ObservableCollection<UserType>((UserType[])System.Enum.GetValues(typeof(UserType)));

            AddUserCommand = new RelayCommand(AddUser);
            PurchaseCommand = new RelayCommand(PurchaseItem);
            ShipCommand = new RelayCommand(ShipItem);

            UpdateShopState();
            RefreshProductList();
        }

        private void AddUser()
        {
            if (long.TryParse(UserId, out long id))
            {
                _logic.AddUser(id, SelectedUserType);
                UserIds.Add(id);
                Users.Add($"Added User - ID: {id} | Type: {SelectedUserType}");
            }
            else
            {
                Users.Add("Invalid user ID.");
            }

            UserId = string.Empty;
            OnPropertyChanged(nameof(UserId));
        }

        private void PurchaseItem()
        {
            if (SelectedUserId == null)
            {
                Users.Add("No user selected.");
                return;
            }

            if (int.TryParse(ProductAmount, out int amt) && !string.IsNullOrEmpty(ProductName))
            {
                float price = _logic.GetPrice(ProductName);
                if (price <= 0)
                {
                    Users.Add($"Product '{ProductName}' does not exist.");
                    return;
                }

                bool success = _logic.Purchase(ProductName, amt);
                Users.Add(success
                    ? $"Purchased {amt} x {ProductName}"
                    : $"Failed to purchase {amt} x {ProductName}");
                RefreshProductList();
                UpdateShopState();
            }
            else
            {
                Users.Add("Invalid input for purchase.");
            }
        }

        private void ShipItem()
        {
            if (SelectedUserId == null)
            {
                Users.Add("No user selected.");
                return;
            }

            if (int.TryParse(ProductAmount, out int amt) && !string.IsNullOrEmpty(ProductName))
            {
                float price = _logic.GetPrice(ProductName);
                if (price <= 0)
                {
                    Users.Add($"Product '{ProductName}' does not exist.");
                    return;
                }

                bool success = _logic.Shipment(ProductName, amt);
                Users.Add(success
                    ? $"Shipped {amt} x {ProductName}"
                    : $"Failed to ship {amt} x {ProductName}");
                RefreshProductList();
                UpdateShopState();
            }
            else
            {
                Users.Add("Invalid input for shipment.");
            }
        }

        private void RefreshProductList()
        {
            ProductList.Clear();
            var inventory = _logic.GetInventory();

            foreach (var item in inventory)
            {
                ProductList.Add(new ProductDisplay
                {
                    Name = item.Key,
                    Quantity = item.Value.quantity,
                    Price = item.Value.price
                });
            }
        }

        private void UpdateShopState()
        {
            ShopFunds = $"${_logic.GetFunds():0.00}";
            InventoryDisplay.Clear();

            foreach (var kvp in _logic.GetInventory())
            {
                InventoryDisplay.Add($"{kvp.Key}: {kvp.Value.quantity} units @ ${kvp.Value.price:0.00}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
