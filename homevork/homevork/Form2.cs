using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace homevork
{
    public struct Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public int Calories { get; set; }
    }
    public partial class Form2 : Form
    {
        List<ProdactControl> products = new List<ProdactControl>();
        public Form2()
        {
            InitializeComponent();
            radioButtonFruit.Checked = true;
        }
        #region
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            double price;
            int calories;

            if (!double.TryParse(textBoxPrise.Text, out price))
            {
                MessageBox.Show("Неправильна ціна");
                return;
            }

            if (!int.TryParse(textBoxCaloris.Text, out calories))
            {
                MessageBox.Show("Неправильні калорії");
                return;
            }

            var product = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = textBoxName.Text,
                Price = price,
                Type = radioButtonFruit.Checked ? "Fruit" : "Vegetables",
                Date = textBoxData.Text,
                Calories = calories,
                Image = textBoximage.Text
            };

            var control = new ProdactControl(product);
            products.Add(control);
            flowLayoutPanel1.Controls.Add(control);
        }
        public void RemoveProduct(ProdactControl control)
        {
            products.Remove(control);
            flowLayoutPanel1.Controls.Remove(control);
        }
    }
}

