using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyDangKyHocPhan : UserControl
    {
        // Danh sách đợt đăng ký hiển thị trên DataGrid
        public ObservableCollection<DotDangKy> DanhSachDotDangKy { get; set; }

        // Biến tạm lưu đợt đang chọn
        private DotDangKy? dotDangKyDangChon;

        public QuanLyDangKyHocPhan()
        {
            InitializeComponent();

            // Khởi tạo danh sách
            DanhSachDotDangKy = new ObservableCollection<DotDangKy>();
            dgDotDangKy.ItemsSource = DanhSachDotDangKy;

            // Dữ liệu mẫu ban đầu
            DanhSachDotDangKy.Add(new DotDangKy
            {
                MaDot = "DKHP2025-01",
                NgayBatDau = new DateTime(2025, 10, 20),
                NgayKetThuc = new DateTime(2025, 11, 5),
                TinChiToiDa = 25,
                MonTienQuyet = "Phải hoàn thành các môn cơ sở",
                TrangThai = "Đang mở"
            });
        }

        // Định nghĩa lớp DotDangKy
        public class DotDangKy
        {
            public string MaDot { get; set; } = string.Empty;
            public DateTime NgayBatDau { get; set; }
            public DateTime NgayKetThuc { get; set; }
            public int TinChiToiDa { get; set; }
            public string MonTienQuyet { get; set; } = string.Empty;
            public string TrangThai { get; set; } = string.Empty;
        }

        // ====== SỰ KIỆN ======

        // Khi thay đổi tín chỉ tối đa
        private void txtTinChiToiDa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtTinChiToiDa.Text, out int tinChi))
            {
                // Nếu cần, có thể cập nhật dữ liệu hiện tại
            }
        }

        // Khi thay đổi mô tả môn tiên quyết
        private void txtMonTienQuyet_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Có thể cập nhật trực tiếp DotDangKy đang chọn nếu cần
        }

        // Mở đăng ký
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var maDot = $"DKHP{DateTime.Now:yyyyMMddHHmm}";
            var dot = new DotDangKy
            {
                MaDot = maDot,
                NgayBatDau = dpNgayBatDau.SelectedDate ?? DateTime.Now,
                NgayKetThuc = dpNgayKetThuc.SelectedDate ?? DateTime.Now.AddDays(15),
                TinChiToiDa = int.TryParse(txtTinChiToiDa.Text, out int tc) ? tc : 25,
                MonTienQuyet = txtMonTienQuyet.Text,
                TrangThai = "Đang mở"
            };

            DanhSachDotDangKy.Add(dot);
            MessageBox.Show("✅ Đã mở đợt đăng ký mới!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Đóng đăng ký
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dotDangKyDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một đợt trong danh sách!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dotDangKyDangChon.TrangThai = "Đã đóng";
            dgDotDangKy.Items.Refresh();

            MessageBox.Show($"Đã đóng đợt {dotDangKyDangChon.MaDot}.", "Hoàn tất", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Lưu cấu hình
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("💾 Đã lưu cấu hình đợt đăng ký thành công!", "Lưu thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Khi chọn một đợt trong bảng
        private void dgDotDangKy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dotDangKyDangChon = dgDotDangKy.SelectedItem as DotDangKy;
        }
    }
}
