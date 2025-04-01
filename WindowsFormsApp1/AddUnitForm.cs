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
            unitName = textBoxName.Text;
            unitDescription = textBoxDescription.Text;
            unitPrice = Convert.ToDouble(textBoxPrice.Text);
            unitQuantity = Convert.ToInt32(textBoxQuantity.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
