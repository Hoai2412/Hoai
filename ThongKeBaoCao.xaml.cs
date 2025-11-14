using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

#nullable disable

namespace QuanLySinhVien
{
    public partial class ThongKeBaoCao : UserControl
    {
        private List<Record> _db = new();
        private List<ReportRow> _view = new();

        public ThongKeBaoCao()
        {
            InitializeComponent();
            SeedData();
            InitFilters();
            BuildReport();
        }

        #region ==== Model ====
        public class Record
        {
            public string MaSV { get; set; } = "";
            public string HoTen { get; set; } = "";
            public string Khoa { get; set; } = "";
            public string Nganh { get; set; } = "";
            public string MaMon { get; set; } = "";
            public string TenMon { get; set; } = "";
            public string LopHP { get; set; } = "";
            public string HocKy { get; set; } = "";
            public int TinChi { get; set; }
            public double DiemGK { get; set; }
            public double DiemCK { get; set; }
        }

        public class ReportRow
        {
            public string MaSV { get; set; } = "";
            public string HoTen { get; set; } = "";
            public string Khoa { get; set; } = "";
            public string Nganh { get; set; } = "";
            public string TenMon { get; set; } = "";
            public string LopHP { get; set; } = "";
            public string HocKy { get; set; } = "";
            public int TinChi { get; set; }
            public double DiemGK { get; set; }
            public double DiemCK { get; set; }
            public double DiemTK { get; set; }
            public string KetQua { get; set; } = "";
        }
        #endregion

        #region ==== Seed dữ liệu + Filters ====
        private void SeedData()
        {
            _db = new List<Record>
            {
                new() { MaSV="SV001", HoTen="Nguyễn Văn A", Khoa="CNTT", Nganh="CNPM", MaMon="LTC#", TenMon="Lập trình C#", LopHP="LTC#-01", HocKy="HK1/2025-2026", TinChi=3, DiemGK=8.2, DiemCK=7.8 },
                new() { MaSV="SV002", HoTen="Trần Thị B",   Khoa="CNTT", Nganh="HTTT", MaMon="CSDL", TenMon="Cơ sở dữ liệu", LopHP="CSDL-02", HocKy="HK1/2025-2026", TinChi=3, DiemGK=7.0, DiemCK=8.4 },
                new() { MaSV="SV003", HoTen="Lê Văn C",     Khoa="SP",   Nganh="Toán",  MaMon="XSTK", TenMon="Xác suất thống kê", LopHP="XSTK-01", HocKy="HK2/2025-2026", TinChi=3, DiemGK=5.5, DiemCK=6.0 },
                new() { MaSV="SV001", HoTen="Nguyễn Văn A", Khoa="CNTT", Nganh="CNPM", MaMon="CSDL", TenMon="Cơ sở dữ liệu", LopHP="CSDL-01", HocKy="HK2/2025-2026", TinChi=3, DiemGK=8.0, DiemCK=8.5 },
            };
        }

        private void InitFilters()
        {
            void Fill(ComboBox cb, IEnumerable<string> items)
            {
                cb.Items.Clear();
                cb.Items.Add("Tất cả");
                foreach (var it in items.Distinct().OrderBy(x => x))
                    cb.Items.Add(it);
                cb.SelectedIndex = 0;
            }

            Fill(cbKhoa, _db.Select(x => x.Khoa));
            Fill(cbNganh, _db.Select(x => x.Nganh));
            Fill(cbMon, _db.Select(x => x.TenMon));
            Fill(cbLopHP, _db.Select(x => x.LopHP));
            Fill(cbHocKy, _db.Select(x => x.HocKy));
        }
        #endregion

        #region ==== Xây dựng báo cáo + lọc ====
        private void BuildReport()
        {
            string khoa = cbKhoa.SelectedItem?.ToString() ?? "Tất cả";
            string nganh = cbNganh.SelectedItem?.ToString() ?? "Tất cả";
            string mon = cbMon.SelectedItem?.ToString() ?? "Tất cả";
            string lop = cbLopHP.SelectedItem?.ToString() ?? "Tất cả";
            string hk = cbHocKy.SelectedItem?.ToString() ?? "Tất cả";
            string kw = (txtSearch.Text ?? "").Trim().ToLower();

            var q = _db.AsEnumerable();

            if (khoa != "Tất cả") q = q.Where(x => x.Khoa == khoa);
            if (nganh != "Tất cả") q = q.Where(x => x.Nganh == nganh);
            if (mon != "Tất cả") q = q.Where(x => x.TenMon == mon);
            if (lop != "Tất cả") q = q.Where(x => x.LopHP == lop);
            if (hk != "Tất cả") q = q.Where(x => x.HocKy == hk);
            if (!string.IsNullOrEmpty(kw))
                q = q.Where(x => x.MaSV.ToLower().Contains(kw) || x.HoTen.ToLower().Contains(kw));

            _view = q.Select(x => new ReportRow
            {
                MaSV = x.MaSV,
                HoTen = x.HoTen,
                Khoa = x.Khoa,
                Nganh = x.Nganh,
                TenMon = x.TenMon,
                LopHP = x.LopHP,
                HocKy = x.HocKy,
                TinChi = x.TinChi,
                DiemGK = x.DiemGK,
                DiemCK = x.DiemCK,
                DiemTK = Math.Round(x.DiemGK * 0.4 + x.DiemCK * 0.6, 2),
                KetQua = (x.DiemGK * 0.4 + x.DiemCK * 0.6) >= 5.0 ? "Đạt" : "Không đạt"
            }).OrderBy(r => r.MaSV).ThenBy(r => r.TenMon).ToList();

            dgBaoCao.ItemsSource = null;
            dgBaoCao.ItemsSource = _view;
        }

