using DataLayer;

namespace LogicLayer
{
    public static class LogicLayerInitializer
    {
        public static ILogicService InitializeLogicService()
        {
            // This is the composition root for the Logic and Data Layers
            DatabaseInitializer.Initialize(); // Ensure database exists and is initialized
                
            // Create data layer concrete instances
            var repository = DataRepository.CreateNewRepository(DatabaseInitializer.GetConnectionString());
            var state = new State_class(repository);
            var events = new Events_class(); // Events_class also creates its own repository internally based on its constructor

            // Create logic layer instance, injecting data layer dependencies
            var logicService = new LogicService(state, events);
            
            return logicService;
        }
    }
} 