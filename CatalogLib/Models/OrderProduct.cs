using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
    public class OrderProduct
    {[Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Колличество")]
        public int Quantity { get; set; }
        [NotMapped]
        [Display(Name ="Суммарная стоимость")]
        public decimal TotallPrice
        {
            get { return Quantity * Product.ProductPrice; }
        }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
    
}
