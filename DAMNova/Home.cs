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
            using (var ctx = new Context())
            {
                ConversionFunctions temp = new ConversionFunctions();
                temp.ShowMetaData((int)explorerTree.SelectedNode.Tag, metaDataBox);
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            using (var ctx = new Context())
            {
                ConversionFunctions temp = new ConversionFunctions();
                foreach (File item in ctx.File)
                {
                    temp.PopulateTreeView(explorerTree, item.ID);
                }

                List<File> list = new List<File>(ctx.File.Where(a => a.ID >= 0));

                // temp.PopulateTreeView2(explorerTree, list);
                
                foreach(File item in list)
                {
                    metaDataBox.Text += item.FileName;
                }

                temp.PopulateTreeView3(explorerTree, list);
                

            }
        }
    }
}
