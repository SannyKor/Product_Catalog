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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class MainCatalogForm : Form
    {
        private Catalog catalog;

        public MainCatalogForm()
        {
            
            catalog = new Catalog(new StorageFromFile());
            InitializeComponent();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = catalog.Units;


        }        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string findId = requestId_field.Text;
            //Unit unit = catalog.GetUnitById(Convert.ToInt32(findId));
            if (int.TryParse(findId, out int Id))
            {
                Unit unit = catalog.GetUnitById(Id);
                if (unit == null)
                {
                    MessageBox.Show("Товар не знайдено");
                    return;
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = new List<Unit>{unit};
            }
            else
            {
                MessageBox.Show("Введіть коректний артикул товару");
            }

        }

        private void findId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddUnitForm addUnitForm = new AddUnitForm();
            if (addUnitForm.ShowDialog() == DialogResult.OK)
            {
                catalog.AddUnit
                    (
                    addUnitForm.unitName,
                    addUnitForm.unitDescription,
                    addUnitForm.unitPrice,
                    addUnitForm.unitQuantity
                    );
            }
        }
    }
}
