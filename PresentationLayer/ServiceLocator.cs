using DataLayer;
using LogicLayer;

namespace PresentationLayer
{
    public static class ServiceLocator
    {
        private static ILogicService _logicService;

        public static ILogicService GetLogicService()
        {
            if (_logicService == null)
            {
                DatabaseInitializer.Initialize();
                
                // to zeby nie bralo events bezposrednio w viewmodel
                var events = new Events_class();
                _logicService = new LogicService(events);
            }
            return _logicService;
        }
    }
} 