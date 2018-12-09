using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class File_Types
    {
        [Key]
        public int ID { get; set; }
        public string FieldName { get; set; }
        public bool Deleted { get; set; }
        public string Fields { get; set; }

        //
        public override string ToString()
        {
            return FieldName;
        }
    }
}
