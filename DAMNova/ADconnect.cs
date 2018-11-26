using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace DAMNova
{
    public class ADconnect
    {
        DirectoryEntry dir = new DirectoryEntry("LDAP://NOVA.test");

    }
}
