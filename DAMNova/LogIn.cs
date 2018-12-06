using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAMNova
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            Home testform = new Home();
            testform.Show();
        }


        private bool Authenticate(string userName, string password, string domain)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://NOVA.test" + domain, userName, password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException) { }
            return authentic;
        }

        public bool IsInGroup(string domain, string username, string groupname)
        {
            bool result = false;
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                var user = UserPrincipal.FindByIdentity(context, username);
                if (user != null)
                {
                    var group = GroupPrincipal.FindByIdentity(context, groupname);
                    if (group != null && user.IsMemberOf(group))
                        result = true;
                }
            }
            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        // the below code is just a test for now and cannot be used yet
        //public void AddToGroup(string userDn, string groupDn)
        //{
        //    try
        //    {
        //        DirectoryEntry dirEntry = new DirectoryEntry("LDAP://NOVA.test" + groupDn);
        //        dirEntry.Invoke("Add", new object[] { userDn });
        //        dirEntry.CommitChanges();
        //        dirEntry.Close();
        //    }
        //    catch (DirectoryServicesCOMException E)
        //    {
        //        E.Message.ToString();
        //    }
        //}

        private void EnterButton_Click(object sender, EventArgs e)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NOVA.test");

            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "SomeUserName");

            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "Admin");

            if (user != null)
            {
                Authenticate(UserNameBox.Text, PasswordBox.Text, "NOVA.test");

                if (user.IsMemberOf(group))
                {
                    // login as admin
                }
                else
                {
                    // login as user
                }
            }
        }
    }
}
