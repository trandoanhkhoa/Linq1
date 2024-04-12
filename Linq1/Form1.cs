using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linq1
{
    public partial class Form1 : Form
    {
        //Tạo biến database
        private readonly QLHSDataContext QLHS;
        public void Loaddata()
        {
            //Mình cập nhật dữ liệu lên datagridview
            var lsths = QLHS.HocSinhs.Select(x => new
            {
                x.MaHS,
                x.TenHS,
                x.DTB,
                x.NgaySinh,
                x.DiaChi,
                x.MaLop
            });
            dgvhocsinh.DataSource = lsths;
            
        }
        public Form1()
        {
            InitializeComponent();
            QLHS = new QLHSDataContext();
            Loaddata();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgvhocsinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMahs.Text = dgvhocsinh.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenhs.Text = dgvhocsinh.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDTB.Text = dgvhocsinh.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtDiachi.Text = dgvhocsinh.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtNgaysinh.Text = dgvhocsinh.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtMalop.Text = dgvhocsinh.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        void ResetTextBox()
        {
            txtMahs.ResetText();
            txtTenhs.ResetText();
            txtDiachi.ResetText();
            txtNgaysinh.ResetText();
            txtMalop.ResetText();
            txtDTB.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            HocSinh hocSinh = new HocSinh();
            hocSinh.MaHS = txtMahs.Text;
            hocSinh.TenHS = txtTenhs.Text;
            hocSinh.DiaChi = txtDiachi.Text;
            hocSinh.NgaySinh = DateTime.Parse(txtNgaysinh.Text);
            hocSinh.MaLop = txtMalop.Text;
            hocSinh.DTB = float.Parse(txtDTB.Text);

            QLHS.HocSinhs.InsertOnSubmit(hocSinh);
            //Lưu lại thay đổi
            QLHS.SubmitChanges();
            Loaddata(); ResetTextBox();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mahs = txtMahs.Text;
            if(!string.IsNullOrEmpty(mahs)){
                var hs = QLHS.HocSinhs.SingleOrDefault(x => x.MaHS == mahs);
                QLHS.HocSinhs.DeleteOnSubmit(hs);
                QLHS.SubmitChanges();
                Loaddata();
                ResetTextBox();
            }
            
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string mahs = txtMahs.Text;
            HocSinh hocSinh = QLHS.HocSinhs.SingleOrDefault(x => x.MaHS == mahs);
            hocSinh.MaHS = txtMahs.Text;
            hocSinh.TenHS = txtTenhs.Text;
            hocSinh.DiaChi = txtDiachi.Text;
            hocSinh.NgaySinh = DateTime.Parse(txtNgaysinh.Text);
            hocSinh.MaLop = txtMalop.Text;
            //hocSinh.DTB = ̣̣̣̣̣float.Parsẹ̣̣̣̣̣̣(txtDTB.Text);
            //Lưu lại thay đổi
            QLHS.SubmitChanges();
            Loaddata(); 
            ResetTextBox();
        }
    }
}