        private void Filter_Changed(object sender, EventArgs e) => BuildReport();
        private void BtnTongHop_Click(object sender, RoutedEventArgs e) => BuildReport();
        #endregion

        #region ==== Xuất Excel ====
        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (_view.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = "BaoCaoHocTap.xlsx"
            };
            if (sfd.ShowDialog() != true) return;

            try
            {
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Báo cáo");

                string[] headers = { "MSSV", "Họ tên", "Khoa", "Ngành", "Môn học", "Lớp HP", "Học kỳ", "TC", "GK", "CK", "Tổng kết", "Kết quả" };
                for (int c = 0; c < headers.Length; c++)
                {
                    ws.Cell(1, c + 1).Value = headers[c];
                    ws.Cell(1, c + 1).Style.Font.Bold = true;
                }

                for (int i = 0; i < _view.Count; i++)
                {
                    var r = _view[i];
                    int row = i + 2;
                    ws.Cell(row, 1).Value = r.MaSV;
                    ws.Cell(row, 2).Value = r.HoTen;
                    ws.Cell(row, 3).Value = r.Khoa;
                    ws.Cell(row, 4).Value = r.Nganh;
                    ws.Cell(row, 5).Value = r.TenMon;
                    ws.Cell(row, 6).Value = r.LopHP;
                    ws.Cell(row, 7).Value = r.HocKy;
                    ws.Cell(row, 8).Value = r.TinChi;
                    ws.Cell(row, 9).Value = r.DiemGK;
                    ws.Cell(row, 10).Value = r.DiemCK;
                    ws.Cell(row, 11).Value = r.DiemTK;
                    ws.Cell(row, 12).Value = r.KetQua;
                }

                ws.Columns().AdjustToContents();
                wb.SaveAs(sfd.FileName);
                MessageBox.Show("✅ Xuất Excel thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region ==== Xuất PDF ====
        private void BtnPdf_Click(object sender, RoutedEventArgs e)
        {
            if (_view.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var sfd = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = "BaoCaoHocTap.pdf"
            };
            if (sfd.ShowDialog() != true) return;

            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                var doc = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(20);
                        page.Header().Text("BÁO CÁO HỌC TẬP").SemiBold().FontSize(18).AlignCenter();
                        page.Content().Table(table =>
                        {
                            var cols = new[] { "MSSV", "Họ tên", "Khoa", "Ngành", "Môn học", "Lớp HP", "Học kỳ", "TC", "GK", "CK", "Tổng kết", "KQ" };
                            table.ColumnsDefinition(c =>
                            {
                                for (int i = 0; i < cols.Length; i++) c.RelativeColumn();
                            });

                            foreach (var h in cols)
                                table.Cell().Element(CellHeader).Text(h);

                            foreach (var r in _view)
                            {
                                table.Cell().Element(Cell).Text(r.MaSV);
                                table.Cell().Element(Cell).Text(r.HoTen);
                                table.Cell().Element(Cell).Text(r.Khoa);
                                table.Cell().Element(Cell).Text(r.Nganh);
                                table.Cell().Element(Cell).Text(r.TenMon);
                                table.Cell().Element(Cell).Text(r.LopHP);
                                table.Cell().Element(Cell).Text(r.HocKy);
                                table.Cell().Element(Cell).Text(r.TinChi.ToString());
                                table.Cell().Element(Cell).Text(r.DiemGK.ToString("0.##"));
                                table.Cell().Element(Cell).Text(r.DiemCK.ToString("0.##"));
                                table.Cell().Element(Cell).Text(r.DiemTK.ToString("0.##"));
                                table.Cell().Element(Cell).Text(r.KetQua);
                            }

                            static IContainer Cell(IContainer x) => x.Border(0.5f).Padding(3).AlignLeft().MinHeight(14);
                            static IContainer CellHeader(IContainer x) => x.Border(0.5f).Background("#F1F5F9").Padding(3).AlignCenter().MinHeight(16);
                        });
                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Xuất lúc: ").SemiBold();
                            txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        });
                    });
                });

                doc.GeneratePdf(sfd.FileName);
                MessageBox.Show("✅ Xuất PDF thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
