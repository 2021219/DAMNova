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
    public partial class AddFile : Form
    {
        public AddFile()
        {
            InitializeComponent();
        }

        private void AddFile_Load(object sender, EventArgs e)
        {
            using (var ctx = new Context())
            {
                foreach (File_Types item in ctx.FileTypes.Where(a => a.Deleted == false))
                {
                    cbFileType.Items.Add(item);
                }
            }
        }

        private void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFileName.Text != "")
            {
                RemoveFields();
                File_Types tempitem = (File_Types)cbFileType.Items[cbFileType.SelectedIndex];
                GenerateInputs(tempitem.Fields);
                cbFileType.Tag = tempitem.Fields;
            }
            else
            {
                MessageBox.Show("Select a file");
            }
        }

        private void GenerateInputs(string code)
        {
            int count = 1;

            using (var ctx = new Context())
            {
                ConversionFunctions temp = new ConversionFunctions();

                foreach (string item in temp.StringSplit(code))
                {
                    if (item != "")
                    {
                        int i = 1;
                        string tempstring = "";
                        while (i < item.Length)
                        {
                            tempstring += item[i];
                            i++;
                        }

                        int tempint = int.Parse(tempstring);

                        FieldName tempfield = ctx.FieldName.Where(a => a.ID == tempint).FirstOrDefault();

                        switch (tempfield.DataType[0])
                        {

                            case 'S':

                                Label NewLabel = new Label();
                                TextBox NewTextBox = new TextBox();
                                // label4
                                // 
                                NewLabel.AutoSize = true;
                                NewLabel.Location = new System.Drawing.Point(3, 61 + (count * 24));
                                NewLabel.Name = "NewLabel";
                                NewLabel.Size = new System.Drawing.Size(69, 13);
                                NewLabel.TabIndex = 7;
                                NewLabel.Text = temp.FetchValue(item);
                                NewLabel.Tag = "L";

                                // NewTextBox
                                // 
                                NewTextBox.Location = new System.Drawing.Point(75, 58 + (count * 24));
                                NewTextBox.Name = "NewTextBox";
                                NewTextBox.Size = new System.Drawing.Size(219, 20);
                                NewTextBox.TabIndex = 6;
                                NewTextBox.Tag = "S" + count.ToString();

                                this.Controls.Add(NewLabel);
                                this.Controls.Add(NewTextBox);

                                break;

                            case 'F':

                                Label NewLabel1 = new Label();
                                NumericUpDown NewTextBox1 = new NumericUpDown();
                                // label4
                                // 
                                NewLabel1.AutoSize = true;
                                NewLabel1.Location = new System.Drawing.Point(3, 61 + (count * 24));
                                NewLabel1.Name = "NewLabel";
                                NewLabel1.Size = new System.Drawing.Size(69, 13);
                                NewLabel1.TabIndex = 7;
                                NewLabel1.Text = temp.FetchValue(item);
                                NewLabel1.Tag = "L";

                                // NewTextBox
                                // 
                                NewTextBox1.Location = new System.Drawing.Point(75, 58 + (count * 24));
                                NewTextBox1.Name = "NewTextBox";
                                NewTextBox1.Size = new System.Drawing.Size(219, 20);
                                NewTextBox1.TabIndex = 6;
                                NewTextBox1.Tag = "F" + count.ToString();
                                NewTextBox1.DecimalPlaces = 20;

                                this.Controls.Add(NewLabel1);
                                this.Controls.Add(NewTextBox1);

                                break;


                            case 'I':

                                Label NewLabelI = new Label();
                                NumericUpDown NewTextBoxI = new NumericUpDown();
                                // label4
                                // 
                                NewLabelI.AutoSize = true;
                                NewLabelI.Location = new System.Drawing.Point(3, 61 + (count * 24));
                                NewLabelI.Name = "NewLabel";
                                NewLabelI.Size = new System.Drawing.Size(69, 13);
                                NewLabelI.TabIndex = 7;
                                NewLabelI.Text = temp.FetchValue(item);
                                NewLabelI.Tag = "L";

                                // NewTextBox
                                // 
                                NewTextBoxI.Location = new System.Drawing.Point(75, 58 + (count * 24));
                                NewTextBoxI.Name = "NewTextBox";
                                NewTextBoxI.Size = new System.Drawing.Size(219, 20);
                                NewTextBoxI.TabIndex = 6;
                                NewTextBoxI.Tag = "I" + count.ToString();
                                NewTextBoxI.DecimalPlaces = 0;

                                this.Controls.Add(NewLabelI);
                                this.Controls.Add(NewTextBoxI);

                                break;

                            case 'D':

                                Label NewLabelD = new Label();
                                DateTimePicker NewTextBoxD = new DateTimePicker();
                                // label4
                                // 
                                NewLabelD.AutoSize = true;
                                NewLabelD.Location = new System.Drawing.Point(3, 61 + (count * 24));
                                NewLabelD.Name = "NewLabel";
                                NewLabelD.Size = new System.Drawing.Size(69, 13);
                                NewLabelD.TabIndex = 7;
                                NewLabelD.Text = temp.FetchValue(item);
                                NewLabelD.Tag = "L";

                                // NewTextBox
                                // 
                                NewTextBoxD.Location = new System.Drawing.Point(75, 58 + (count * 24));
                                NewTextBoxD.Name = "NewTextBox";
                                NewTextBoxD.Size = new System.Drawing.Size(219, 20);
                                NewTextBoxD.TabIndex = 6;
                                NewTextBoxD.Tag = "D" + count.ToString();

                                this.Controls.Add(NewLabelD);
                                this.Controls.Add(NewTextBoxD);
                                MessageBox.Show((string)NewTextBoxD.Tag);
                                break;
                        }

                        count += 1;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OFDSelectFile.ShowDialog();
            cbFileName.Text = OFDSelectFile.FileName;
        }

        private void RemoveFields()
        {
            List<Control> templist = new List<Control>();

            foreach (Control item in Controls)
            {
                if ((string)item.Tag != "" & (string)item.Tag != null)
                {
                    if (((string)item.Tag)[0] != '.' )
                    templist.Add(item);
                }
            }

            foreach (Control item in templist)
            {
                this.Controls.Remove(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var ctx = new Context())
            {
                List<Control> templist = new List<Control>();
                File tempfile = new File();
                string Code = ".";

                foreach (Control item in Controls)
                {
                    if ((string)item.Tag != "" & (string)item.Tag != null)
                    {
                        if (((string)item.Tag)[0] != 'L' & ((string)item.Tag)[0] != '.')
                        {
                            templist.Add(item);
                        }
                    }
                }

                string[] temparray = new string[templist.Count];


                foreach (Control item in templist)
                {
                    string tempstring = (string)item.Tag;

                    string tempstring2 = tempstring.Substring(1, tempstring.Length - 1);
                    switch (tempstring[0])
                    {
                        case 'S':

                            Strings tempstrings = new Strings();
                            tempstrings.Content = item.Text;
                            ctx.Strings.Add(tempstrings);
                            ctx.SaveChanges();

                            tempstring = "S" + tempstrings.ID;
                            temparray[int.Parse(tempstring2) - 1] = tempstring;

                            break;
                        case 'F':

                            Floats tempfloats = new Floats();
                            tempfloats.Content = float.Parse(item.Text);
                            ctx.Floats.Add(tempfloats);
                            ctx.SaveChanges();

                            tempstring = "F" + tempfloats.ID;
                            temparray[int.Parse(tempstring2) - 1] = tempstring;

                            break;
                        case 'I':

                            Ints tempints = new Ints();
                            tempints.Content = int.Parse(item.Text);
                            ctx.Ints.Add(tempints);
                            ctx.SaveChanges();

                            tempstring = "I" + tempints.ID;
                            temparray[int.Parse(tempstring2) - 1] = tempstring;

                            break;

                        case 'D':

                            DateTimes temptimes = new DateTimes();
                            temptimes.Content = ((DateTimePicker)item).Value;
                            ctx.DateTimes.Add(temptimes);
                            ctx.SaveChanges();

                            tempstring = "D" + temptimes.ID;
                            temparray[int.Parse(tempstring2) - 1] = tempstring;

                            break;
                    }
                }

                int count = 0;
                while (count < temparray.Length)
                {
                    Code += temparray[count] + ".";
                    count++;
                }

                ConversionFunctions temp = new ConversionFunctions();

                tempfile.Fields = (string)cbFileType.Tag;
                tempfile.Code = Code;
                tempfile.FilePath = cbFileName.Text;
                tempfile.FileType = ctx.FileTypes.Where(a => a.FieldName == ((File_Types)cbFileType.SelectedItem).FieldName).FirstOrDefault().ID;
                tempfile.LastModifiedOn = DateTime.Now;
                tempfile.AccessLevel = 1;
                tempfile.Login = ctx.Login.Where(a => a.UserID == 3).FirstOrDefault();
                tempfile.Deleted = false;
                tempfile.FileName = cbFileName.Text.Split('\\')[cbFileName.Text.Split('\\').Length - 1];
                tempfile.Locked = chbPassword.Checked;
                tempfile.LiterallyFile = temp.FileToString(cbFileName.Text, tbpassword.Text);

                if (tempfile.Code != ".")
                {
                    if (temp.StringSplit(tempfile.Code).Length == temp.StringSplit(tempfile.Fields).Length)
                    {
                        ctx.File.Add(tempfile);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Please fill out all required information");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill out all required information");
                }
                

                

            }
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbpassword.Enabled = chbPassword.Checked;
        }
    }

}
