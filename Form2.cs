using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace QLNT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string chuoiketnoi = "Data Source=LAPTOP-PMCHOE3P\\BUIDUCHUY; " + "Initial Catalog = QLNT; " + " Integrated Security = True ";
        SqlConnection conn = null;
        void hienthi_1()
        {
            string sql = "SELECT * FROM KHACHHANG";
            SqlDataAdapter daSV = new SqlDataAdapter(sql, conn);
            DataTable dtSV = new DataTable();
            daSV.Fill(dtSV);
            dataGridView1.DataSource = dtSV;
        }
        void hienthi_2()
        {
            string sql = "SELECT * FROM PHONG";
            SqlDataAdapter daSV = new SqlDataAdapter(sql, conn);
            DataTable dtSV = new DataTable();
            daSV.Fill(dtSV);
            dataGridView2.DataSource = dtSV;
        }
        void hienthi_3()
        {
            string sql = "SELECT * FROM HOADON";
            SqlDataAdapter daSV = new SqlDataAdapter(sql, conn);
            DataTable dtSV = new DataTable();
            daSV.Fill(dtSV);
            dataGridView3.DataSource = dtSV;
        }
        void add_MaP()
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql_addcombox = "select MaP from PHONG";
            SqlCommand cmd = new SqlCommand(sql_addcombox, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBox2.Items.Add(DR[0]);
            }
        }
        void add_MaKH()
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql_addcombox = "select MaKH from KHACHHANG";
            SqlCommand cmd = new SqlCommand(sql_addcombox, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBox4.Items.Add(DR[0]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "" || txtHoTen.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false) || comboBox1.Text == "" || txtSDT.Text == "" || comboBox2.SelectedItem == null || txtSoThangThue.Text == "")
                {
                    MessageBox.Show("Phải nhập đủ thông tin!");
                }
                else
                {
                    string sql_count = "select COUNT(MaP) from KHACHHANG where MaP = N'"+comboBox2.Text+"'";
                    SqlCommand cmd0 = new SqlCommand(sql_count, conn);
                    int count =  Convert.ToInt32(cmd0.ExecuteScalar());
                    if(count > 0)
                    {
                        MessageBox.Show("Phòng đã có người ở!");
                    }
                    else
                    {
                        try
                        {
                            string sql_insert = "INSERT INTO KHACHHANG (MaKH, HoTen, GioiTinh, NgaySinh, QueQuan, SDT, SoThangThue, MaP) VALUES(N'" + txtMaKH.Text + "', N'" + txtHoTen.Text + "', N'" + (radioButton1.Checked == true ? radioButton1.Text : radioButton2.Text) + "','" + dateTimePicker1.Value + "', N'" + comboBox1.Text + "', N'" + txtSDT.Text + "', " + Convert.ToInt32(txtSoThangThue.Text) + ", N'" + comboBox2.Text + "')";
                            SqlCommand cmd = new SqlCommand(sql_insert, conn);
                            cmd.ExecuteNonQuery();
                            hienthi_1();

                            string sql_update = "update PHONG set TrangThai = N'Đã thuê' where MaP = '" + comboBox2.Text + "'";
                            SqlCommand cmd2 = new SqlCommand(sql_update, conn);
                            cmd2.ExecuteNonQuery();
                            hienthi_2();

                            //clear
                            txtMaKH.Text = "";
                            txtHoTen.Text = "";
                            txtSDT.Text = "";
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            dateTimePicker1.Value = DateTime.Now;
                            txtSoThangThue.Text = "";
                            txtMaP.Text = "";
                            comboBox1.Text = "";
                            comboBox2.SelectedItem = null;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Mã khách hàng đã tồn tại!");
                        }
                    }
                    
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Chưa nhập đúng thông tin");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd 'tháng' MM 'năm' yyyy";
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            add_MaP();
            add_MaKH();
            hienthi_1();
            hienthi_2();
            hienthi_3();
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                if(txtMaP.Text == "" || txtGiaPhong.Text == "" || comboBox3.Text == "")
                {
                    MessageBox.Show("Phải nhập đầy đủ thông tin!");
                }
                else
                {
                    try
                    {
                        string sql_insert = "insert into PHONG values('"+txtMaP.Text+"','"+txtGiaPhong.Text+"', N'"+comboBox3.Text+"')";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        cmd.ExecuteNonQuery();
                        hienthi_2();
                        //clear
                        txtMaP.Text = "";
                        txtGiaPhong.Text = "";
                        comboBox3.SelectedItem = null;
    
                    }
                catch (Exception)
                    {
                        MessageBox.Show("Mã phòng đã tồn tại");
                    }                
                 }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtMaP.Text == "" || txtGiaPhong.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Phải chọn thông tin cần sửa!");
            }
            else
            {
                string sql_update = "update PHONG set GiaPhong = '"+float.Parse(txtGiaPhong.Text)+"', TrangThai = N'"+comboBox3.Text+"' where MaP = '" +txtMaP.Text+"' ";
                SqlCommand cmd = new SqlCommand(sql_update, conn);
                cmd.ExecuteNonQuery();
                hienthi_2();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMaP.Text == "")
                {
                    MessageBox.Show("Chưa chọn mã phòng!");
                }
                else
                {

                    string sql_delete = "delete from PHONG where MaP = '" + txtMaP.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql_delete, conn);
                    cmd.ExecuteNonQuery();
                    hienthi_2();
                    //clear
                    txtMaP.Text = "";
                    txtGiaPhong.Text = "";
                    comboBox3.SelectedItem = null;
                    txtMaP.ReadOnly = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Phòng đang có người ở!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMaKH.ReadOnly = false;
            comboBox2.Enabled = true;
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            txtSoThangThue.Text = "";
            txtMaP.Text = "";
            comboBox1.Text = "";
            //Cập nhật MaP khi thay đổi bên bảng PHONG
            comboBox2.Items.Clear();
            add_MaP();
            comboBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "" || txtHoTen.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false) || comboBox1.Text == "" || txtSDT.Text == "" || comboBox2.SelectedItem == null || txtSoThangThue.Text == "")
            {
                MessageBox.Show("Phải chọn thông tin cầu sửa!");
            }
            else
            {
            string sql_update = "update KHACHHANG set HoTen = N'" + txtHoTen.Text + "',GioiTinh = N'" + (radioButton1.Checked == true ? radioButton1.Text : radioButton2.Text) + "',NgaySinh = '" + dateTimePicker1.Value + "', QueQuan = N'" + comboBox1.Text + "',SDT = N'" + txtSDT.Text + "', SoThangThue = " + Convert.ToInt32(txtSoThangThue.Text) + ", MaP = N'" + comboBox2.Text + "' where MaKH = '"+txtMaKH.Text+"'";
            SqlCommand cmd = new SqlCommand(sql_update,conn);
            cmd.ExecuteNonQuery();
            hienthi_1();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "" || txtHoTen.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false) || comboBox1.Text == "" || txtSDT.Text == "" || comboBox2.SelectedItem == null || txtSoThangThue.Text == "")
            {
                MessageBox.Show("Phải chọn thông tin cầu xóa!");
            }
            else
            {
                string sql_delete = "delete from KHACHHANG where MaKH = '"+txtMaKH.Text+"' ";
                SqlCommand cmd = new SqlCommand(sql_delete, conn);
                cmd.ExecuteNonQuery();
                hienthi_1();

                string sql_update = "update PHONG set TrangThai = N'Chưa thuê' where MaP = '" + comboBox2.Text + "'";
                SqlCommand cmd2 = new SqlCommand(sql_update, conn);
                cmd2.ExecuteNonQuery();
                hienthi_2();

                //clear
                txtMaKH.Text = "";
                txtHoTen.Text = "";
                txtSDT.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                dateTimePicker1.Value = DateTime.Now;
                txtSoThangThue.Text = "";
                txtMaP.Text = "";
                comboBox1.Text = "";
                comboBox2.SelectedItem = null;
                txtMaKH.ReadOnly = false;

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaP.ReadOnly = true;
            txtMaP.Text = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
            txtGiaPhong.Text = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();
            comboBox3.Text = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.ReadOnly = true;
            comboBox2.Enabled = false;
            txtMaKH.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            txtHoTen.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString() == "Nam")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            dateTimePicker1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
            txtSDT.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString();
            txtSoThangThue.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtMaP.ReadOnly = false;
            //clear
            txtMaP.Text = "";
            txtGiaPhong.Text = "";
            comboBox3.SelectedItem = null;
            hienthi_2();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT PHONG.GiaPhong FROM KHACHHANG INNER JOIN PHONG ON KHACHHANG.MaP = PHONG.MaP WHERE(KHACHHANG.MaKH = '" + comboBox4.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql_select, conn);

            /*SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                textBox4.Text = DR[0].ToString();
            }*/

            txtGiaPhongtab3.Text = cmd.ExecuteScalar().ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(txtTienDien.Text == "" || txtTienNuoc.Text == "" || txtGiaPhongtab3.Text == "")
            {
                MessageBox.Show("Chưa đủ thông tin!");
            }
            else if (txtMaHD.ReadOnly == false)
            {

                txtTongTien.Text = (float.Parse(txtTienDien.Text) + float.Parse(txtTienNuoc.Text) + float.Parse(txtGiaPhongtab3.Text)).ToString();
                btnThemHD.Enabled = true;
                
            }
            else
            {
                txtTongTien.Text = (float.Parse(txtTienDien.Text) + float.Parse(txtTienNuoc.Text) + float.Parse(txtGiaPhongtab3.Text)).ToString();
                btnSuaHD.Enabled = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(txtMaHD.Text == ""|| txtTienDien.Text == "" || txtTienNuoc.Text == "" || txtTongTien.Text == ""|| comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Chưa nhập đủ thông tin!");
            }
            else
            {
                string sql_count = "select COUNT(MaKH) from HOADON where MaKH = N'" + comboBox4.Text + "'";
                SqlCommand cmd0 = new SqlCommand(sql_count, conn);
                int count = Convert.ToInt32(cmd0.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Khách hàng đã có hóa đơn!");
                }
                else
                {

                    try
                    {
                        string sql_insert = "insert into HOADON values('" +txtMaHD.Text + "', '" + float.Parse(txtTienDien.Text) + "', " + float.Parse(txtTienNuoc.Text) + ", " + float.Parse(txtTongTien.Text) + ", '" + comboBox4.Text + "')";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        cmd.ExecuteNonQuery();
                        hienthi_3();
                        //clear
                        txtMaHD.Text = "";
                        txtTienDien.Text = "";
                        txtTienNuoc.Text = "";
                        txtTongTien.Text = "";
                        txtGiaPhongtab3.Text = "";
                        

                        //Cập nhật lại MaKH
                        comboBox4.Items.Clear();
                        add_MaKH();
                        comboBox4.Text = "";

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Mã hóa đơn đã tồn tại!");
                    }
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.ReadOnly = true;
            comboBox4.Enabled = false;
            txtMaHD.Text = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value.ToString();
            txtTienDien.Text = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[1].Value.ToString();
            txtTienNuoc.Text = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[2].Value.ToString();
            txtTongTien.Text = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[3].Value.ToString();
            comboBox4.Text = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[4].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "" || txtTienDien.Text == "" || txtTienNuoc.Text == "" || txtTongTien.Text == " " || comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn thông tin!");
            }
            else
            {
                string sql_update = "update HOADON set TienDien = " + float.Parse(txtTienDien.Text) + ",TienNuoc = '"+float.Parse(txtTienNuoc.Text)+"',TongTien = '"+float.Parse(txtTongTien.Text)+"' where MaHD = '"+txtMaHD.Text+"'";
                SqlCommand cmd = new SqlCommand(sql_update,conn);
                cmd.ExecuteNonQuery();
                hienthi_3();
                

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "" || txtTienDien.Text == "" || txtTienNuoc.Text == "" || txtTongTien.Text == " " || comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn thông tin!");
            }
            else
            {

                string sql_delete = "delete from HOADON where MaHD = N'" + txtMaHD.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_delete, conn);
            cmd.ExecuteNonQuery();
            hienthi_3();
            //clear
            txtMaHD.Text = "";
            txtTienDien.Text = "";
            txtTienNuoc.Text = "";
            txtTongTien.Text = "";
            txtGiaPhongtab3.Text = "";
            txtMaHD.ReadOnly = false;
          

                //Cập nhật lại MaKH
                comboBox4.Items.Clear();
                add_MaKH();
                comboBox4.Text = "";
                comboBox4.Enabled = true;
            }

        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT PHONG.GiaPhong FROM KHACHHANG INNER JOIN PHONG ON KHACHHANG.MaP = PHONG.MaP WHERE(KHACHHANG.MaKH = '" + comboBox4.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql_select, conn);

            /*SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                textBox4.Text = DR[0].ToString();
            }*/

            txtGiaPhongtab3.Text = cmd.ExecuteScalar().ToString();
            btnThemHD.Enabled = false;
            btnSuaHD.Enabled = false;
        }

        

        private void button14_Click(object sender, EventArgs e)
        {
            //clear
            txtMaHD.Text = "";
            txtTienDien.Text = "";
            txtTienNuoc.Text = "";
            txtTongTien.Text = "";
            txtGiaPhongtab3.Text = "";
            
            txtMaHD.ReadOnly = false;

            //Cập nhật lại MaKH
            comboBox4.Items.Clear();
            add_MaKH();
            comboBox4.Text = "";
            
            comboBox4.Enabled = true;

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void txtSoThangThue_TextChanged(object sender, EventArgs e)
        {
            for(int i = 0;i<txtSoThangThue.Text.Length; i++)
            {
                if (txtSoThangThue.Text.Trim().Length > 0 && !char.IsDigit(txtSoThangThue.Text, txtSoThangThue.Text.Length - 1))
                {
                    MessageBox.Show("Số tháng thuê phải là số!");
                    txtSoThangThue.Text = "";
                    break;
                }
            }
        }

        private void txtGiaPhong_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtGiaPhong.Text.Length; i++)
            {
                if (txtGiaPhong.Text.Trim().Length > 0 && !char.IsDigit(txtGiaPhong.Text, txtGiaPhong.Text.Length - 1))
                {
                    MessageBox.Show("Giá phòng không hợp lệ!");
                    txtGiaPhong.Text = "";
                    break;
                }
            }
        }

        private void txtTienDien_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtTienDien.Text.Length; i++)
            {
                if (txtTienDien.Text.Trim().Length > 0 && !char.IsDigit(txtTienDien.Text, txtTienDien.Text.Length - 1))
                {
                    MessageBox.Show("Số tiền nhập vào không hợp lệ!");
                    txtTienDien.Text = "";
                    break;
                }
            }
        }

        private void txtTienNuoc_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtTienNuoc.Text.Length; i++)
            {
                if (txtTienNuoc.Text.Trim().Length > 0 && !char.IsDigit(txtTienNuoc.Text, txtTienNuoc.Text.Length - 1))
                {
                    MessageBox.Show("Số tiền nhập vào không hợp lệ!");
                    txtTienNuoc.Text = "";
                    break;
                }
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            


        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(txtMaKH.Text))
            {
                MessageBox.Show("Mã khách hàng không chứa ký tự đặc biệt");
                txtMaKH.Text = "";
            }
        }

        private void txtMaP_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(txtMaP.Text))
            {
                MessageBox.Show("Mã phòng không chứa ký tự đặc biệt");
                txtMaP.Text = "";
            }
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(txtMaHD.Text))
            {
                MessageBox.Show("Mã hóa đơn không chứa ký tự đặc biệt");
                txtMaHD.Text = "";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();


        }

        private void txtSDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSDT.Text.Trim().Length > 0 && !char.IsDigit(txtSDT.Text, txtSDT.Text.Length - 1))
            {
                MessageBox.Show("SDT phải là số");
                txtSDT.Text = "";
            }
        }


        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTienDien_TextChanged_1(object sender, EventArgs e)
        {
            for (int i = 0; i < txtGiaPhong.Text.Length; i++)
            {
                if (txtGiaPhong.Text.Trim().Length > 0 && !char.IsDigit(txtGiaPhong.Text, txtGiaPhong.Text.Length - 1))
                {
                    MessageBox.Show("Giá phòng không hợp lệ!");
                    txtGiaPhong.Text = "";
                    break;
                }
            }

            btnThemHD.Enabled = false;
            btnSuaHD.Enabled = false;
        }

        private void txtTienDien_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtTienDien.Text.Trim().Length > 0 && !char.IsDigit(txtTienDien.Text, txtTienDien.Text.Length - 1))
            {
                MessageBox.Show("Tiền điện phải là số");
                txtTienDien.Text = "";
            }
        }

        private void txtTienNuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtTienNuoc.Text.Trim().Length > 0 && !char.IsDigit(txtTienNuoc.Text, txtTienNuoc.Text.Length - 1))
            {
                MessageBox.Show("Tiền nước phải là số");
                txtTienNuoc.Text = "";
            }
        }
    }
}
