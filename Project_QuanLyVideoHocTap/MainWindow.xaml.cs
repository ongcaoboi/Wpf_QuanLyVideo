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
using System.Windows.Threading;
using Project_QuanLyVideoHocTap.UserControls;
using Project_QuanLyVideoHocTap.Controller;

namespace Project_QuanLyVideoHocTap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double witdh;
        public double height;
        public MainWindow()
        {
            InitializeComponent();

            btn_RestoreSize.Visibility = Visibility.Collapsed;
            double x = this.Width;
            double y = this.Height;
            witdh = x;
            height = y;
            this.Width = x / 1.25;
            this.Height = y / 1.25;
            if (!CT_QuanLyVDHT.isAdmin)
            {
                it_TrangChu.Visibility = Visibility.Collapsed;
                ListViewMenu.SelectedItem = it_Videos;
            }
            else ListViewMenu.SelectedItem = it_TrangChu;
            txbl_Account.Text = CT_QuanLyVDHT.taiKhoan.tenDn;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            Grid_Menu.Width = 170;
            Grid_Page.Width = witdh - Grid_Menu.Width;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            Grid_Menu.Width = 44;
            Grid_Page.Width = witdh - Grid_Menu.Width;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid_Main.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "it_TrangChu":
                    Grid_Main.Children.Add(CT_QuanLyVDHT.trangChu = new UC_TrangChu());
                    break;
                case "it_Videos":
                    Grid_Main.Children.Add(CT_QuanLyVDHT.videos = new UC_Videos());
                    break;
                case "it_KhoaHoc":
                    Grid_Main.Children.Add(CT_QuanLyVDHT.khoaHoc = new UC_KhoaHoc());
                    break;
                case "it_KHDangTheo":
                    Grid_Main.Children.Add(CT_QuanLyVDHT.khoaHocCuaToi = new UC_KHCuaToi());
                    break;
                case "it_LichSuXem":
                    Grid_Main.Children.Add(CT_QuanLyVDHT.lichSuCoi = new UC_LichSuXem());
                    break;
                default:
                    break;
            }
        }

        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            btn_Maximize.Visibility = Visibility.Collapsed;
            btn_RestoreSize.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Maximized;
        }

        private void btn_RestoreSize_Click(object sender, RoutedEventArgs e)
        {
            btn_Maximize.Visibility = Visibility.Visible;
            btn_RestoreSize.Visibility = Visibility.Collapsed;
            this.WindowState = WindowState.Normal;
        }

        private void btn_An_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
                this.WindowState = WindowState.Minimized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void btn_Account_Click(object sender, RoutedEventArgs e)
        {
            ctm_Acc.PlacementTarget = sender as Button;
            ctm_Acc.IsOpen = true;
        }

        private void btn_DoiMatKhau(object sender, RoutedEventArgs e)
        {
            new DoiMatKhau();
        }

        private void btn_DangXuat(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.isAdmin = false;
            CT_QuanLyVDHT.taiKhoan = null;
            CT_QuanLyVDHT.trangChu = null;
            CT_QuanLyVDHT.videos = null;
            CT_QuanLyVDHT.khoaHoc = null;
            CT_QuanLyVDHT.khoaHocCuaToi = null;
            CT_QuanLyVDHT.lichSuCoi = null;
            CT_QuanLyVDHT.Main = null;
            CT_QuanLyVDHT.cuaSoLogin = new Login();
            CT_QuanLyVDHT.cuaSoLogin.Show();
            this.Close();
        }
    }
}
