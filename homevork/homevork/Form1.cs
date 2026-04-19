using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;



namespace homevork
{
    public partial class Form1 : Form
    {
         List <User> userslist = new List <User> ();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            singupPenal.Visible = false;
           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Chek_Click(object sender, EventArgs e)
        {
            if (singupPenal.Visible)
            {
                singupPenal.Visible = false;
                Loginpenal.Visible = true;
                Chek.Text = "create an acount?";
            }
            else
            {
                singupPenal.Visible = true;
                Loginpenal.Visible = false;
                Chek.Text = "login?";
            }
            
           // singupPenal.Visible = !singupPenal.Visible;
           // Loginpenal.Visible = !Loginpenal.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (singupPenal.Visible)
            {   if (
                    string.IsNullOrWhiteSpace(NametextBox.Text) ||
                    string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PasswordtextBox2.Text) ||
                    string.IsNullOrWhiteSpace(ComafirmtextBox.Text)
                    )
                {
                    MessageBox.Show("Заповніть всі поля");
                }







                if(PasswordtextBox2.Text != ComafirmtextBox.Text)
                {
                    MessageBox.Show("Password or Email does not Woth");
                    return;
                }
                var user = new User
                {
                    Id = 1,
                    Email = EmailTextBox.Text,
                    Password = Convert.ToInt32(PasswordtextBox2.Text),
                };
                userslist.Add(user);
                string directoryPath = "data";
                string filePath = Path.Combine(directoryPath, "users.json");

                // Проверяем, существует ли папка, и создаем её при необходимости
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                //string usersJson = JsonSerializer.Serialize(userslist);
               // File.WriteAllText("data/users.json", usersJson);
                
            }
        }

        private void PasswordtextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComafirmtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
