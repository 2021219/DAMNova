using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAMNova
{
    public class ConversionFunctions
    {
        public string FetchValue(string input)
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

        public string GetString(int counter)
        {
            Strings ans;

            using (var ctx = new Context())
            {
                ans = ctx.Strings.Where(Value => Value.ID == counter).SingleOrDefault();
            }

            return ans.Content;
        }

        public string GetFileType(int counter)
        {
            File_Types ans;

            using (var ctx = new Context())
            {
                ans = ctx.FileTypes.Where(Value => Value.ID == counter).SingleOrDefault();
                return ans.FieldName;
            }


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

        public File GetFileInt(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.File.Where(a => a.ID == id).FirstOrDefault();
            }
        }

        public void PopulateTreeView(TreeView output, int input)
        {
            output.Nodes.Add(GetFileInt(input).FileName);
            output.Nodes[output.Nodes.Count-1].Tag = input;

        }

        public void ShowMetaData(int input, TextBox output)
        {
            using (var ctx = new Context())
            {
                File temp = ctx.File.Where(a => a.ID == input).FirstOrDefault();

                int i = 0;

                while (i < StringSplit(temp.Fields).Count())
                {
                    output.Text += FetchValue(StringSplit(temp.Fields)[i]) + ": " + FetchValue(StringSplit(temp.Code)[i]) + "\n";
                    i++;
                }
            }
        }

        public string[] SplitFilePath(string input)
        {
            return input.Split('\\');
        }


        public void PopulateTreeView2(TreeView output, List<File> input)
        {
            foreach(var item in input)
            {
                string[] t = SplitFilePath(item.FilePath);
                t.Reverse();
                TreeNode tempnode = new TreeNode(t[0]);
                foreach (string item2 in t.Skip(1))
                {
                    TreeNode tempnode2 = new TreeNode(item2);
                    tempnode2.Nodes.Add(tempnode);
                    tempnode = tempnode2;
                    output.Nodes.Add(tempnode2);

                }
            }
        }

        public void PopulateTreeView3(TreeView output, List<File> input)
        {
            TreeNode tempnode = null;
            foreach (var item in input)
            {
                foreach (string item2 in SplitFilePath(item.FilePath))
                {
                    TreeNode[] tempnode2 = output.Nodes.Find(item2, true);
                    if (tempnode2.Length == 0)
                    {
                        if (tempnode == null)
                        {
                            tempnode = output.Nodes.Add(item2, item2);
                        }
                    }

                }
            }
        }

        private static void PopulateTreeView4(TreeView treeView, IEnumerable<string> paths, char pathSeparator)
        {
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in paths)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {
                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = treeView.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
            }
        }

        public void PopulateSplit(TreeNode input, string input2)
        {
            input.Nodes.Add(input2);
        }

    }



}
