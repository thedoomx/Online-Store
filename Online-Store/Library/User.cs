using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Key]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(202)]
        public string PasswordSalt { get; set; }
        
        public virtual MemberBasket MemberBasket { get; set; }
        public virtual List<Role> Roles { get; set; }
    }
}
