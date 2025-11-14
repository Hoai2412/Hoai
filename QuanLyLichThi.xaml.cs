using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyLichThi : UserControl
    {
        private List<LichThi> danhSachLichThi;

        public QuanLyLichThi()
        {
            InitializeComponent();
            danhSachLichThi = new List<LichThi>();
            NapDuLieuMau();
            HienThiDanhSach();
        }

        // ===== CLASS DỮ LIỆU =====
        public class LichThi
        {
            public string HocKy { get; set; } = "";
            public string MonThi { get; set; } = "";
            public DateTime NgayThi { get; set; }
            public string CaThi { get; set; } = "";
            public string PhongThi { get; set; } = "";
            public string GiamThi { get; set; } = "";
            public string TrangThai { get; set; } = "Chưa công bố";
        }

        // ===== DỮ LIỆU MẪU =====
        private void NapDuLieuMau()
        {
            danhSachLichThi.Add(new LichThi
            {
                HocKy = "Học kỳ 1 - Năm học 2025-2026",
                MonThi = "Cơ sở dữ liệu",
                NgayThi = new DateTime(2025, 12, 20),
                CaThi = "Ca 1 - 7h30",
                PhongThi = "B305",
                GiamThi = "ThS. Nguyễn Văn Hòa",
                TrangThai = "Đã công bố"
            });
        }

        // ===== HIỂN THỊ =====
        private void HienThiDanhSach()
        {
            dgLichThi.ItemsSource = null;
            dgLichThi.ItemsSource = danhSachLichThi.ToList();
        }

        // ===== THÊM =====
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cbHocKy.SelectedItem is not ComboBoxItem hk || string.IsNullOrWhiteSpace(txtMonThi.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var lich = new LichThi
            {
                HocKy = hk.Content.ToString() ?? "",
                MonThi = txtMonThi.Text,
                NgayThi = dpNgayThi.SelectedDate ?? DateTime.Now,
                CaThi = txtCaThi.Text,
                PhongThi = txtPhongThi.Text,
                GiamThi = txtGiamThi.Text,
                TrangThai = "Chưa công bố"
            };

            danhSachLichThi.Add(lich);
            HienThiDanhSach();
            MessageBox.Show("✅ Đã thêm lịch thi mới!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            XoaForm();
        }

        // ===== SỬA =====
        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgLichThi.SelectedItem is not LichThi item)
            {
                MessageBox.Show("⚠️ Vui lòng chọn lịch thi để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            item.MonThi = txtMonThi.Text;
            item.NgayThi = dpNgayThi.SelectedDate ?? item.NgayThi;
            item.CaThi = txtCaThi.Text;
            item.PhongThi = txtPhongThi.Text;
            item.GiamThi = txtGiamThi.Text;

            HienThiDanhSach();
            MessageBox.Show("✅ Đã cập nhật lịch thi!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== XÓA =====
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgLichThi.SelectedItem is not LichThi item)
            {
                MessageBox.Show("⚠️ Vui lòng chọn lịch thi để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa lịch thi môn '{item.MonThi}' không?",
                                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                danhSachLichThi.Remove(item);
                HienThiDanhSach();
            }
        }

        // ===== CÔNG BỐ =====
        private void BtnCongBo_Click(object sender, RoutedEventArgs e)
        {
            if (dgLichThi.SelectedItem is not LichThi item)
            {
                MessageBox.Show("⚠️ Vui lòng chọn lịch thi để công bố!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            item.TrangThai = "Đã công bố";
            HienThiDanhSach();
            MessageBox.Show("📢 Lịch thi đã được công bố!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ===== XÓA FORM =====
        private void XoaForm()
        {
            txtMonThi.Clear();
            txtPhongThi.Clear();
            txtCaThi.Clear();
            txtGiamThi.Clear();
            dpNgayThi.SelectedDate = null;
            cbHocKy.SelectedIndex = -1;
        }
    }
}
