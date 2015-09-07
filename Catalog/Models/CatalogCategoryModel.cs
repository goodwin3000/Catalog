using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class CatalogCategoryModel
    {
        public List<Product> Products { get; set; }
       public PaginationModel Pagination { get; set; }
        public Category Category { get; set; }
    }
}
