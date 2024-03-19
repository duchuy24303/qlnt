using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
namespace QLNT
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string chuoiketnoi = "Data Source=LAPTOP-PMCHOE3P\\BUIDUCHUY; " + "Initial Catalog = QLNT; " + " Integrated Security = True ";
        SqlConnection conn = null;

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            comboBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            comboBox5.SelectedItem = null;
            //
            dataGridView4.DataSource = null;
            dataGridView4.ClearSelection();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            Form1 frm = new Form1();
            frm.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "" || txtTimKiem.Text == "")
            {
                MessageBox.Show("Phải nhập đủ thông tin tìm kiếm");
            }
            else if (comboBox5.Text == "MaKH")
            {
                string sql_select = "select *from KHACHHANG where MaKH = N'" + txtTimKiem.Text + "'";
                SqlDataAdapter dtAT = new SqlDataAdapter(sql_select, conn);
                DataTable dt = new DataTable();
                dtAT.Fill(dt);
                dataGridView4.DataSource = dt;
                dataGridView4.ClearSelection();
            }
            else if (comboBox5.Text == "MaP")
            {
                string sql_select = "select *from PHONG where MaP = N'" + txtTimKiem.Text + "'";
                SqlDataAdapter dtAT = new SqlDataAdapter(sql_select, conn);
                DataTable dt = new DataTable();
                dtAT.Fill(dt);
                dataGridView4.DataSource = dt;
                dataGridView4.ClearSelection();
            }
            else
            {
                string sql_select = "select *from HOADON where MaHD = N'" + txtTimKiem.Text + "'";
                SqlDataAdapter dtAT = new SqlDataAdapter(sql_select, conn);
                DataTable dt = new DataTable();
                dtAT.Fill(dt);
                dataGridView4.DataSource = dt;
                dataGridView4.ClearSelection();
            }
        }
    }
}
