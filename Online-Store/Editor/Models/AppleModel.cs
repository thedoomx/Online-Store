using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Editor.Models
{
    public class AppleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Type is required!")]
        public string TypeOfApple { get; set; }
        [Required(ErrorMessage ="Price is required!")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Quantity is required!")]
        public int Quantity { get; set; }
    }
}