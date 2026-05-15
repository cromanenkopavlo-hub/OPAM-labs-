namespace WinFormsApp1
{
    partial class MyControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
          
            button1.Location = new Point(20, 20);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 0;
            button1.Text = "Button";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(150, 25);
            label1.Name = "label1";
            label1.Size = new Size(39, 17);
            label1.TabIndex = 1;
            label1.Text = "Label";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(250, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 25);
            textBox1.TabIndex = 2;
            // 
            // MyControl
            // 
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "MyControl";
            Size = new Size(400, 80);
            Load += MyControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Label label1;
        private TextBox textBox1;

        private void MyControl_Load(object sender, EventArgs e)
        {

        }
    }
}