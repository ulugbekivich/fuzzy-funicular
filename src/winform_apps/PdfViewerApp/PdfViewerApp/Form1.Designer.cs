namespace PdfViewerApp
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
            pdfViewer1 = new PdfiumViewer.PdfViewer();
            dataGridView1 = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pdfViewer1
            // 
            pdfViewer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pdfViewer1.BorderStyle = BorderStyle.FixedSingle;
            pdfViewer1.Location = new Point(739, 52);
            pdfViewer1.Margin = new Padding(4, 3, 4, 3);
            pdfViewer1.Name = "pdfViewer1";
            pdfViewer1.Size = new Size(422, 497);
            pdfViewer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Title });
            dataGridView1.Location = new Point(12, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(646, 497);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.Width = 640;
            // 
            // button2
            // 
            button2.Location = new Point(12, 16);
            button2.Name = "button2";
            button2.Size = new Size(95, 23);
            button2.TabIndex = 3;
            button2.Text = "Search local";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(143, 16);
            button1.Name = "button1";
            button1.Size = new Size(95, 23);
            button1.TabIndex = 4;
            button1.Text = "Search set";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(284, 16);
            button3.Name = "button3";
            button3.Size = new Size(95, 23);
            button3.TabIndex = 5;
            button3.Text = "Export Excel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(457, 16);
            button4.Name = "button4";
            button4.Size = new Size(95, 23);
            button4.TabIndex = 6;
            button4.Text = "Search local 2";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(pdfViewer1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PdfiumViewer.PdfViewer pdfViewer1;
        private DataGridView dataGridView1;
        private Button button2;
        private DataGridViewTextBoxColumn Title;
        private Button button1;
        private Button button3;
        private Button button4;
    }
}