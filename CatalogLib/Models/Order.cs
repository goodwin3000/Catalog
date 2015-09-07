using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
  public  class Order
    {
        public Order()
        {
           
        }

        [Display(Name = "Номер заказа")]
        public int OrderId { get; set; }
        [Display(Name ="Дата оформления заказа")]
        public DateTime OrerDate { get; set; }
        [Display(Name = "ФИО покупателя")]
        public string CustomerFullName { get; set; }
        [Display(Name = "Email покупателя")]
        public string Email { get; set; }
        [Display(Name = "Телефон покупателя")]
        public string Phone { get; set; }
        [Display(Name = "Адрес почты")]
        public string PostAddress { get; set; }
      
        public int PayOutId { get; set; }
        [ForeignKey("PayOutId")]
        [Display(Name = "Способ доставки")]
        public virtual PayOutOption PayOutOption { get; set; }

        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        [Display(Name = "Сатус заказа")]
        public virtual OrderStatus OrderStatus { get; set; }
    
        public virtual List<OrderProduct> OrderProducts { get; set; }
        [NotMapped]
        [Display(Name ="Сумма заказа")]
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                if (OrderProducts != null)
                {
                    foreach (var item in OrderProducts)
                    {
                        total += item.TotallPrice;
                    }
                }
               
                return total;
            }
        }
    }
}
