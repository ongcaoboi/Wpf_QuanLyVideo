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
    /// Interaction logic for UC_TrangChu.xaml
    /// </summary>
    public partial class UC_TrangChu : UserControl
    {
        public UC_TrangChu()
        {
            InitializeComponent();
            txb_TieuDe.Text = "Quản lý Videos!";
            Grid_QuanLy.Children.Add(new UC_QLVideos());
        }
        private void mn_ItVideo_Click(object sender, RoutedEventArgs e)
        {
            Grid_QuanLy.Children.Clear();
            txb_TieuDe.Text = "Quản lý Videos!";
            Grid_QuanLy.Children.Add(new UC_QLVideos());
        }

        private void mn_ItKhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            txb_TieuDe.Text = "Quản lý Khoá học!";
            Grid_QuanLy.Children.Clear();
            Grid_QuanLy.Children.Add(new UC_QLKhoaHoc());
        }

        private void mn_ItThongke_Click(object sender, RoutedEventArgs e)
        {
            txb_TieuDe.Text = "";
            Grid_QuanLy.Children.Clear();
            Grid_QuanLy.Children.Add(new UC_QLThongKe());
        }

        private void mn_ItChucNang_Click(object sender, RoutedEventArgs e)
        {
            new Create_User();
        }
    }
}
