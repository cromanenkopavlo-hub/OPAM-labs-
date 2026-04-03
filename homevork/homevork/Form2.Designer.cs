namespace homevork
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPrise = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonFruit = new System.Windows.Forms.RadioButton();
            this.radioButtonVegetable = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.textBoxCaloris = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoximage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(25, 37);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prise";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxPrise
            // 
            this.textBoxPrise.Location = new System.Drawing.Point(25, 160);
            this.textBoxPrise.Name = "textBoxPrise";
            this.textBoxPrise.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrise.TabIndex = 4;
            this.textBoxPrise.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Type";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // radioButtonFruit
            // 
            this.radioButtonFruit.AutoSize = true;
            this.radioButtonFruit.Location = new System.Drawing.Point(25, 213);
            this.radioButtonFruit.Name = "radioButtonFruit";
            this.radioButtonFruit.Size = new System.Drawing.Size(45, 17);
            this.radioButtonFruit.TabIndex = 6;
            this.radioButtonFruit.TabStop = true;
            this.radioButtonFruit.Text = "Fruit";
            this.radioButtonFruit.UseVisualStyleBackColor = true;
            this.radioButtonFruit.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonVegetable
            // 
            this.radioButtonVegetable.AutoSize = true;
            this.radioButtonVegetable.Location = new System.Drawing.Point(25, 236);
            this.radioButtonVegetable.Name = "radioButtonVegetable";
            this.radioButtonVegetable.Size = new System.Drawing.Size(79, 17);
            this.radioButtonVegetable.TabIndex = 7;
            this.radioButtonVegetable.TabStop = true;
            this.radioButtonVegetable.Text = "Vegetapble";
            this.radioButtonVegetable.UseVisualStyleBackColor = true;
            this.radioButtonVegetable.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Date Expari";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "caloris";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(37, 309);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(100, 20);
            this.textBoxData.TabIndex = 10;
            this.textBoxData.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBoxCaloris
            // 
            this.textBoxCaloris.Location = new System.Drawing.Point(36, 352);
            this.textBoxCaloris.Name = "textBoxCaloris";
            this.textBoxCaloris.Size = new System.Drawing.Size(100, 20);
            this.textBoxCaloris.TabIndex = 11;
            this.textBoxCaloris.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Image";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBoximage
            // 
            this.textBoximage.Location = new System.Drawing.Point(25, 99);
            this.textBoximage.Name = "textBoximage";
            this.textBoximage.Size = new System.Drawing.Size(100, 20);
            this.textBoximage.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 35);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(225, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(370, 383);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoximage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxCaloris);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioButtonVegetable);
            this.Controls.Add(this.radioButtonFruit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPrise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPrise;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonFruit;
        private System.Windows.Forms.RadioButton radioButtonVegetable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.TextBox textBoxCaloris;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoximage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}