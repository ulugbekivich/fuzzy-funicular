using PdfiumViewer;
using System.IO.Compression;

namespace PdfViewerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DataGridViewRow previouslySelectedRow;

        private void button2_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\A'zamjon\Desktop\docs";
            string[] files = Directory.GetFiles(folderPath);

            List<string> pdfFiles = new List<string>();

            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".zip")
                {
                    using (ZipArchive archive = ZipFile.OpenRead(file))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.Name.ToLower() == "preview.pdf")
                            {
                                string pdfFilePath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(file), entry.Name);
                                if (!Directory.Exists(Path.GetDirectoryName(pdfFilePath)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(pdfFilePath));
                                }
                                using (var stream = entry.Open())
                                {
                                    using (var fileStream = File.Create(pdfFilePath))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                }
                                pdfFiles.Add(pdfFilePath);
                            }
                        }
                    }
                }
            }

            foreach (string file in pdfFiles)
            {
                dataGridView1.Rows.Add(file);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {

                DataGridViewRow clickedRow = dataGridView1.Rows[rowIndex];

                clickedRow.DefaultCellStyle.BackColor = Color.RoyalBlue;

                if (previouslySelectedRow != null)
                {
                    previouslySelectedRow.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                }

                previouslySelectedRow = clickedRow;


                try
                {
                    string file = clickedRow.Cells[0].Value.ToString()!;
                    if (Path.GetExtension(file).ToLower() == ".pdf")
                    {
                        var pdf = PdfDocument.Load(file);
                        pdfViewer1.Document = pdf;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}