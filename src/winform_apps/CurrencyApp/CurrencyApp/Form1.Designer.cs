namespace CurrencyApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            maskedTextBox1 = new MaskedTextBox();
            panel1 = new Panel();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.SelectionBackColor = Color.RoyalBlue;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.Menu;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, Title, Price, Date });
            dataGridView1.Location = new Point(57, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(676, 331);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.Width = 70;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.Width = 190;
            // 
            // Price
            // 
            Price.HeaderText = "Price";
            Price.Name = "Price";
            Price.Width = 180;
            // 
            // Date
            // 
            Date.HeaderText = "Date";
            Date.Name = "Date";
            Date.Width = 175;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(190, 19);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Title";
            textBox1.Size = new Size(170, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(382, 19);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Price";
            textBox2.Size = new Size(152, 23);
            textBox2.TabIndex = 2;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(566, 19);
            maskedTextBox1.Mask = "00.00.0000";
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(131, 23);
            maskedTextBox1.TabIndex = 3;
            maskedTextBox1.ValidatingType = typeof(DateTime);
            maskedTextBox1.MaskInputRejected += maskedTextBox1_MaskInputRejected;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Menu;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(774, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(268, 330);
            panel1.TabIndex = 4;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semilight", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(89, 237);
            label9.Name = "label9";
            label9.Size = new Size(0, 25);
            label9.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semilight", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(89, 191);
            label8.Name = "label8";
            label8.Size = new Size(0, 25);
            label8.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semilight", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(89, 142);
            label7.Name = "label7";
            label7.Size = new Size(0, 25);
            label7.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semilight", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(89, 102);
            label6.Name = "label6";
            label6.Size = new Size(0, 25);
            label6.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(35, 237);
            label5.Name = "label5";
            label5.Size = new Size(48, 21);
            label5.TabIndex = 4;
            label5.Text = "Date:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(35, 191);
            label4.Name = "label4";
            label4.Size = new Size(50, 21);
            label4.TabIndex = 3;
            label4.Text = "Price:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(35, 146);
            label3.Name = "label3";
            label3.Size = new Size(46, 21);
            label3.TabIndex = 2;
            label3.Text = "Title:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(52, 105);
            label2.Name = "label2";
            label2.Size = new Size(29, 21);
            label2.TabIndex = 1;
            label2.Text = "Id:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(89, 45);
            label1.Name = "label1";
            label1.Size = new Size(76, 25);
            label1.TabIndex = 0;
            label1.Text = "Valyuta";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1084, 451);
            Controls.Add(panel1);
            Controls.Add(maskedTextBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Currency App";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private MaskedTextBox maskedTextBox1;
        private Panel panel1;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Date;
    }
}