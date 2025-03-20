using ClassCatalogLib;

namespace WinFormsApp1
{
    internal class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
    public partial class Form1 : Form
    {
        private void LoadProducts()
        {
            // Список товарів
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук", Price = 25000 },
                new Product { Id = 2, Name = "Смартфон", Price = 15000 },
                new Product { Id = 3, Name = "Монітор", Price = 12000 }
            };

            // Прив’язка до DataGridView
            dataGridView1.DataSource = products;
        }

        public Form1()
        {
            InitializeComponent();
            LoadProducts();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int value1 = (int)numericUpDown1.Value;
            int value2 = (int)numericUpDown2.Value;
            Class1 class1 = new Class1();
            label1.Text = (class1.add(value1, value2)).ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
