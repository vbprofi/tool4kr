using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KRTool.Controller;
using KRTool.Model;

namespace KRTool.View
{
    public partial class Form1 : Form, Controller.iAusgabe
    {
        public Form1()
        {
            InitializeComponent();
        }

        AusgabeController _controller;

        public void SetController(AusgabeController controller)
        {
            _controller = controller;
        }

        public void AddToAusgabe(string usr)
        {
            txtAusgabe.Text = usr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
