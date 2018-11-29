/*
https://mycsharp.de/wbb2/thread.php?postid=155734
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KRTool.View
{
    public class LoadOnDemandTreeView : System.Windows.Forms.TreeView
    {
        private bool _blnFakeNodes=true;
        public LoadOnDemandTreeView()
        {
        }

        public bool FakeNodes
        {
            get{return this._blnFakeNodes;}
            set{this._blnFakeNodes=value;}
        }

        public bool FakeNode(TreeNode pTreeNodeToFake)
        {
            bool ret=false;

            if (pTreeNodeToFake!=null && !pTreeNodeToFake.IsExpanded && pTreeNodeToFake.Nodes.Count==0)
            {
                TreeNode ndFake=new TreeNode("fake");
                ndFake.Tag="fake";
                pTreeNodeToFake.Nodes.Add(ndFake);
                ret=true;
            }
            return ret;
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            if(_blnFakeNodes && e.Node.Nodes.Count>0 && e.Node.Nodes[0].Tag!=null && e.Node.Nodes[0].Tag.ToString()=="fake"  )
            {
                e.Node.Nodes.Clear();
                this.OnLoadData(e);
            }
            base.OnBeforeExpand (e);
        }
        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            base.OnAfterCollapse (e);
            if (_blnFakeNodes)
            {
                e.Node.Nodes.Clear();
                TreeNode ndFake=new TreeNode("fake");
                ndFake.Tag="fake";
                e.Node.Nodes.Add(ndFake);
            }
        }

        private void OnLoadData(TreeViewCancelEventArgs e)
        {
            if (this.LoadData!=null)
            {
                this.LoadData(this,e);
            }
        }

        public event TreeViewCancelEventHandler LoadData;
    }
}
