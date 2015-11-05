using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace webshop.Models
{
  //med virtual vare list
  public class CartItem
  {
  

    [Key]
    public string ItemId { get; set; }
    public string CartId { get; set; }
    public int VareId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitSum { get; set; }
    public virtual Vare Vare { get; set; }
  }
}