using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing.Core.DataAccess.Entities
{
    public class Login
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        public string AuthorizationToken { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual User User { get; set; }
    }
}
