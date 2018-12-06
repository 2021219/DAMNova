using System;
using System.Collections.Generic;
using System.IO;
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
            output.Nodes[output.Nodes.Count - 1].Tag = input;

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
            foreach (var item in input)
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

        public void PopulateTreeView5(TreeView workingTreeview, List<File> FileList, TextBox testbox)
        {
            foreach (File FileItem in FileList)
            {

                string FIlePath = Path.GetFullPath(FileItem.FilePath);

                if (workingTreeview.Nodes.Count > 0)
                {
                    TreeNode FoundNode = SelectNodeByTag(FIlePath, workingTreeview.Nodes[0]);

                    if (FoundNode != null)
                    {

                    }
                    else
                    {
                        //We couldnt find it, now add it
                        workingTreeview.Nodes.Add(FIlePath, FileItem.FileName);
                    }
                }
                else
                {
                    //There are no other nodes, add this one!
                    //Item: c:\dasas\dasdasd\dasdas\file.txt
                }




                /*
                // to 
                //C:  Store    Whatever     Something    File.txt
                string[] FIlePathParts = SplitFilePath(FileItem.FilePath);

                //???
                FIlePathParts.Reverse();

                TreeNode tempnode = null;
                List<TreeNode> nodelist = new List<TreeNode>();


                foreach (string FilePartItem in FIlePathParts)
                {
                    //Locate the   c:  or the    Store     node to add items below it



                    nodelist.Add(new TreeNode(FilePartItem));
                }

                int i = nodelist.Count - 1;





                //Loop the NodeList
                while (i > 0)
                {
                    nodelist[i - 1].Nodes.Add(nodelist[i]);
                    string tempstring;
                    i--;
                }

                workingTreeview.Nodes.Add(nodelist[0]);

                testbox.AppendText("\r\n");
                */
            }
        }

        public TreeNode SelectNodeByTag(string NodeTag, TreeNode BaseNode)
        {

            //Basenode is the one!! :)
            if ((string)BaseNode.Tag == NodeTag)
            {
                return BaseNode;
            }


            //Loop all the items below the BaseNode
            foreach (TreeNode node in BaseNode.Nodes)
            {

                //Once of the sub items of the basenode is the one
                if ((string)node.Tag == NodeTag)
                {
                    return node;
                }
                else
                {
                    //So the sub node was not the one. It may be one of the children of the subnode...
                    TreeNode SubNode = SelectNodeByTag(NodeTag, node);
                    if (SubNode != null)
                        return SubNode;
                }
            }

            //When all else fails
            return null;
        }


























        public void populateListView(List<String> FieldList, ListView OutputList)
        {
            OutputList.Columns.Clear();
            var item = new HashSet<string>(FieldList);

            OutputList.Columns.Add("ID", "ID", 40, HorizontalAlignment.Left, 0);
            OutputList.Columns.Add("FileName", "FileName", 100, HorizontalAlignment.Left, 0);
            OutputList.Columns.Add("DateModified", "Date Modified", 150, HorizontalAlignment.Left, 0);

            foreach (string i in item)
            {
                OutputList.Columns.Add(i, FetchValue(i), 120, HorizontalAlignment.Left, 3);
            }
        }











        public List<File> SearchFiles(string SearchValue)
        {
            using (var ctx = new Context())
            {
                List<String> tempstringlist = new List<String>();
                List<File> tempfilelist = new List<File>();
                foreach (Strings item in ctx.Strings)
                {
                    if (item.Content.ToLower() == SearchValue.ToLower())
                    {
                        tempstringlist.Add(".S" + item.ID.ToString() + ".");
                    }
                }
                foreach (Ints item in ctx.Ints)
                {
                    if (item.Content.ToString() == SearchValue)
                    {
                        tempstringlist.Add(".I" + item.ID.ToString() + ".");
                    }
                }
                foreach (Floats item in ctx.Floats)
                {
                    if (item.Content.ToString() == SearchValue)
                    {
                        tempstringlist.Add(".F" + item.ID.ToString() + ".");
                    }
                }
                foreach (DateTimes item in ctx.DateTimes)
                {
                    if (item.Content.ToString() == SearchValue)
                    {
                        tempstringlist.Add(".D" + item.ID.ToString() + ".");
                    }
                }
                foreach (File_Types item in ctx.FileTypes)
                {
                    if (item.FieldName == SearchValue)
                    {
                        tempstringlist.Add(".T" + item.ID.ToString() + ".");
                    }
                }



                foreach (File item in ctx.File)
                {
                    if (item.FileName == SearchValue || tempstringlist.Any(item.Fields.Contains) || tempstringlist.Any(item.Code.Contains)
                                                     || item.ID.ToString() == SearchValue)
                    {
                        if (item.Deleted == false)
                        {
                            tempfilelist.Add(item);
                        }
                    }
                }
                return tempfilelist;
            }
        }







        public void Search(string SearchValue, ListView OutputListView)
        {
            OutputListView.Items.Clear();

            List<File> templist = SearchFiles(SearchValue);
            List<string> tempstringlist = new List<string>();

            //Generate list of fields
            foreach (File item in templist)
            {
                foreach (string item2 in StringSplit(item.Fields))
                {
                    if (item2 == "")
                    { }
                    else
                    {
                        tempstringlist.Add(item2);
                    }
                }
            }
            
            // Builds the listview's columns
            populateListView(tempstringlist, OutputListView);



            //Populates the listview with data

            foreach (File item in templist)
            {
                ListViewItem templistitem = new ListViewItem();
                List<ListViewItem.ListViewSubItem> tempsublist = new List<ListViewItem.ListViewSubItem>();
                List<string> tempstringlist2 = new List<string>();
                int counter = 0;



                foreach (string item2 in StringSplit(item.Code))
                {
                    tempstringlist2.Add(item2);
                }


                foreach (string item2 in StringSplit(item.Code))
                {
                    if (item2 == "")
                    {
                        counter += 1;
                    }
                    else
                    {
                        ListViewItem.ListViewSubItem tempsub = new ListViewItem.ListViewSubItem();
                        tempsub.Tag = StringSplit(item.Fields)[counter];
                        tempsub.Text = FetchValue(item2);
                        tempsublist.Add(tempsub);
                        counter += 1;
                    }

                }
                //sets ID, Name & DateModified 

                templistitem.Text = item.ID.ToString();
                ListViewItem.ListViewSubItem tempitemprop = new ListViewItem.ListViewSubItem();
                ListViewItem.ListViewSubItem tempitemprop2 = new ListViewItem.ListViewSubItem();
                tempitemprop.Text = item.FileName;
                templistitem.SubItems.Add(tempitemprop);
                tempitemprop2.Text = item.LastModifiedOn.ToString();
                templistitem.SubItems.Add(tempitemprop2);

                foreach (ColumnHeader item2 in OutputListView.Columns)
                {

                    Boolean found = false;

                    foreach (ListViewItem.ListViewSubItem item3 in tempsublist)
                    {
                        if ((string)item3.Tag == item2.Name)
                        {
                            templistitem.SubItems.Add(item3);
                            found = true;
                        }
                    }
                    if (found == false & item2.Name != "ID" & item2.Name != "DateModified" & item2.Name != "FileName")
                    {
                        ListViewItem.ListViewSubItem temp = new ListViewItem.ListViewSubItem();
                        temp.Text = "";
                        templistitem.SubItems.Add("");
                    }
                }
                OutputListView.Items.Add(templistitem);
            }

        }
    }
}





