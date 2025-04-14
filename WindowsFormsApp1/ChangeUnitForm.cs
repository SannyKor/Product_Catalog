using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ChangeUnitForm : Form
    {
        public string unitName { get; set; }
        public string unitDescription { get; set; }
        public double unitPrice { get; set; }
        
        public ChangeUnitForm()
        {
            InitializeComponent();
        }

        private void buttonChangeUnit_Click(object sender, EventArgs e)
        {
            unitName = textBoxName.Text;
            unitDescription = textBoxDescription.Text;
            if (string.IsNullOrWhiteSpace(textBoxPrice.Text))
            {
                unitPrice = 0;
            }
            else
            {
                if (!double.TryParse(textBoxPrice.Text, out double parsedPrice))
                {
                    unitPrice = 0;
                }
                else
                {
                    unitPrice = parsedPrice;
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
