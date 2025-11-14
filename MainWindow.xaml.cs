using System.Windows;
using System.Windows.Controls;

namespace QuanLySinhVien
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // ✅ Hiển thị trang chào mừng mặc định (không hiển thị Thống kê)
            MainFrame.Content = new TextBlock
            {
                Text = "Chào mừng đến với hệ thống Quản lý Sinh viên!",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Foreground = System.Windows.Media.Brushes.DarkBlue,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center
            };
        }

        // 🧭 Các sự kiện menu sidebar
        private void BtnDanhMuc_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyDanhMuc();
        }

        private void BtnLopHocPhan_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyLopHocPhan();
        }

        private void BtnDangKy_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyDangKyHocPhan();
        }

        private void BtnLichThi_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyLichThi();
        }

        private void BtnNguoiDung_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyNguoiDung();
        }

        private void BtnDiem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuanLyDiem();
        }

        private void BtnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ThongKeBaoCao();
        }

        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
