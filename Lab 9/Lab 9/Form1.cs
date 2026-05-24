using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string alphabet = "";
            int length = 0;

           
            if (rbEasy.Checked) { alphabet = "abcdefghijklmnopqrstuvwxyz"; length = 6; }
            else if (rbMedium.Checked) { alphabet = "abcdefghijklmnopqrstuvwxyz0123456789"; length = 10; }
            else if (rbHard.Checked) { alphabet = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*"; length = 14; }

     
            Random rnd = new Random();
            string pass = "";
            for (int i = 0; i < length; i++)
            {
                pass += alphabet[rnd.Next(alphabet.Length)];
            }
            txtResult.Text = pass;

         
            if (cbSaveToFile.Checked)
            {
                string path = "password.txt";
                List<string> passwords = new List<string>();

                if (File.Exists(path))
                {
                    passwords = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
                }

              
                if (passwords.Count >= 5)
                {
                    passwords.Clear();
                }

                passwords.Add(pass);
                File.WriteAllLines(path, passwords);

                MessageBox.Show($"Пароль додано! У файлі зараз: {passwords.Count}/5");
            }
        }
    }
}