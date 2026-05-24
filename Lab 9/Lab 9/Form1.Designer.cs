namespace Lab_9
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.rbEasy = new System.Windows.Forms.RadioButton();
            this.rbMedium = new System.Windows.Forms.RadioButton();
            this.rbHard = new System.Windows.Forms.RadioButton();
            this.cbSaveToFile = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rbEasy
            // 
            this.rbEasy.AutoSize = true;
            this.rbEasy.Location = new System.Drawing.Point(30, 30);
            this.rbEasy.Name = "rbEasy";
            this.rbEasy.Size = new System.Drawing.Size(110, 17);
            this.rbEasy.TabIndex = 0;
            this.rbEasy.Text = "Легкий (6 букв)";
            this.rbEasy.UseVisualStyleBackColor = true;
            // 
            // rbMedium
            // 
            this.rbMedium.AutoSize = true;
            this.rbMedium.Checked = true;
            this.rbMedium.Location = new System.Drawing.Point(30, 60);
            this.rbMedium.Name = "rbMedium";
            this.rbMedium.Size = new System.Drawing.Size(125, 17);
            this.rbMedium.TabIndex = 1;
            this.rbMedium.TabStop = true;
            this.rbMedium.Text = "Середній (10 симв.)";
            this.rbMedium.UseVisualStyleBackColor = true;
            // 
            // rbHard
            // 
            this.rbHard.AutoSize = true;
            this.rbHard.Location = new System.Drawing.Point(30, 90);
            this.rbHard.Name = "rbHard";
            this.rbHard.Size = new System.Drawing.Size(130, 17);
            this.rbHard.TabIndex = 2;
            this.rbHard.Text = "Складний (14 симв.)";
            this.rbHard.UseVisualStyleBackColor = true;
            // 
            // cbSaveToFile
            // 
            this.cbSaveToFile.AutoSize = true;
            this.cbSaveToFile.Location = new System.Drawing.Point(30, 130);
            this.cbSaveToFile.Name = "cbSaveToFile";
            this.cbSaveToFile.Size = new System.Drawing.Size(180, 17);
            this.cbSaveToFile.TabIndex = 3;
            this.cbSaveToFile.Text = "Зберегти в password.txt ";
            this.cbSaveToFile.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(30, 170);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(220, 40);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "ЗГЕНЕРУВАТИ";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(30, 230);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(220, 20);
            this.txtResult.TabIndex = 5;
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 281);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cbSaveToFile);
            this.Controls.Add(this.rbHard);
            this.Controls.Add(this.rbMedium);
            this.Controls.Add(this.rbEasy);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор паролів";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbEasy;
        private System.Windows.Forms.RadioButton rbMedium;
        private System.Windows.Forms.RadioButton rbHard;
        private System.Windows.Forms.CheckBox cbSaveToFile;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtResult;
    }
}