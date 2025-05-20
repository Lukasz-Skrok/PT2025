using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Type { get; set; } // "Customer" or "Supplier"
    }
}
