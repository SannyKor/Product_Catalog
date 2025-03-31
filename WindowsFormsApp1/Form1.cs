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
    public partial class Form1 : Form
    {
        private Catalog catalog;

        public Form1()
        {
            Storage storage = new StorageFromFile();
            catalog = new Catalog(storage);
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
            Unit unit = catalog.GetUnitById(Convert.ToInt32(findId));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = unit;

        }

        private void findId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
