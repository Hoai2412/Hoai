using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyNguoiDung : UserControl
    {
        private List<NguoiDung> danhSachNguoiDung;

        public QuanLyNguoiDung()
        {
            InitializeComponent();
            danhSachNguoiDung = new List<NguoiDung>();
            NapDuLieuMau();
            HienThiDanhSach();
        }

        // ===== CLASS DỮ LIỆU =====
        public class NguoiDung
        {
            public string TenDangNhap { get; set; } = "";
            public string MatKhau { get; set; } = "";
            public string HoTen { get; set; } = "";
            public string VaiTro { get; set; } = "";
            public string TrangThai { get; set; } = "Hoạt động"; // hoặc "Bị khóa"
        }

        // ===== DỮ LIỆU MẪU =====
        private void NapDuLieuMau()
        {
            danhSachNguoiDung.Add(new NguoiDung
            {
                TenDangNhap = "sv001",
                MatKhau = "123456",
                HoTen = "Nguyễn Văn A",
                VaiTro = "Sinh viên",
                TrangThai = "Hoạt động"
            });
            danhSachNguoiDung.Add(new NguoiDung
            {
                TenDangNhap = "gv01",
                MatKhau = "654321",
                HoTen = "ThS. Trần Thị B",
                VaiTro = "Giảng viên",
                TrangThai = "Hoạt động"
            });
        }

        // ===== HIỂN THỊ =====
        private void HienThiDanhSach()
        {
            dgNguoiDung.ItemsSource = null;
            dgNguoiDung.ItemsSource = danhSachNguoiDung.ToList();
        }

        // ===== THÊM =====
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Password) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                cbVaiTro.SelectedItem == null)
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (danhSachNguoiDung.Any(u => u.TenDangNhap == txtTenDangNhap.Text))
            {
                MessageBox.Show("❌ Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new NguoiDung
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = txtMatKhau.Password,
                HoTen = txtHoTen.Text,
                VaiTro = ((ComboBoxItem)cbVaiTro.SelectedItem).Content.ToString() ?? "",
                TrangThai = "Hoạt động"
            };

            danhSachNguoiDung.Add(user);
            HienThiDanhSach();
            MessageBox.Show("✅ Tạo tài khoản thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            XoaForm();
        }

        // ===== SỬA =====
        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgNguoiDung.SelectedItem is not NguoiDung nd)
            {
                MessageBox.Show("⚠️ Vui lòng chọn tài khoản để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            nd.HoTen = txtHoTen.Text;
            nd.MatKhau = txtMatKhau.Password;
            nd.VaiTro = ((ComboBoxItem)cbVaiTro.SelectedItem)?.Content.ToString() ?? nd.VaiTro;

            HienThiDanhSach();
            MessageBox.Show("✅ Cập nhật tài khoản thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== XÓA =====
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgNguoiDung.SelectedItem is not NguoiDung nd)
            {
                MessageBox.Show("⚠️ Vui lòng chọn tài khoản để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{nd.TenDangNhap}' không?",
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                danhSachNguoiDung.Remove(nd);
                HienThiDanhSach();
                MessageBox.Show("🗑️ Đã xóa tài khoản!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ===== KHÓA / MỞ KHÓA =====
        private void BtnKhoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgNguoiDung.SelectedItem is not NguoiDung nd)
            {
                MessageBox.Show("⚠️ Vui lòng chọn tài khoản!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (nd.TrangThai == "Hoạt động")
                nd.TrangThai = "Bị khóa";
            else
                nd.TrangThai = "Hoạt động";

            HienThiDanhSach();
            MessageBox.Show($"🔒 Tài khoản '{nd.TenDangNhap}' hiện: {nd.TrangThai}", "Cập nhật", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== XÓA FORM =====
        private void XoaForm()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            cbVaiTro.SelectedIndex = -1;
        }
    }
}
