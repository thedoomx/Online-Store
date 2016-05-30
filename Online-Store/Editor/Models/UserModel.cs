using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Editor.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name ="Email address: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength =6)]
        [Display(Name ="Password: ")]
        public string Password { get; set; }
        
        public string Role { get; set; }
        public string UserId { get; set; }
    }
}