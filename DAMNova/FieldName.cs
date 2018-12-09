using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class FieldName
    {
        [Key]
        public int ID { get; set; }
        public string Content { get; set; }
        public string DataType { get; set; }
    }
}
