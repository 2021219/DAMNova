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

        private void Form1_Load(object sender, EventArgs e)
        {
          InitializeComponent();
            foreach (string item in StringSplit("F1.F2.S2.I1.T4.T2.T5.T1"))
            {
                MessageBox.Show(FetchValue(item));
            }
        }

        private string FetchValue(string input)
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
                case 'S':
                    tempstring = GetString(int.Parse(tempstring));
                    break;
                case 'F':
                    tempstring = GetFloat(int.Parse(tempstring)).ToString();
                    break;
                case 'D':
                    tempstring = GetDateTime(int.Parse(tempstring)).ToString();
                    break;
                case 'I':
                    tempstring = GetInt(int.Parse(tempstring)).ToString();
                    break;
                case 'T':
                    tempstring = GetFileType(int.Parse(tempstring)).ToString();
                    break;
            }

            return tempstring;


        }

        public string[] StringSplit(string input)
        {
            string[] TempString = input.Split('.');
            return TempString;

        }

        public string GetFileType(int counter)
        {
            File_Types ans;

            using (var ctx = new Context())
            {
                ans = ctx.FileTypes.Where(Value => Value.ID == counter).SingleOrDefault();
                return ans.FieldName;
            }

            ConversionFunctions temp = new ConversionFunctions();

        }

        public string GetString(int counter)
        {
            Strings ans;

            using (var ctx = new Context())
            foreach (string item in temp.StringSplit("F1.F2.S2.I1.T4.T2.T5.T1"))
            {
                ans = ctx.Strings.Where(Value => Value.ID == counter).SingleOrDefault();
                MessageBox.Show(temp.FetchValue(item));
            }

            return ans.Content;
            Home homepage = new Home();
            homepage.Show();
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
