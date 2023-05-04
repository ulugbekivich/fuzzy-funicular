using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;

namespace CurrencyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        SQLiteConnection conn = new SQLiteConnection("Data Source=valyuta.db;Version=3;");
        private DataGridViewRow previouslySelectedRow;


        public class Valyuta
        {
            public string title { get; set; } = string.Empty;

            public double cb_price { get; set; }

            public string date { get; set; } = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = GetAll(conn);
            int i = 1;
            foreach (var item in result)
            {
                string[] row = { i.ToString(), item.title, item.cb_price.ToString(), item.date };
                dataGridView1.Rows.Add(row);
                i++;
            }
        }

        private List<Valyuta> GetAll(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "SELECT * FROM valyuta";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Valyuta> currencies = new List<Valyuta>();
            while (reader.Read())
            {
                Valyuta c = new Valyuta();
                c.title = reader.GetString(0);
                c.cb_price = reader.GetDouble(1);
                c.date = reader.GetString(2);
                currencies.Add(c);
            }

            conn.Close();
            return currencies;
        }

        private List<Valyuta> Filter(SQLiteConnection conn, string Title, string Price, string Date)
        {
            conn.Open();
            List<Valyuta> currencies = new List<Valyuta>();
            if (!DateTime.TryParseExact(maskedTextBox1.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Date = "";
            }
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Price) && !string.IsNullOrEmpty(Date))
            {
                string sql = "SELECT * FROM valyuta WHERE title LIKE @title AND CAST(price AS TEXT) LIKE @price AND date LIKE @date";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", Title + "%");
                cmd.Parameters.AddWithValue("@price", Price + "%");
                cmd.Parameters.AddWithValue("@date", Date + "%");

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Valyuta c = new Valyuta();
                        c.title = reader.GetString(0);
                        c.cb_price = reader.GetDouble(1);
                        c.date = reader.GetString(2);
                        currencies.Add(c);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Price))
            {
                string sql = "SELECT * FROM valyuta WHERE title LIKE @title AND CAST(price AS TEXT) LIKE @price";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", Title + "%");
                cmd.Parameters.AddWithValue("@price", Price + "%");

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Valyuta c = new Valyuta();
                        c.title = reader.GetString(0);
                        c.cb_price = reader.GetDouble(1);
                        c.date = reader.GetString(2);
                        currencies.Add(c);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Title))
            {
                string sql = "SELECT * FROM valyuta WHERE title LIKE @title || '%'";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", Title);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Valyuta c = new Valyuta();
                        c.title = reader.GetString(0);
                        c.cb_price = reader.GetDouble(1);
                        c.date = reader.GetString(2);
                        currencies.Add(c);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Price))
            {
                string sql = "SELECT * FROM valyuta WHERE price LIKE @price || '%'";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@price", Price);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Valyuta c = new Valyuta();
                        c.title = reader.GetString(0);
                        c.cb_price = reader.GetDouble(1);
                        c.date = reader.GetString(2);
                        currencies.Add(c);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Date))
            {
                string sql = "SELECT * FROM valyuta WHERE date LIKE @date";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", Date);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Valyuta c = new Valyuta();
                        c.title = reader.GetString(0);
                        c.cb_price = reader.GetDouble(1);
                        c.date = reader.GetString(2);
                        currencies.Add(c);
                    }
                }
            }

            conn.Close();
            return currencies;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (DateTime.TryParseExact(maskedTextBox1.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                UpdateDataGridView();
            }
        }
        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            var result = Filter(conn, textBox1.Text, textBox2.Text, maskedTextBox1.Text.ToString());
            int i = 1;
            foreach (var item in result)
            {
                string[] row = { i.ToString(), item.title, item.cb_price.ToString(), item.date };
                dataGridView1.Rows.Add(row);
                i++;
            }
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
                    string id = clickedRow.Cells[0].Value.ToString()!;
                    string title = clickedRow.Cells[1].Value.ToString()!;
                    string price = clickedRow.Cells[2].Value.ToString()!;
                    string date = clickedRow.Cells[3].Value.ToString()!;

                    label6.Text = id;
                    label7.Text = title;
                    label8.Text = price;
                    label9.Text = date;

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}