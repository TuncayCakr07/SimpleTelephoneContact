using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        SqlDataAdapter data;
        SqlConnection connect=new SqlConnection("Data Source=MSI;Initial Catalog=TelefonRehberi;Integrated Security=True");
        SqlCommand cm=new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }

        void Kisigetir()
        {
            connect.Open(); 
            data=new SqlDataAdapter("select *from Kisibilgileri",connect);
            DataTable tablo= new DataTable();
            data.Fill(tablo);
            dataGridView1.DataSource= tablo;
            connect.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Kisigetir();
            temizle();
        }

        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into Kisibilgileri (ad,soyad,tel) values (@ad,@soyad,@tel)";
            cm=new SqlCommand(sorgu,connect);
            cm.Parameters.AddWithValue("@ad",textBox2.Text);
            cm.Parameters.AddWithValue("@soyad", textBox3.Text);
            cm.Parameters.AddWithValue("@tel", textBox4.Text);
            connect.Open();
            cm.ExecuteNonQuery();
            connect.Close();
            Kisigetir();
            temizle();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From Kisibilgileri Where id=@no";
            cm=new SqlCommand(sorgu,connect);
            cm.Parameters.AddWithValue("@no",Convert.ToInt32(textBox1.Text));
            connect.Open();
            cm.ExecuteNonQuery();
            connect.Close();
            Kisigetir();
            temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Kisibilgileri set ad=@ad,soyad=@soyad,tel=@tel Where id=@no";
            cm=new SqlCommand(sorgu,connect);
            cm.Parameters.AddWithValue("@no", Convert.ToInt32(textBox1.Text));
            cm.Parameters.AddWithValue("@ad", textBox2.Text);
            cm.Parameters.AddWithValue("@soyad", textBox3.Text);
            cm.Parameters.AddWithValue("@tel", textBox4.Text);
            connect.Open();
            cm.ExecuteNonQuery();
            connect.Close();
            Kisigetir();
            temizle();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da=new SqlDataAdapter("Select *from Kisibilgileri where ad like'"+textBox5.Text+"%'",connect);
            DataSet ds = new DataSet();
            connect.Open();
            da.Fill(ds,"Kisibilgileri");
            dataGridView1.DataSource= ds.Tables["Kisibilgileri"];
            connect.Close();
        }
    }
}
