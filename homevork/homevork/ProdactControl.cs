using System;
using System.Drawing;
using System.Windows.Forms;

namespace homevork
{
    public partial class ProdactControl : UserControl
    {
        private Product _product;

        public ProdactControl(Product product)
        {
            InitializeComponent();

            _product = product;

            labelName.Text = product.Name;
            labelPrice.Text = "Price: " + product.Price;
            labelCaloris.Text = "Calories: " + product.Calories;
            labelDate.Text = "Date: " + product.Date;
            labelType.Text = product.Type;

         
            try
            {
                pictureBox1.Load(product.Image);
            }
            catch
            {
                pictureBox1.BackColor = Color.Gray;
            }

            buttonDelete.Click += ButtonDelete_Click;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Form2 form = (Form2)FindForm();
            form.RemoveProduct(this);
        }
    }
}