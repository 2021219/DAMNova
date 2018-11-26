using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NOVA.test");

            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "SomeUserName");

            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "Admin");

            if (user != null)
            {
                ValidateCredentials("NOVA.test", UserNameBox.Text, PasswordBox.Text);

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
