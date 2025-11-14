using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QuanLySinhVien
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            // Màu chủ đạo (xanh đậm giống ảnh)
            theme.SetPrimaryColor(System.Windows.Media.Color.FromRgb(30, 58, 138)); // #1E3A8A
                                                                                    // Màu nhấn (xanh nhạt)
            theme.SetSecondaryColor(System.Windows.Media.Color.FromRgb(59, 130, 246)); // #3B82F6

            paletteHelper.SetTheme(theme);
        }

    }
}
