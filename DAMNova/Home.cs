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
            using (var ctx = new Context())
            {
                foreach (File_Types item in ctx.FileTypes.Where(a => a.Deleted == false))
                {
                    cbSearchCategory.Items.Add(item);
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ConversionFunctions Temp = new ConversionFunctions();
            Temp.Search(searchBox.Text, ExplorerList, chbsearchdeleted.Checked, (File_Types)cbSearchCategory.SelectedItem);

            ToggleButtons(btnDelete, btnOpen);
        }

        private void ExplorerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleButtons(btnDelete, btnOpen);
        }

        private void ToggleButtons(Button buttonopen, Button buttondelete)
        {
            if (ExplorerList.SelectedItems != null)
            {
                buttonopen.Enabled = true;
                buttondelete.Enabled = true;
            }
            else
            {
                buttonopen.Enabled = false;
                buttondelete.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConversionFunctions temp = new ConversionFunctions();
            using (var ctx = new Context())
            {
                int tempint = int.Parse(ExplorerList.SelectedItems[0].Text);

                File tempfile = ctx.File.Where(a => a.ID == tempint).FirstOrDefault();
                temp.StringToFile(tempfile.LiterallyFile, tbpassword.Text, tempfile.FileName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ConversionFunctions temp = new ConversionFunctions();
            using (var ctx = new Context())
            {
                int tempint = int.Parse(ExplorerList.SelectedItems[0].Text);

                File tempfile = ctx.File.Where(a => a.ID == tempint).FirstOrDefault();
                if (tempfile.Deleted == true)
                { tempfile.Deleted = false; }
                else
                { tempfile.Deleted = true; }

                ctx.SaveChanges();
                
                temp.Search(searchBox.Text, ExplorerList, chbsearchdeleted.Checked, (File_Types)cbSearchCategory.SelectedItem);

                ToggleButtons(btnDelete, btnOpen);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFile FileOpen = new AddFile();
            FileOpen.Show();
        }
    }
}
