using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LogicLayer;
using PresentationModels;

namespace PresentationViewModels
{
    public class StateViewModel : INotifyPropertyChanged
    {
        private readonly ILogicService _logicService;
        private ObservableCollection<StateModel> _inventory;
        private float _currentCash;
        private string _newProductName;
        private int _newProductAmount;
        private string _selectedProduct;
        private int _buyAmount;
        private int _supplyAmount;

        public StateViewModel(ILogicService logicService)
        {
            _logicService = logicService;
            Inventory = new ObservableCollection<StateModel>();
            AddStockCommand = new RelayCommand(AddStock, CanAddStock);
            BuyCommand = new RelayCommand(Buy, CanBuy);
            SupplyCommand = new RelayCommand(Supply, CanSupply);
            RefreshState();
        }

        public ObservableCollection<StateModel> Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        public float CurrentCash
        {
            get => _currentCash;
            set
            {
                _currentCash = value;
                OnPropertyChanged(nameof(CurrentCash));
            }
        }

        public string NewProductName
        {
            get => _newProductName;
            set
            {
                _newProductName = value;
                OnPropertyChanged(nameof(NewProductName));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int NewProductAmount
        {
            get => _newProductAmount;
            set
            {
                _newProductAmount = value;
                OnPropertyChanged(nameof(NewProductAmount));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int BuyAmount
        {
            get => _buyAmount;
            set
            {
                _buyAmount = value;
                OnPropertyChanged(nameof(BuyAmount));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int SupplyAmount
        {
            get => _supplyAmount;
            set
            {
                _supplyAmount = value;
                OnPropertyChanged(nameof(SupplyAmount));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddStockCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand SupplyCommand { get; }

        private void AddStock()
        {
            _logicService.AddToState(NewProductName, NewProductAmount);
            RefreshState();
            NewProductName = string.Empty;
            NewProductAmount = 0;
        }

        private bool CanAddStock()
        {
            return !string.IsNullOrWhiteSpace(NewProductName) && NewProductAmount > 0;
        }

        private void Buy()
        {
            if (_logicService.BuyProduct(SelectedProduct, BuyAmount))
            {
                RefreshState();
                BuyAmount = 0;
            }
        }

        private bool CanBuy()
        {
            return !string.IsNullOrWhiteSpace(SelectedProduct) && BuyAmount > 0;
        }

        private void Supply()
        {
            _logicService.AddSupply(SelectedProduct, SupplyAmount);
            RefreshState();
            SupplyAmount = 0;
        }

        private bool CanSupply()
        {
            return !string.IsNullOrWhiteSpace(SelectedProduct) && SupplyAmount > 0;
        }

        public void RefreshState()
        {
            Inventory.Clear();
            var inventory = _logicService.GetAllInventory();
            foreach (var item in inventory)
            {
                Inventory.Add(new StateModel { product = item.Key, amount = item.Value });
            }
            CurrentCash = _logicService.GetCurrentCash();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 