using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models
{
  public  class OrderListViewModel
    {
        public PaginationModel Pagination { get; set; }
        public List<Order> Orders { get; set; }
    }
}
