using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
   public class OrderStatus
    {
        public int id { get; set; }
        public string Status { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
