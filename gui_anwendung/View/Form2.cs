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
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                // check whether each parent node has child nodes
                if (node.IsExpanded && node.Nodes.Count > 0)
                {

                    // iterate through child nodes in the collection
                    foreach (TreeNode nd in node.Nodes)
                    {
                        if (nd.IsSelected)
                            this.treeView1.SelectedNode.Remove();
                    }
                }
            }
        }


    }//end class
}//end namespace
