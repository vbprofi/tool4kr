namespace KRTool.View
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.MenuItemDatei = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBeenden = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFragezeichen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHP = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new KRTool.View.LoadOnDemandTreeView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.tsBearbeiten = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLoeschen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            //this.contextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemDatei,
            this.MenuItemFragezeichen});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 33);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "Menü";
            // 
            // MenuItemDatei
            // 
            this.MenuItemDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBeenden});
            this.MenuItemDatei.Name = "MenuItemDatei";
            this.MenuItemDatei.Size = new System.Drawing.Size(65, 29);
            this.MenuItemDatei.Text = "&Datei";
            // 
            // MenuItemBeenden
            // 
            this.MenuItemBeenden.Name = "MenuItemBeenden";
            this.MenuItemBeenden.Size = new System.Drawing.Size(252, 30);
            this.MenuItemBeenden.Text = "&Beenden";
            // 
            // MenuItemFragezeichen
            // 
            this.MenuItemFragezeichen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemHP,
            this.MenuItemInfo});
            this.MenuItemFragezeichen.Name = "MenuItemFragezeichen";
            this.MenuItemFragezeichen.Size = new System.Drawing.Size(32, 29);
            this.MenuItemFragezeichen.Text = "&?";
            // 
            // MenuItemHP
            // 
            this.MenuItemHP.Name = "MenuItemHP";
            this.MenuItemHP.Size = new System.Drawing.Size(185, 30);
            this.MenuItemHP.Text = "&Homepage";
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(185, 30);
            this.MenuItemInfo.Text = "&Info";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenu1;
            this.treeView1.FakeNodes = true;
            this.treeView1.Location = new System.Drawing.Point(0, -1);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 1;
            this.treeView1.LoadData += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_LoadData);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenu1
            // 
            this.contextMenu1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBearbeiten,
            this.tsLoeschen});
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(241, 97);
            // 
            // tsBearbeiten
            // 
            this.tsBearbeiten.Name = "tsBearbeiten";
            this.tsBearbeiten.Size = new System.Drawing.Size(243, 30);
            this.tsBearbeiten.Text = "&Bearbeiten";
            this.tsBearbeiten.Click += new System.EventHandler(this.tsBearbeiten_Click);
            // 
            // tsLoeschen
            // 
            this.tsLoeschen.Name = "tsLoeschen";
            this.tsLoeschen.Size = new System.Drawing.Size(240, 30);
            this.tsLoeschen.Text = "&Löschen";
            this.tsLoeschen.Click += new System.EventHandler(this.tsLoeschen_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.contextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDatei;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFragezeichen;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBeenden;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHP;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private LoadOnDemandTreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem tsBearbeiten;
        private System.Windows.Forms.ToolStripMenuItem tsLoeschen;
    }
}