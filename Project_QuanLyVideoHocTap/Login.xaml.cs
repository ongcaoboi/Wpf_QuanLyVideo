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
using System.Windows.Shapes;
using Project_QuanLyVideoHocTap.Controller;

namespace Project_QuanLyVideoHocTap
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_DangNhap_Click(object sender, RoutedEventArgs e)
        {
            if (CT_QuanLyVDHT.dangNhap(txb_TaiKhoan.Text, txb_MatKhau.Password))
            {
                CT_QuanLyVDHT.Main = new MainWindow();
                this.Close();
                CT_QuanLyVDHT.Main.ShowDialog();
            }
            else
            {
                new ThongBao("Đăng nhập thất bại");
                txb_TaiKhoan.Text = "";
                txb_MatKhau.Password = "";
                txb_TaiKhoan.Focus();
            }
        }
    }
}
