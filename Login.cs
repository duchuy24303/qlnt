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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string chuoiketnoi = "Data Source=LAPTOP-PMCHOE3P\\BUIDUCHUY; " + "Initial Catalog = QLNT; " + " Integrated Security = True ";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql_tk = "select TaiKhoan from TAIKHOAN";
            SqlCommand cmd = new SqlCommand(sql_tk, conn);
            string tk = cmd.ExecuteScalar().ToString();
           
            string sql_mk = "select MatKhau from TAIKHOAN";
            SqlCommand cmd2 = new SqlCommand(sql_mk, conn);
            string mk = cmd2.ExecuteScalar().ToString();
            
            if (txtTaiKhoan.Text.Trim() == tk.Trim() && txtMatKhau.Text.Trim() == mk.Trim())
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();

            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = checkBox1.Checked ? '\0' : '*';

        }

        private void Login_Shown(object sender, EventArgs e)
        {
            txtTaiKhoan.Focus();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
