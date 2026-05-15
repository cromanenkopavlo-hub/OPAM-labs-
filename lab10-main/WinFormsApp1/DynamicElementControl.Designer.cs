using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MyControl : UserControl
    {
        Random rnd = new Random();

        public MyControl()
        {
            InitializeComponent();

            button1.Click += CreateSameElement;
            label1.Click += CreateSameElement;
            textBox1.Click += CreateSameElement;
        }

        private void CreateSameElement(object sender, EventArgs e)
        {
            Control newControl = null;

            Random rand = new Random();

            int x = rand.Next(10, 500);
            int y = rand.Next(10, 300);

            if (sender is Button)
            {
                newControl = new Button();
                newControl.Text = "Button";
                newControl.Size = new Size(100, 30);
            }
            else if (sender is Label)
            {
                newControl = new Label();
                newControl.Text = "Label";
                newControl.AutoSize = true;
            }
            else if (sender is TextBox)
            {
                newControl = new TextBox();
                newControl.Size = new Size(120, 25);
            }

            if (newControl != null)
            {
                newControl.Location = new Point(x, y);

                newControl.Click += CreateSameElement;

                this.Parent.Controls.Add(newControl);
            }
        }
    }
}