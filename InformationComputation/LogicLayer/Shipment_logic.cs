using DataLayer;

namespace LogicLayer
{
    public abstract class Shipment_logic
    {
        protected readonly Events _events;

        public Shipment_logic(Events events)
        {
            _events = events;
        }

        public abstract bool Shipment(string productName, int amount);
        public abstract Dictionary<string, (int quantity, double price)> GetInventory();
        public abstract double GetFunds();
        public abstract double GetPrice(string name);
    }

    public class ShipmentLogic : Shipment_logic
    {
        public ShipmentLogic(Events events) : base(events) { }

        public override bool Shipment(string productName, int amount)
        {
            if (string.IsNullOrWhiteSpace(productName) || amount <= 0)
                return false;

            if (!_events.GetAllItemNames().Contains(productName))
            {
                double defaultPrice = 10.0;
                _events.AddItem(productName, defaultPrice);
            }

            double price = _events.GetPrice(productName);
            _events.RecordProfit(price * amount);
            _events.AddToStorage(productName, amount);
            return true;
        }

        public override Dictionary<string, (int quantity, double price)> GetInventory()
        {
            return _events.GetInventory();
        }

        public override double GetFunds()
        {
            return _events.GetFunds();
        }

        public override double GetPrice(string name)
        {
            try
            {
                return _events.GetPrice(name);
            }
            catch
            {
                return -1; // Or handle it differently if needed
            }
        }
    }
}
