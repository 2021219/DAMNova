using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class Ints
    {
        [Key]
        public int ID { get; set; }
        public int Content { get; set; }
    }
}
