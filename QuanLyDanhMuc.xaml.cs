using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class QuanLyDanhMuc : UserControl
    {
        private List<DanhMuc> dsKhoa = new();
        private List<DanhMuc> dsNganh = new();
        private List<DanhMuc> dsMonHoc = new();
        private List<DanhMuc> dsHocKy = new();

        public QuanLyDanhMuc()
        {
            InitializeComponent();
            LoadData();

            // ✅ Chờ control load xong rồi mới hiển thị dữ liệu
            this.Loaded += (s, e) => HienThiDanhMuc();
        }

        public class DanhMuc
        {
            public string Ma { get; set; } = string.Empty;
            public string Ten { get; set; } = string.Empty;
            public string MoTa { get; set; } = string.Empty;
        }

        // --- KHỞI TẠO DỮ LIỆU MẪU ---
        private void LoadData()
        {
            dsKhoa = new List<DanhMuc>
            {
                new DanhMuc { Ma = "CNTT", Ten = "Công nghệ thông tin", MoTa = "Khoa đào tạo ngành công nghệ thông tin" },
                new DanhMuc { Ma = "SP", Ten = "Sư phạm", MoTa = "Khoa đào tạo giáo viên" },
            };

            dsNganh = new List<DanhMuc>
            {
                new DanhMuc { Ma = "CNPM", Ten = "Công nghệ phần mềm", MoTa = "Ngành chuyên sâu về phát triển phần mềm" },
                new DanhMuc { Ma = "HTTT", Ten = "Hệ thống thông tin", MoTa = "Ngành về quản lý và thiết kế hệ thống thông tin" }
            };

            dsMonHoc = new List<DanhMuc>
            {
                new DanhMuc { Ma = "LTC#", Ten = "Lập trình C#", MoTa = "Môn học lập trình với ngôn ngữ C#" },
                new DanhMuc { Ma = "CSDL", Ten = "Cơ sở dữ liệu", MoTa = "Môn học về SQL Server và thiết kế cơ sở dữ liệu" }
            };

            dsHocKy = new List<DanhMuc>
            {
                new DanhMuc { Ma = "HK1", Ten = "Học kỳ 1", MoTa = "Học kỳ mùa xuân" },
                new DanhMuc { Ma = "HK2", Ten = "Học kỳ 2", MoTa = "Học kỳ mùa thu" }
            };
        }

        // --- HIỂN THỊ DỮ LIỆU LÊN DATAGRID ---
        private void HienThiDanhMuc()
        {
            if (cbLoaiDanhMuc == null || dgDanhMuc == null) return;

            string loai = (cbLoaiDanhMuc.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khoa";

            dgDanhMuc.ItemsSource = loai switch
            {
                "Khoa" => dsKhoa.ToList(),
                "Ngành" => dsNganh.ToList(),
                "Môn học" => dsMonHoc.ToList(),
                "Học kỳ" => dsHocKy.ToList(),
                _ => dsKhoa.ToList()
            };
        }

        private void cbLoaiDanhMuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HienThiDanhMuc();
        }

        // --- THÊM MỚI DANH MỤC ---
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            string loai = (cbLoaiDanhMuc.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khoa";
            var danhMuc = new DanhMuc { Ma = txtMaDanhMuc.Text, Ten = txtTenDanhMuc.Text, MoTa = txtMoTa.Text };

            var danhSach = loai switch
            {
                "Khoa" => dsKhoa,
                "Ngành" => dsNganh,
                "Môn học" => dsMonHoc,
                "Học kỳ" => dsHocKy,
                _ => dsKhoa
            };

            if (danhSach.Any(dm => dm.Ma == danhMuc.Ma))
            {
                MessageBox.Show($"❌ Mã {loai.ToLower()} '{danhMuc.Ma}' đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            danhSach.Add(danhMuc);
            HienThiDanhMuc();
            MessageBox.Show($"✅ Đã thêm {loai} thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // --- SỬA DANH MỤC ---
        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is not DanhMuc item)
            {
                MessageBox.Show("⚠️ Vui lòng chọn một dòng để sửa!");
                return;
            }

            item.Ten = txtTenDanhMuc.Text;
            item.MoTa = txtMoTa.Text;

            HienThiDanhMuc();
            MessageBox.Show("✅ Cập nhật thông tin thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // --- XÓA DANH MỤC ---
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is not DanhMuc item)
            {
                MessageBox.Show("⚠️ Vui lòng chọn một dòng để xóa!");
                return;
            }

            string loai = (cbLoaiDanhMuc.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khoa";

            switch (loai)
            {
                case "Khoa": dsKhoa.Remove(item); break;
                case "Ngành": dsNganh.Remove(item); break;
                case "Môn học": dsMonHoc.Remove(item); break;
                case "Học kỳ": dsHocKy.Remove(item); break;
            }

            HienThiDanhMuc();
            MessageBox.Show($"🗑️ Đã xóa {loai} thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // --- CHỌN DÒNG TRONG DATAGRID ---
        private void dgDanhMuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is DanhMuc item)
            {
                txtMaDanhMuc.Text = item.Ma;
                txtTenDanhMuc.Text = item.Ten;
                txtMoTa.Text = item.MoTa;
            }
        }

        private void txtMaDanhMuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is DanhMuc item)
                item.Ma = txtMaDanhMuc.Text;
        }

        private void txtTenDanhMuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is DanhMuc item)
                item.Ten = txtTenDanhMuc.Text;
        }

        private void txtMoTa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgDanhMuc.SelectedItem is DanhMuc item)
                item.MoTa = txtMoTa.Text;
        }
    }
}
