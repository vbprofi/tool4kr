namespace KRTool.View
{
    partial class Form1
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
            this.labelAusgabe = new System.Windows.Forms.Label();
            this.txtAusgabe = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelAusgabe
            // 
            this.labelAusgabe.AutoSize = true;
            this.labelAusgabe.Location = new System.Drawing.Point(0, -1);
            this.labelAusgabe.Name = "labelAusgabe";
            this.labelAusgabe.Size = new System.Drawing.Size(77, 20);
            this.labelAusgabe.TabIndex = 0;
            this.labelAusgabe.Text = "&Ausgabe:";
            // 
            // txtAusgabe
            // 
            this.txtAusgabe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAusgabe.Location = new System.Drawing.Point(0, 0);
            this.txtAusgabe.Multiline = true;
            this.txtAusgabe.Name = "txtAusgabe";
            this.txtAusgabe.ReadOnly = true;
            this.txtAusgabe.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAusgabe.Size = new System.Drawing.Size(800, 450);
            this.txtAusgabe.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtAusgabe);
            this.Controls.Add(this.labelAusgabe);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAusgabe;
        private System.Windows.Forms.TextBox txtAusgabe;
    }
}