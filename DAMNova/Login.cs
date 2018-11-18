using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class Login
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        //public List<int> FileID { get; set; }
        public List<File> Files { get; set; }
        //public int RoleID { get; set; }
        public Roles Role { get; set; }
    }
}
