using System.Data.Linq.Mapping;

namespace DataLayer
{
    [Table(Name = "Inventory")]
    public class Inventory
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string ProductName { get; set; }

        [Column]
        public int Quantity { get; set; }

        [Column]
        public double Price { get; set; }
    }
} 