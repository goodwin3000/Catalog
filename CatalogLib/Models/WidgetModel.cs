using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{

    public class Widget
    {
        public Widget()
        {
            this.Style = "widget-default";
            Preority = 1;
            ColumnNumber = 1;
        }
        public int id { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Содержание")]
        public string Text { get; set; }
      
        [Required]
        [Display(Name = "Разместить в колонке номер")]
        public int ColumnNumber { get; set; }
        [Display(Name = "Особый стиль оформления")]
        public string Style { get; set; }
        [Display(Name = "Публиковать")]
        public bool Published { get; set; }
        [Display(Name = "Приоритет, чем больше тем выше будет элемент в колонке")]
        public int Preority { get; set; }
        [Display(Name = "Место расположения")]        
        public WidgetPosition Position{get;set;}

    }
    public enum WidgetPosition
    {
        Top = 1,
        Middle = 2,
        Bottom = 3,
        side=4
    }
}
