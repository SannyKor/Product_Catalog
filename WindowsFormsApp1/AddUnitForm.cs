using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassCatalog;

namespace WindowsFormsApp1
{
    public partial class AddUnitForm : Form
    {
        public string unitName { get; set; }
        public string unitDescription { get; set; }
        public double unitPrice { get; set; }
        public int unitQuantity { get; set; }

        public AddUnitForm()
        {
            
            InitializeComponent();
        }

        private void AddUnitForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddUnit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                unitName = textBoxName.Text;
            }
            else
            {
                unitName = "No name";
            }

            if (!string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                unitDescription = textBoxDescription.Text;
            }
            else
            {
                unitDescription = "Empty";
            }

            if (string.IsNullOrWhiteSpace(textBoxPrice.Text))
            {
                MessageBox.Show("Введіть ціну товару");
            }
            else
            {
                if (!double.TryParse(textBoxPrice.Text, out double parsedPrice))
                {
                    MessageBox.Show("Введіть коректну ціну товару");
                }
                else
                {
                    unitPrice = parsedPrice;
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text))
            {
                MessageBox.Show("Вкажіть кількість товару");
            }
            else
            {
                if (!int.TryParse(textBoxQuantity.Text, out int parsedQuantity))
                {
                    MessageBox.Show("Введіть коректну кількість товару");
                }
                else
                {
                    unitQuantity = parsedQuantity;
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
