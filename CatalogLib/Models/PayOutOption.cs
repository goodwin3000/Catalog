using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Models
{
  public  class PayOutOption
    {
        [Key]
        public int id { get; set; }
        public string OptionName { get; set; }
        public string OptionDescription { get; set; }        
        public List<Order> Orders { get; set; }   
    }
}
