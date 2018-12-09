using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class File
    {
        [Key]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Code { get; set; }
        public string Fields { get; set; }
        public int AccessLevel { get; set; }
        public string FilePath { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        //public int LoginID { get; set; }
        public Login Login { get; set; }
        public int FileType { get; set; }
        public bool Locked { get; set; }
        public string LiterallyFile { get; set; }
    }

}
