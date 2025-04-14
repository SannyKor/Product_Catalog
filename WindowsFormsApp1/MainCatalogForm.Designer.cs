namespace WindowsFormsApp1
{
    partial class MainCatalogForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.requestId_field = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEditUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddUnit = new System.Windows.Forms.Button();
            this.textBoxIdSearch = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // requestId_field
            // 
            this.requestId_field.Location = new System.Drawing.Point(12, 65);
            this.requestId_field.Name = "requestId_field";
            this.requestId_field.Size = new System.Drawing.Size(123, 22);
            this.requestId_field.TabIndex = 3;
            this.requestId_field.TextChanged += new System.EventHandler(this.findId_TextChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(123, 47);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Пошук";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 195);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(934, 352);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditUnit,
            this.toolStripMenuItemDelUnit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 80);
            // 
            // toolStripMenuItemEditUnit
            // 
            this.toolStripMenuItemEditUnit.Name = "toolStripMenuItemEditUnit";
            this.toolStripMenuItemEditUnit.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItemEditUnit.Text = "Редагувати";
            this.toolStripMenuItemEditUnit.Click += new System.EventHandler(this.toolStripMenuItemEditUnit_Click);
            // 
            // toolStripMenuItemDelUnit
            // 
            this.toolStripMenuItemDelUnit.Name = "toolStripMenuItemDelUnit";
            this.toolStripMenuItemDelUnit.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItemDelUnit.Text = "Видалити";
            this.toolStripMenuItemDelUnit.Click += new System.EventHandler(this.toolStripMenuItemDelUnit_Click);
            // 
            // buttonAddUnit
            // 
            this.buttonAddUnit.Location = new System.Drawing.Point(177, 12);
            this.buttonAddUnit.Name = "buttonAddUnit";
            this.buttonAddUnit.Size = new System.Drawing.Size(123, 47);
            this.buttonAddUnit.TabIndex = 11;
            this.buttonAddUnit.Text = "Додати товар";
            this.buttonAddUnit.UseVisualStyleBackColor = true;
            this.buttonAddUnit.Click += new System.EventHandler(this.buttonAddUnit_Click);
            // 
            // textBoxIdSearch
            // 
            this.textBoxIdSearch.Location = new System.Drawing.Point(80, 169);
            this.textBoxIdSearch.Name = "textBoxIdSearch";
            this.textBoxIdSearch.Size = new System.Drawing.Size(75, 22);
            this.textBoxIdSearch.TabIndex = 12;
            this.textBoxIdSearch.TextChanged += new System.EventHandler(this.textBoxIdSearch_TextChanged);
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(12, 172);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(65, 16);
            this.labelId.TabIndex = 13;
            this.labelId.Text = "Артикул:";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // MainCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 559);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.textBoxIdSearch);
            this.Controls.Add(this.buttonAddUnit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.requestId_field);
            this.Name = "MainCatalogForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox requestId_field;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonAddUnit;
        private System.Windows.Forms.TextBox textBoxIdSearch;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditUnit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelUnit;
    }
}

