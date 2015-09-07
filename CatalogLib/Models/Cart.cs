using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
  public  class Cart
    {
        private List<CartItem> Items = new List<CartItem>();
        public void Add(Product product,int quantity)
        {
            CartItem ct;
            ct = Items.Where(ci => ci.Product.ProductId == product.ProductId).SingleOrDefault();
            if ( ct== null)
            {
              ct  = new CartItem { Product = product, Quantity = quantity };
                Items.Add(ct);
            }
            else
            {
                ct.Quantity += quantity;
            }
        }
        public List<CartItem> CartItems
        {
            get { return Items; }
           
        }
        public void Remove(Product product)
        {
            Items.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }
        public decimal TotalPrice ()
        {
            return Items.Sum(i => i.Product.ProductPrice * i.Quantity); 
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
}
