using System.ComponentModel;
using LogicLayer;
//only logic (abstract) no data layer (dependency injection)
namespace PresentationLayer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ILogicService _logicService;
        private UsersViewModel _usersViewModel;
        private CatalogViewModel _catalogViewModel;
        private StateViewModel _stateViewModel;

        public MainViewModel(ILogicService logicService)
        {
            _logicService = logicService;
            
            UsersViewModel = new UsersViewModel(_logicService);
            CatalogViewModel = new CatalogViewModel(_logicService);
            StateViewModel = new StateViewModel(_logicService);
        }

        public UsersViewModel UsersViewModel
        {
            get => _usersViewModel;
            set
            {
                _usersViewModel = value;
                OnPropertyChanged(nameof(UsersViewModel));
            }
        }

        public CatalogViewModel CatalogViewModel
        {
            get => _catalogViewModel;
            set
            {
                _catalogViewModel = value;
                OnPropertyChanged(nameof(CatalogViewModel));
            }
        }

        public StateViewModel StateViewModel
        {
            get => _stateViewModel;
            set
            {
                _stateViewModel = value;
                OnPropertyChanged(nameof(StateViewModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
