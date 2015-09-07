using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models
{
  public  class ShopOrderViewModel
    {

        
        [Required(ErrorMessage = "Ошибка заполнения")]
        [Display(Name = "Полные ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Ваш телефон")]
        [Required(ErrorMessage = "Ошибка заполнения")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Ошибка заполнения")]
        [EmailAddress]
        [Display(Name = "Ваш E-mail адрес")]
        public string Email { get; set; }

        public List<PayOutOption> PayoutOptions { get; set; }
        //   public List<SelectListItem> PayoutOptions { get; set; }

        [Display(Name = @"Укажите город и отделение ""Новой почты""")]
        [Required(ErrorMessage = "Ошибка заполнения")]
        public string PostAddress { get; set; }

        [Display(Name = "Способ оплаты")]
        public int SelectedPayOutId { get; set; }
    }
}
