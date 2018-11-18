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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(GetFileType(2));
        }

        private void FetchValues(string[] input)
        {
            int i = 1;
            string tempstring = "";
            while (i < input.Length)
            {
                tempstring += input[i];
                i++;
            }

            switch (input[0])
            {
                case "S":
                    GetString(int.Parse(tempstring));
                    break;

            }


        }

        public string[] StringSplit(string input)
        {
            string[] TempString = input.Split('.');
            return TempString;

        }

        public string GetFileType(int counter)
        {

            using (var ctx = new Context())
            {
                var ans = ctx.FileTypes.Where(Value => Value.ID == counter).SingleOrDefault();
                return ans.FieldName;
            }


        }

        public string GetString(int counter)
        {
            Strings ans;

            using (var ctx = new Context())
            {
                ans = ctx.Strings.Where(Value => Value.ID == counter).SingleOrDefault();
            }

            return ans.Content;
        }

        public int GetInt(int counter)
        {
            Ints ans;

            using (var ctx = new Context())
            {
                ans = ctx.Ints.Where(Value => Value.ID == counter).SingleOrDefault();
            }

            return ans.Content;
        }

        public float GetFloat(int counter)
        {
            Floats ans;

            using (var ctx = new Context())
            {
                ans = ctx.Floats.Where(Value => Value.ID == counter).SingleOrDefault();
            }

            return ans.Content;
        }

        public DateTime GetDateTime(int counter)
        {
            DateTimes ans;

            using (var ctx = new Context())
            {
                ans = ctx.DateTimes.Where(Value => Value.ID == counter).SingleOrDefault();
            }

            return ans.Content;
        }
    }
}
