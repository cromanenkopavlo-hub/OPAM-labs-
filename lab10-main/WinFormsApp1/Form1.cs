using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MyControl mc = new MyControl();

            mc.Location = new System.Drawing.Point(10, 10);

            this.Controls.Add(mc);
        }
    }
}