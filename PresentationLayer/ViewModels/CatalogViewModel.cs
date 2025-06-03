using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LogicLayer;

namespace PresentationLayer.ViewModels
{
    public class CatalogViewModel : INotifyPropertyChanged
    {
        private readonly ILogicService _logicService;
        private ObservableCollection<ProductModel> _products;
        private string _newProductName;
        private string _newProductPrice;

        public CatalogViewModel(ILogicService logicService)
        {
            _logicService = logicService;
            Products = new ObservableCollection<ProductModel>();
            AddProductCommand = new RelayCommand(AddProduct, CanAddProduct);
            RefreshProducts();
        }

        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
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

        public string NewProductPrice
        {
            get => _newProductPrice;
            set
            {
                _newProductPrice = value;
                OnPropertyChanged(nameof(NewProductPrice));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddProductCommand { get; }

        private void AddProduct()
        {
            _logicService.AddProduct(NewProductName, float.Parse(NewProductPrice));
            RefreshProducts();
            NewProductName = string.Empty;
            NewProductPrice = "";
        }

        private bool CanAddProduct()
        {
            if (string.IsNullOrWhiteSpace(NewProductName))
                return false;
            if (string.IsNullOrWhiteSpace(NewProductPrice))
                return false;
            if (!float.TryParse(NewProductPrice, out float price))
                return false;
            return price > 0;
        }

        public void RefreshProducts()
        {
            Products.Clear();
            var products = _logicService.GetAllProducts();
            foreach (var product in products)
            {
                Products.Add(new ProductModel { name = product.Key, price = product.Value });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 