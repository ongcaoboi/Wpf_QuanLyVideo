using Project_QuanLyVideoHocTap.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_QuanLyVideoHocTap.Controller;

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_PageVideos_InKh.xaml
    /// </summary>
    public partial class UC_PageVideos_InKh : UserControl
    {
        public List<Video> oject_lL;
        public int id;
        public string typeOf;
        public UC_PageVideos_InKh(List<Video> s, int al, string t)
        {
            InitializeComponent();
            oject_lL = s;
            List_VdInKh.ItemsSource = oject_lL;
            id = al; typeOf = t;
            txb_SoVd.Text = CT_QuanLyVDHT.demSumVideos(al).ToString();
            switch (typeOf)
            {
                case "ListKhoaHoc":
                    btn_DangKyKhoaHoc.Visibility = Visibility.Visible;
                    btn_HuyDangKy.Visibility = Visibility.Collapsed;
                    if (CT_QuanLyVDHT.isAdmin) btn_XoaKhoaHoc.Visibility = Visibility.Visible;
                    break;
                case "ListKhoaHocCuaToi":
                    btn_DangKyKhoaHoc.Visibility = Visibility.Collapsed;
                    btn_HuyDangKy.Visibility = Visibility.Visible;
                    break;
            }
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Danh sách trống!");
                a.Foreground = Brushes.White;
                Grid_List.Children.Add(a);
            }
        }
        public UC_PageVideos_InKh(List<Video> s)
        {
            InitializeComponent();
            oject_lL = s;
            txb_SoVd.Text = "";
            txb_SoLuongVd.Text = "Lịch sử xem";
            btn_DangKyKhoaHoc.Visibility = Visibility.Collapsed;
            btn_HuyDangKy.Visibility = Visibility.Collapsed;
            List_VdInKh.ItemsSource = oject_lL;
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Danh sách trống!");
                a.Foreground = Brushes.White;
                Grid_List.Children.Add(a);
            }
        }

        private void List_VdInKh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new XemVideo((Video)oject_lL[List_VdInKh.SelectedIndex]);
        }

        private void btn_DangKyKhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            if (CT_QuanLyVDHT.dkKhoaHoc(id, CT_QuanLyVDHT.taiKhoan.idTk))
                new ThongBao("Đăng Ký Khoá học thành công!");
            else
                new ThongBao("Khoá học này đã có trong danh sách của bạn!");
        }

        private void btn_HuyDangKy_Click(object sender, RoutedEventArgs e)
        {
            if (CT_QuanLyVDHT.huyDangKy(id, CT_QuanLyVDHT.taiKhoan.idTk))
            {
                new ThongBao("Huỷ đăng Ký Khoá học thành công!");
                CT_QuanLyVDHT.Main.Grid_Main.Children.Clear();
                CT_QuanLyVDHT.Main.Grid_Main.Children.Add(CT_QuanLyVDHT.khoaHocCuaToi = new UC_KHCuaToi());
            }
            else
                new ThongBao("Thất bại");
        }

        private void btn_XoaKhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.xoaKhoaHoc(id);
            new ThongBao("Đã xoá thành công!");
            CT_QuanLyVDHT.Main.Grid_Main.Children.Clear();
            CT_QuanLyVDHT.Main.Grid_Main.Children.Add(CT_QuanLyVDHT.khoaHocCuaToi = new UC_KHCuaToi());
        }
    }
}
