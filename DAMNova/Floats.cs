using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class Floats
    {
        [Key]
        public int ID { get; set; }
        public float Content { get; set; }
    }
}
