using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> myHistory = new List<string>();
        public double GetNumber(string text)
        {
            if (double.TryParse(text, out double s)) return s;
            MessageBox.Show("Введіть число");
            return 0;
        }
        void seyv(string record)
        {
            myHistory.Add(record);
            textBox3.Clear();
            var lastFive = myHistory.Skip(Math.Max(0, myHistory.Count - 5)).ToList();
            foreach (var item in lastFive)
            {
                textBox3.AppendText(item + Environment.NewLine);
            }
        }
        private void onload(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


       
        private void button1_Click(object sender, EventArgs e)
        {
            double s = GetNumber(textBox1.Text);
            double y = GetNumber(textBox2.Text);
            label2.Text = (s + y).ToString();
            double result = s + y;
            seyv($"{s} + {y} = {result}");
        }

       void button3_Click(object sender, EventArgs e)
        {
            double s = GetNumber(textBox1.Text);
            double y = GetNumber(textBox2.Text);
            if (s == 0)
            {
                label2.Text = "Введіть число більше за 0 ";
            }
            else if (y == 0)
            {
                label2.Text = "Введіть число більше за 0 ";
            }
            else  label2.Text = (s / y).ToString();
            double result = s / y;
            seyv($"{s} / {y} = {result}");
        }

       void button2_Click(object sender, EventArgs e)
        {
            double s = GetNumber(textBox1.Text);
            double y = GetNumber(textBox2.Text);
            label2.Text = (s * y).ToString();
            double result = s * y;
            seyv($"{s} * {y} = {result}");
        }

        


        

        void button1_Click_1(object sender, EventArgs e)
        {
            double s = GetNumber(textBox1.Text);
            double y = GetNumber(textBox2.Text);
           
            label2.Text = (s - y).ToString();
            double result = s - y;
            seyv($"{s} - {y} = {result}");
    }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}