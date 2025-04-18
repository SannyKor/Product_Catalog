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
using Product_Catalog.Models;

namespace WindowsFormsApp1
{
    public partial class MainCatalogForm : Form
    {
        protected Catalog catalog;

        public MainCatalogForm()
        {
            
            catalog = new Catalog(new SqliteStorage());
            InitializeComponent();
            SetupDataGridView();

            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = catalog.Units;
            dataGridView1.Rows.Clear();
            foreach (Unit unit in catalog.Units)
            {
                dataGridView1.Rows.Add(unit.Id, unit.Name, unit.Description, unit.Price, unit.Quantity);
            }


        }     
        private void SetupDataGridView()
        {
            /*dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Назва";
            dataGridView1.Columns[2].Name = "Опис";
            dataGridView1.Columns[3].Name = "Ціна";
            dataGridView1.Columns[4].Name = "Кількість";*/

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Артикул", DataPropertyName = "Id", Name = "Id", Width = 50 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва", DataPropertyName = "Name", Name = "Name", Width = 150 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Опис", DataPropertyName = "Description", Name = "Description", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ціна", DataPropertyName = "Price", Name = "Price", Width = 75 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кількість", DataPropertyName = "Quantity", Name = "Quantity", Width = 75 });
        }

        

        private void buttonSearch_Click(object sender, EventArgs e)
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

        private void buttonAddUnit_Click(object sender, EventArgs e)
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
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = catalog.Units;
            }
        }

        private void textBoxIdSearch_TextChanged(object sender, EventArgs e)
        {
            string input = textBoxIdSearch.Text.Trim();
            dataGridView1.ClearSelection();
            if (int.TryParse(input, out int idToFind))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Id"].Value != null && Convert.ToInt32(row.Cells["Id"].Value) == idToFind)
                    {
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        return;
                    }
                }
               dataGridView1.ClearSelection();
            }
            else
            {
                dataGridView1.ClearSelection();
            }

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[hit.RowIndex].Cells[0];
                    contextMenuStrip1.Show(dataGridView1, e.Location);
                }
            }
        }

        private void toolStripMenuItemEditUnit_Click(object sender, EventArgs e)
        {
            ChangeUnitForm changeUnitForm = new ChangeUnitForm();
            if(changeUnitForm.ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1 != null)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    Unit unit = catalog.GetUnitById(id);
                    if (unit != null)
                    {
                        if (!string.IsNullOrWhiteSpace(changeUnitForm.unitName))
                        {                            
                            unit.Name = changeUnitForm.unitName;
                        }
                        if (!string.IsNullOrWhiteSpace(changeUnitForm.unitDescription))
                        {                           
                            unit.Description = changeUnitForm.unitDescription;
                        }
                        if (changeUnitForm.unitPrice != 0)
                        {                            
                            unit.Price = changeUnitForm.unitPrice;
                        }
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = catalog.Units;
                    }
                }
            }
        }

        private void toolStripMenuItemDelUnit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                DialogResult result = MessageBox.Show(
                    $"Ви видаляете товар: \n\n{id}", "Підтвердження",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    catalog.RemoveUnit(id);                    
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = catalog.Units;
                    MessageBox.Show("Товар видалено");
                }
            }
        }

        private void toolStripMenuItemName_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                Unit unit = catalog.GetUnitById(id);
                if (unit != null)
                {
                    MessageBox.Show($"Назва товару: {unit.Name}");
                }
                else
                {
                    MessageBox.Show("Товар не знайдено");
                }
            }
        }
    }
}
