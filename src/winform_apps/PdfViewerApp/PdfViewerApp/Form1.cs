using PdfiumViewer;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;

namespace PdfViewerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SQLiteConnection conn = new SQLiteConnection("Data Source=files.db;Version=3;");
        private DataGridViewRow previouslySelectedRow;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("files.db"))
            {
                CreateTable(conn);
            }
        }

        private void CreateTable(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "CREATE TABLE files (title TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private List<string> ReadData(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "SELECT * FROM files";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            List<string> files = new List<string>();
            while (reader.Read())
            {
                files.Add(reader.GetString(0));
            }

            conn.Close();
            return files;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\A'zamjon\Desktop\docs";
            string[] files = Directory.GetFiles(folderPath);

            List<string> pdfFiles = new List<string>();
            dataGridView1.Rows.Clear();
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
            var dataFiles = ReadData(conn);
            int i = 0;
            foreach (string file in dataFiles)
            {
                string s = file.Replace('`', '\'');
                if (pdfFiles.Contains(s))
                {
                    dataGridView1.Rows.Add(s);
                }
                else
                {
                    dataGridView1.Rows.Add(s);
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                i++;
            }
        }

        private void InsertData(List<string> files)
        {
            var dataFiles = ReadData(conn);

            conn.Open();
            string sql = "INSERT INTO files (title) VALUES (@title)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            foreach (string file in files)
            {
                if (!dataFiles.Contains(file))
                {
                    string s = file.Replace('\'', '`');
                    cmd.Parameters.AddWithValue("@title", s);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
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
                    MessageBox.Show("Fayl o'chirib yuborilgan bo'lishi mumkin!", "Fayl topilmadi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\DESKTOP-3JTTA9A\docs";
            string[] files = Directory.GetFiles(folderPath);

            List<string> pdfFiles = new List<string>();
            dataGridView1.Rows.Clear();
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

        private void SaveDataGridViewToCSV(DataGridView dataGridView)
        {

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            sw.Write(dataGridView.Columns[i].HeaderText);
                            if (i < dataGridView.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.Write(sw.NewLine);

                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            for (int i = 0; i < dataGridView.Columns.Count; i++)
                            {
                                if (row.Cells[i].Value != null)
                                    sw.Write(row.Cells[i].Value.ToString());
                                if (i < dataGridView.Columns.Count - 1)
                                    sw.Write(",");
                            }
                            sw.Write(sw.NewLine);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaveDataGridViewToCSV(dataGridView1 as DataGridView);
            
        }

        public DataTable GetAllDataFromTable()
        {
            DataTable dt = new DataTable();

            try
            {
                conn.Open();

                string query = "SELECT * FROM files";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // exception
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }


        private void button4_Click(object sender, EventArgs e)
        {

            DataTable myData = GetAllDataFromTable();
            if (myData != null)
            {
                string folderPath = @"C:\Users\A'zamjon\Desktop\docs";
                string[] files = Directory.GetFiles(folderPath);

                List<string> pdfFiles = new List<string>();
                dataGridView1.Rows.Clear();
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
                                    string pdfFile = pdfFilePath.Replace('\'', '`');
                                    pdfFiles.Add(pdfFile);
                                }
                            }
                        }
                    }
                }

                DataView dv = new DataView(myData);

                dv.RowFilter = $"NOT title IN ('{string.Join("','", pdfFiles)}')";

                dataGridView1.Rows.Clear();

                foreach (DataRow row in myData.Rows)
                {
                    dv.Sort = "Title ASC";
                    string search = row["title"].ToString()!;
                    int index = dv.Find(search);
                    if (index >= 0)
                    {
                        search = search.Replace('`', '\'');
                        dataGridView1.Rows.Add(search);
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        search = search.Replace('`', '\'');
                        dataGridView1.Rows.Add(search);
                    }
                }
            }
        }
    }
}