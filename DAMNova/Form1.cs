﻿using System;
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

            AddFile testadd = new AddFile();
            testadd.Show();
            
            Home homepage = new Home();
            homepage.Show();
        }

        

       

        

       

       

        

        
    }
}
