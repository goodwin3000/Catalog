using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models
{
   public class AdminDashBoardModel
    {
        public List<Category> Category { get; set; }
        public List<Product> Products { get; set; }
    }
}
