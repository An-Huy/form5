using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form5
{
    public partial class Form1 : Form
    {
        DBconnection DBconnection = new DBconnection();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBconnection.Connect();
            DataTable dt = DBconnection.table("Select * from DMGiaoTrinh");
            dataGridView1.DataSource = dt;
            DBconnection.closeConnect();
        }
        private bool check()
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show(" Vui lòng nhập tên cùa giáo trình", "Thông báo");
                return false;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên của tác giả giáo trình", "Thông báo");
                return false;
            }
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập chuyên ngành cùa giáo trình", "Thông báo");
                return false;
            }
            return true;
        }
        private void resetForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin các thông tin yêu cầu!", "Thông báo");
                Form1_Load(sender, e);
            }
            else
            {
                string query = $"Select * from DMGiaoTrinh join TacGia join ChuyenNganh where TenGT = N'{textBox1.Text.Trim()}',TenTacGia = N'{textBox2.Text.Trim()}' and TenChuyenNganh = N'{textBox3.Text.Trim()}'";
                try
                {
                    dataGridView1.DataSource = DBconnection.table(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (check())
            {
                string query = $"Insert into DMGiaoTrinh values(N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}')";
                if (MessageBox.Show("Bạn có muốn thêm giáo trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        DBconnection.excute(query);
                        MessageBox.Show("Đã thêm thành công!", "Thông báo");
                        Form1_Load(sender, e);
                        resetForm();
                        DBconnection.closeConnect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + "Giáo trình đã tồn tại!", "Thông báo");
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (check())
            {
                string query = $"Update DMGiaoTrinh where TenGT = N'{textBox1.Text.Trim()}',TenTacGia = N'{textBox2.Text.Trim()}' and TenChuyenNganh = N'{textBox3.Text.Trim()}'";
                if (MessageBox.Show("Bạn có muốn sửa khách hàng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        DBconnection.excute(query);
                        MessageBox.Show("Sửa thành công!", "Thông báo");
                        Form1_Load(sender, e);
                        DBconnection.closeConnect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Thông báo");
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string query = "delete from DMGiaoTrinh where TenGT = '" + s + "'";
                DBconnection.excute(query);
                Form1_Load(sender, e);
                DBconnection.closeConnect();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
}
