using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAMNova
{
    public class Context : DbContext
    {

        public DbSet<Login> Login { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<File_Types> FileTypes { get; set; }
        public DbSet<Ints> Ints { get; set; }
        public DbSet<Strings> Strings { get; set; }
        public DbSet<Floats> Floats { get; set; }
        public DbSet<DateTimes> DateTimes { get; set; }
        public DbSet<File> File { get; set; }


    }
}
//hi