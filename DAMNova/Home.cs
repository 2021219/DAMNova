using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAMNova
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void explorerTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

                    
        }

        private void fileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Will need some code to upload file/s to database

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            // May need altering, hasn't fully been tested yet
            this.Close();
            LogIn loginform = new LogIn();
            loginform.ShowDialog();
        }
    }
}
