using Newtonsoft.Json;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using static ValyutaKurslari.Form1;

namespace ValyutaKurslari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string path = "https://nbu.uz/uz/exchange-rates/json/";
        // create the connection to the database
        SQLiteConnection conn = new SQLiteConnection("Data Source=valyuta.db;Version=3;");
        SQLiteConnection connn = new SQLiteConnection("Data Source=valyuta2.db;Version=3;");

        public class Valyuta
        {
            public string title { get; set; } = string.Empty;

            public double cb_price { get; set; }

            public string date { get; set; } = string.Empty;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var valyuta = await ValyutaOlish(path);
            var result1 = ConvertToValyuta(valyuta);
            int i = 1;
            foreach (var item in result1)
            {
                string[] row = { i.ToString(), item.title, item.cb_price.ToString(), item.date };
                guna2DataGridView1.Rows.Add(row);
                i++;
            }


            if (!File.Exists("valyuta.db"))
            {
                CreateTable(conn);
            }

            if (!File.Exists("valyuta2.db"))
            {
                CreateTable2(connn);
            }

            /*for (int J = 0; J < 450; J++)
            {
                conn.Open();    
                var valyut = await ValyutaOlish(path);
                var result = ConvertToValyuta(valyut);
                
                //TruncateDatabase(conn);
                //guna2DataGridView2.Rows.Clear();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                StringBuilder sqlBuilder = new StringBuilder("INSERT INTO valyuta (title, price, date) VALUES ");
                foreach (Valyuta item in result)
                {
                    sqlBuilder.Append($"('{item.title}', {item.cb_price}, '{item.date}'), ");
                }

                sqlBuilder.Remove(sqlBuilder.Length - 2, 2);

                string sql = sqlBuilder.ToString();

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                stopwatch.Stop();
                label4.Text = stopwatch.ElapsedMilliseconds.ToString();
                conn.Close();
            }*/
        }

        private void CreateTable(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "CREATE TABLE valyuta (title TEXT, price REAL, date TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void CreateTable2(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "CREATE TABLE valyuta2 (title TEXT, price REAL, date TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private bool InsertData(SQLiteConnection conn)
        {
            /*var valyuta = await ValyutaOlish(path);
            var result = ConvertToValyuta(valyuta);*/

            var result = ReadData(conn);
            connn.Open();

            string sql = "INSERT INTO valyuta2 (title, price, date) VALUES (@title, @price, @date)";
            SQLiteCommand cmd = new SQLiteCommand(sql, connn);

            /*TruncateDatabase(conn);
            guna2DataGridView2.Rows.Clear();*/
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var item in result)
            {
                cmd.Parameters.AddWithValue("@title", item.title);
                cmd.Parameters.AddWithValue("@price", item.cb_price);
                cmd.Parameters.AddWithValue("@date", item.date);
                cmd.ExecuteNonQuery();
            }

            stopwatch.Stop();
            label1.Text = stopwatch.ElapsedMilliseconds.ToString();
            connn.Close();
            return true;
        }

        private List<Valyuta> ReadData(SQLiteConnection conn)
        {
            conn.Open();

            string sql = "SELECT * FROM valyuta";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            guna2DataGridView2.Rows.Clear();
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

        private void TruncateDatabase(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM valyuta";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "VACUUM";
            sqlite_cmd.ExecuteNonQuery();
        }

        public async Task<string> ValyutaOlish(string path)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            var content = new MultipartFormDataContent();
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public List<Valyuta> ConvertToValyuta(string valyuta)
        {
            var json = JsonConvert.DeserializeObject<List<Valyuta>>(valyuta);
            return json!;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var valyuta = await ValyutaOlish(path);
            var result1 = ConvertToValyuta(valyuta);
            int i = 1;
            foreach (var item in result1)
            {
                string[] row = { i.ToString(), item.title, item.cb_price.ToString(), item.date };
                guna2DataGridView1.Rows.Add(row);
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertData(conn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result2 = ReadData(conn);
            int i = 1;
            foreach (var item in result2)
            {
                string[] row = { i.ToString(), item.title, item.cb_price.ToString(), item.date };
                guna2DataGridView2.Rows.Add(row);
                i++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*var valyuta = await ValyutaOlish(path);
            var result = ConvertToValyuta(valyuta);*/

            var result = ReadData(conn);
            connn.Open();
            /*
            TruncateDatabase(conn);
            guna2DataGridView2.Rows.Clear();*/
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var transaction = connn.BeginTransaction())
            {
                string sql = "INSERT INTO valyuta2 (title, price, date) VALUES (@title, @price, @date)";
                using (var cmd = new SQLiteCommand(sql, connn))
                {
                    cmd.Parameters.Add("@title", DbType.String);
                    cmd.Parameters.Add("@price", DbType.Double);
                    cmd.Parameters.Add("@date", DbType.String);
                    foreach (var item in result)
                    {
                        cmd.Parameters["@title"].Value = item.title;
                        cmd.Parameters["@price"].Value = item.cb_price;
                        cmd.Parameters["@date"].Value = item.date;
                        cmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }

            stopwatch.Stop();
            label2.Text = stopwatch.ElapsedMilliseconds.ToString();
            connn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*var valyuta = await ValyutaOlish(path);
            var result = ConvertToValyuta(valyuta);*/

            var result = ReadData(conn);
            connn.Open();

            //TruncateDatabase(conn);
            //guna2DataGridView2.Rows.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            StringBuilder sqlBuilder = new StringBuilder("INSERT INTO valyuta2 (title, price, date) VALUES ");
            foreach (Valyuta item in result)
            {
                sqlBuilder.Append($"('{item.title}', {item.cb_price}, '{item.date}'), ");
            }

            sqlBuilder.Remove(sqlBuilder.Length - 2, 2);

            string sql = sqlBuilder.ToString();

            SQLiteCommand cmd = new SQLiteCommand(sql, connn);
            cmd.ExecuteNonQuery();

            stopwatch.Stop();
            label4.Text = stopwatch.ElapsedMilliseconds.ToString();
            connn.Close();
        }

        /*private async void button5_Click(object sender, EventArgs e)
        {
            conn.Open();

            var valyuta = await ValyutaOlish(path);
            var result = ConvertToValyuta(valyuta);

            string sql = "INSERT INTO valyuta (title, price, date) VALUES (@title, @price, @date)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            TruncateDatabase(conn);
            guna2DataGridView2.Rows.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            TruncateDatabase(conn);
            guna2DataGridView2.Rows.Clear();

            // Add parameters for the SQL statement
            cmd.Parameters.AddWithValue("@title", "");
            cmd.Parameters.AddWithValue("@price", "");
            cmd.Parameters.AddWithValue("@date", "");

            // Set the parameter values and execute the command for each row
            cmd.Parameters["@title"].Value = result.Select(r => r.title).ToArray();
            cmd.Parameters["@price"].Value = result.Select(r => r.cb_price).ToArray();
            cmd.Parameters["@date"].Value = result.Select(r => r.date).ToArray();
            cmd.ArrayBindCount = result.Count;
            cmd.ExecuteNonQuery();

            stopwatch.Stop();
            label1.Text = stopwatch.ElapsedMilliseconds.ToString();

            conn.Close();
        }*/
    }
}