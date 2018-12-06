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

        public bool ValidateCredentials(string domain, string username, string password)
        {
            bool valid = false;
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                valid = context.ValidateCredentials(username, password);
            }
            if (valid == true)
            {
                return valid;
            }
            else
            {
                MessageBox.Show("Error", "Account does not exist");
                Application.Exit();
                return valid;
            }
        }

        //public bool IsInGroup(string domain, string username, string groupname)
        //{
        //    bool result = false;
        //    using (var context = new PrincipalContext(ContextType.Domain, domain))
        //    {
        //        var user = UserPrincipal.FindByIdentity(context, username);
        //        if (user != null)
        //        {
        //            var group = GroupPrincipal.FindByIdentity(context, groupname);
        //            if (group != null && user.IsMemberOf(group))
        //                result = true;
        //        }
        //    }
        //    return result;
        //}

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
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "LDAP://NOVA.test");

            if (UserNameBox.Text != null || PasswordBox.Text != null)
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, UserNameBox.Text);
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
            else
            {
                MessageBox.Show("Please enter all fields","", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
