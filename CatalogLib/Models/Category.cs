using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
  public  class Category
    {
        public Category()
        {
            CreatedDate = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int no { get; set; }
        [Required]
        [Key]
        [Display(Name = "Идентификатор категории")]
        public string CategoryLinkName { get; set; }
        [Display(Name = "Отображаемое имя категории")]
        public string CategoryName { get; set; }
        [Display(Name = "Описание категории")]
        public string CategoryDescription { get; set; }
        [Display(Name = "Адресс картинки")]
        public string CategoryPictureUrl { get; set; }
        [Display(Name = "Публиковать")]
        public bool Published { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; }
        public string ParentId { get; set; }
        public virtual Category ParentCategory { get; private set; }
        public virtual ICollection<Category> SubCategories { get; private set; }
        public virtual List<Product> Products { get; set; }
    }
}
