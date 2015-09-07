using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
  public  class Product
    {
        public Product()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Описание продукта")]
        public string ProductDescription { get; set; }
        [Display(Name = "Картинка продукта")]
        public string ProductPictureUrl { get; set; }
        [Display(Name = "Цена")]
      
        public decimal ProductPrice { get; set; }
        [Display(Name = "Названиеи")]
        public string ProductName { get; set; }
        [Display(Name = "Публиковать")]
        public bool Published { get; set; }
        [Display(Name = "Дата создания/изменения")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Идентификатор категории продукта")]
        public string CategoryLinkName { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<ApplicationUser> Users { get; set; }
    
       public virtual List<OrderProduct> OrderProduct { get; set; }
    }
}
