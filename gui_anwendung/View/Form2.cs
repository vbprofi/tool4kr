using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KRTool;
namespace KRTool.View
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
            //TreeView
            this.treeView1.LabelEdit = true;
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "mainNode";
            mainNode.Text = "Kurdistan-Report";
            this.treeView1.Nodes.Add(mainNode);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //TreeView füllen
            TreeFüllen();
            //this.treeView1.ExpandAll(); //Alles aufklappen beim Starten
        }

        private void TreeFüllen()
        {
            TreeNode nd = new TreeNode();
            nd.Text = "Aktuelle Uhrzeit (auf/zuklappen)";
            nd.Name = "Uhrzeit";
            this.treeView1.Nodes[0].Nodes.Add(nd);
            this.treeView1.FakeNode(nd);
            }
        private void treeView1_LoadData(object sender, TreeViewCancelEventArgs e)
        {
            if(e.Node.Name== "Uhrzeit")
            {
                e.Node.Nodes.Add(new TreeNode(DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second.ToString()));                
            }
            else
            {
                TreeFüllen();
            }            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(e.Node.Text);
            /*
            try
            {
                txtName.Text = "";
                txtParentName.Text = "";
                txtText.Text = "";
                txtTag.Text = "";
                txtName.Text = treeView1.SelectedNode.Name.ToString();
                txtText.Text = treeView1.SelectedNode.Text.ToString();
                txtTag.Text = treeView1.SelectedNode.Tag.ToString();
                txtParentName.Text = treeView1.SelectedNode.Parent.Text.ToString();
            }
            catch { }
            */

            // iterate through parent nodes in the collection
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                if (node.IsSelected)
                {
                    //MessageBox.Show(node.Text + "is selected");
                    listView1.Items.Clear();
                                    }
                CheckForChildren(node);
            }
        }

        private void CheckForChildren(TreeNode node)
        {
            // check whether each parent node has child nodes
            if (node.IsExpanded && node.Nodes.Count > 0)
            {

                // iterate through child nodes in the collection
                foreach (TreeNode nd in node.Nodes)
                {
                    if (nd.IsSelected)
                    {
                        //MessageBox.Show(nd.Text + "is selected");
                        loadListViewContent(nd.Name, nd.Text);
                    }

                    // Do recursive call
                    CheckForChildren(nd);
                }

            }
        }

        private void tsBearbeiten_Click(object sender, EventArgs e)
        {
            this.treeView1.SelectedNode.BeginEdit();
        }

        private void tsLoeschen_Click(object sender, EventArgs e)
        {            
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void loadListViewContent(String name, String text)
        {
            listView1.Items.Clear();
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            item = new ListViewItem(text + " " + name, 0);
            subItems = new ListViewItem.ListViewSubItem[]
                {new ListViewItem.ListViewSubItem(item, "Directory"),
                        new ListViewItem.ListViewSubItem(item,
DateTime.Now.ToString())};
            item.SubItems.AddRange(subItems);
            listView1.Items.Add(item);

            //testdata
            listView1.Items.AddRange(new ListViewItem[]{
new ListViewItem("Amy Alberts"),
new ListViewItem("Amy Recker"),
new ListViewItem("Daniel Weisman")
});

            listView1.Columns.Add("Datum", 20, HorizontalAlignment.Left);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListViewItem foundItem = listView1.FindItemWithText(textBox1.Text, false, 0, true);
            if (foundItem != null)
           {
                listView1.TopItem = foundItem;
                listView1.Refresh();
            listView1.Select();
                listView1.Focus();
            }
        }
    }//end class
}//end namespace
