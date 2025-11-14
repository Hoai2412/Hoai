using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyLopHocPhan : UserControl
    {
        private List<LopHocPhan> danhSachLop;

        public QuanLyLopHocPhan()
        {
            InitializeComponent();
            danhSachLop = new List<LopHocPhan>();
            NapDuLieuMau();
            HienThiDanhSach();
        }

        // ===== CLASS DỮ LIỆU =====
        public class LopHocPhan
        {
            public string MaLop { get; set; } = string.Empty;
            public string TenLop { get; set; } = string.Empty;
            public string GiangVien { get; set; } = string.Empty;
            public string PhongHoc { get; set; } = string.Empty;
            public string LichHoc { get; set; } = string.Empty;
            public int SiSoToiDa { get; set; }
        }

        // ===== DỮ LIỆU MẪU =====
        private void NapDuLieuMau()
        {
            danhSachLop.Add(new LopHocPhan
            {
                MaLop = "LHP001",
                TenLop = "Lập trình C# - Nhóm 1",
                GiangVien = "ThS. Nguyễn Văn An",
                PhongHoc = "A202",
                LichHoc = "Thứ 2, 4, 6 - Ca 1",
                SiSoToiDa = 50
            });

            danhSachLop.Add(new LopHocPhan
            {
                MaLop = "LHP002",
                TenLop = "Cơ sở dữ liệu - Nhóm 2",
                GiangVien = "ThS. Trần Thị Bình",
                PhongHoc = "B305",
                LichHoc = "Thứ 3, 5 - Ca 2",
                SiSoToiDa = 45
            });
        }

        // ===== HIỂN THỊ DANH SÁCH =====
        private void HienThiDanhSach(IEnumerable<LopHocPhan>? data = null)
        {
            dgLopHocPhan.ItemsSource = null;
            dgLopHocPhan.ItemsSource = data ?? danhSachLop;
        }

        // ===== THÊM LỚP =====
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ thông tin lớp học phần!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (danhSachLop.Any(l => l.MaLop == txtMaLop.Text))
            {
                MessageBox.Show("❌ Mã lớp đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int siSo = int.TryParse(txtSiSoToiDa.Text, out int value) ? value : 50;

            LopHocPhan lopMoi = new LopHocPhan
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                GiangVien = txtGiangVien.Text.Trim(),
                PhongHoc = txtPhongHoc.Text.Trim(),
                LichHoc = txtLichHoc.Text.Trim(),
                SiSoToiDa = siSo
            };

            danhSachLop.Add(lopMoi);
            HienThiDanhSach();
            MessageBox.Show("✅ Thêm lớp học phần thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            XoaFormNhap();
        }

        // ===== SỬA LỚP =====
        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgLopHocPhan.SelectedItem is not LopHocPhan lop)
            {
                MessageBox.Show("⚠️ Vui lòng chọn một lớp để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            lop.TenLop = txtTenLop.Text;
            lop.GiangVien = txtGiangVien.Text;
            lop.PhongHoc = txtPhongHoc.Text;
            lop.LichHoc = txtLichHoc.Text;
            lop.SiSoToiDa = int.TryParse(txtSiSoToiDa.Text, out int siSo) ? siSo : lop.SiSoToiDa;

            HienThiDanhSach();
            MessageBox.Show("✅ Cập nhật lớp học phần thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== XÓA LỚP =====
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgLopHocPhan.SelectedItem is not LopHocPhan lop)
            {
                MessageBox.Show("⚠️ Vui lòng chọn một lớp để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp '{lop.TenLop}' không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                danhSachLop.Remove(lop);
                HienThiDanhSach();
                MessageBox.Show("🗑️ Đã xóa lớp học phần!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                XoaFormNhap();
            }
        }

        // ===== KHI CHỌN DÒNG TRONG DATAGRID =====
        private void dgLopHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgLopHocPhan.SelectedItem is LopHocPhan lop)
            {
                txtMaLop.Text = lop.MaLop;
                txtTenLop.Text = lop.TenLop;
                txtGiangVien.Text = lop.GiangVien;
                txtPhongHoc.Text = lop.PhongHoc;
                txtLichHoc.Text = lop.LichHoc;
                txtSiSoToiDa.Text = lop.SiSoToiDa.ToString();
            }
        }

        // ===== TÌM KIẾM (KHI GÕ) =====
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LocDanhSach();
        }

        // ===== TÌM KIẾM (KHI BẤM NÚT "TÌM") =====
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LocDanhSach();
        }

        private void LocDanhSach()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                HienThiDanhSach();
                return;
            }

            var ketQua = danhSachLop.Where(l =>
                l.MaLop.ToLower().Contains(keyword) ||
                l.TenLop.ToLower().Contains(keyword) ||
                l.GiangVien.ToLower().Contains(keyword) ||
                l.PhongHoc.ToLower().Contains(keyword)
            ).ToList();

            HienThiDanhSach(ketQua);
        }

        // ===== XÓA FORM =====
        private void XoaFormNhap()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtGiangVien.Clear();
            txtPhongHoc.Clear();
            txtLichHoc.Clear();
            txtSiSoToiDa.Text = "50";
        }

        private void txtMaLop_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtGiangVien_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtLichHoc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTenLop_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPhongHoc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSiSoToiDa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            LocDanhSach();
        }
    }
}
