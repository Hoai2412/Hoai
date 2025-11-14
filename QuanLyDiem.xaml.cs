using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyDiem : UserControl
    {
        private List<SoDiem> danhSachSoDiem;
        private List<NhatKyThayDoi> danhSachNhatKy;

        public QuanLyDiem()
        {
            InitializeComponent();
            danhSachSoDiem = new List<SoDiem>();
            danhSachNhatKy = new List<NhatKyThayDoi>();
            NapDuLieuMau();
            HienThiDanhSach();
        }

        // ===== CLASS DỮ LIỆU =====
        public class SoDiem
        {
            public string LopHocPhan { get; set; } = "";
            public string MaSV { get; set; } = "";
            public string HoTen { get; set; } = "";
            public double DiemGK { get; set; }
            public double DiemCK { get; set; }
            public double DiemTK => Math.Round(DiemGK * 0.4 + DiemCK * 0.6, 2);
            public string TrangThai { get; set; } = "Khóa";
        }

        public class NhatKyThayDoi
        {
            public string MaSV { get; set; } = "";
            public string MonHoc { get; set; } = "";
            public string ThoiGian { get; set; } = "";
            public string HanhDong { get; set; } = "";
            public string NguoiThucHien { get; set; } = "";
        }

        // ===== DỮ LIỆU MẪU =====
        private void NapDuLieuMau()
        {
            cbLopHocPhan.Items.Add("Lập trình C# - Nhóm 1");
            cbLopHocPhan.Items.Add("Cơ sở dữ liệu - Nhóm 2");

            danhSachSoDiem.AddRange(new[]
            {
                new SoDiem { LopHocPhan="Lập trình C# - Nhóm 1", MaSV="SV001", HoTen="Nguyễn Văn A", DiemGK=8.0, DiemCK=7.5, TrangThai="Khóa" },
                new SoDiem { LopHocPhan="Lập trình C# - Nhóm 1", MaSV="SV002", HoTen="Trần Thị B", DiemGK=7.5, DiemCK=8.0, TrangThai="Khóa" },
                new SoDiem { LopHocPhan="Cơ sở dữ liệu - Nhóm 2", MaSV="SV003", HoTen="Lê Văn C", DiemGK=6.5, DiemCK=7.0, TrangThai="Khóa" }
            });

            danhSachNhatKy.Add(new NhatKyThayDoi
            {
                MaSV = "SV001",
                MonHoc = "Lập trình C#",
                ThoiGian = "2025-10-15 10:30",
                HanhDong = "Giảng viên chỉnh sửa điểm cuối kỳ từ 7.0 -> 7.5",
                NguoiThucHien = "ThS. Nguyễn Văn An"
            });
        }

        // ===== HIỂN THỊ =====
        private void HienThiDanhSach()
        {
            if (cbLopHocPhan.SelectedItem == null)
            {
                dgSoDiem.ItemsSource = null;
                return;
            }

            string lop = cbLopHocPhan.SelectedItem.ToString();
            var ds = danhSachSoDiem.Where(s => s.LopHocPhan == lop).ToList();

            dgSoDiem.ItemsSource = null;
            dgSoDiem.ItemsSource = ds;
        }

        // Khi chọn lớp học phần
        private void cbLopHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HienThiDanhSach();
        }

        // ===== MỞ KHÓA =====
        private void BtnMoKhoa_Click(object sender, RoutedEventArgs e)
        {
            if (cbLopHocPhan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string lop = cbLopHocPhan.SelectedItem.ToString();
            foreach (var item in danhSachSoDiem.Where(s => s.LopHocPhan == lop))
                item.TrangThai = "Đang mở";

            HienThiDanhSach();
            MessageBox.Show($"🔓 Đã mở khóa sổ điểm cho {lop}. Giảng viên có thể chỉnh sửa!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== KHÓA =====
        private void BtnKhoa_Click(object sender, RoutedEventArgs e)
        {
            if (cbLopHocPhan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string lop = cbLopHocPhan.SelectedItem.ToString();
            foreach (var item in danhSachSoDiem.Where(s => s.LopHocPhan == lop))
                item.TrangThai = "Khóa";

            HienThiDanhSach();
            MessageBox.Show($"🔒 Đã khóa sổ điểm cho {lop}. Giảng viên không thể chỉnh sửa!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== NHẬT KÝ THAY ĐỔI =====
        private void BtnNhatKy_Click(object sender, RoutedEventArgs e)
        {
            string logText = string.Join("\n\n", danhSachNhatKy.Select(l =>
                $"📅 {l.ThoiGian}\n📘 Môn: {l.MonHoc}\n👤 {l.NguoiThucHien}\n✏️ {l.HanhDong}"
            ));

            MessageBox.Show(logText, "📜 Nhật ký thay đổi điểm", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
