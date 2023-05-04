namespace ForThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var thread = new Thread(WriteTextUnsafe);
            thread.Start();

            var thread2 = new Thread(WriteTextUnsafe2);
            thread2.Start();
        }

        private void WriteTextUnsafe2(object? obj)
        {
            for (int i = 10000; i > 0; i--)
            {
                label2.Text = i.ToString();
            }
        }

        private void WriteTextUnsafe(object? obj)
        {
            for (int i = 0; i < 10000; i++)
            {
                label1.Text = i.ToString();
            }
        }
    }
}