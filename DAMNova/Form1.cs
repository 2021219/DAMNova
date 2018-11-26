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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConversionFunctions temp = new ConversionFunctions();

            foreach (string item in temp.StringSplit("F1.F2.S2.I1.T4.T2.T5.T1"))
            {
                MessageBox.Show(temp.FetchValue(item));
            }

            Home homepage = new Home();
            homepage.Show();
        }

        

       

        

       

       

        

        
    }
}
