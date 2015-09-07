using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models
{
  public  class EditOrderViewModel
    {
        public Order Order { get; set; }
        public List<PayOutOption> PayOutOptions { get; set; }
        public List<OrderStatus> OrderStatus { get; set; }
    }
}
