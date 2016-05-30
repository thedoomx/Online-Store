using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Editor.Models
{
    public class StoreModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name of store is required!")]
        public string Name { get; set; }
        public ProductsModel Products { get; set; }
        
    }
}