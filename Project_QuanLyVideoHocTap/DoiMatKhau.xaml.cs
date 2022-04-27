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
    /// Interaction logic for DoiMatKhau.xaml
    /// </summary>
    public partial class DoiMatKhau : Window
    {
        public DoiMatKhau()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_DoiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            string mkc = txb_MauKhauCu.Text;
            string mkm = txb_MatKhauMoi.Text;
            string nlmkm = txb_NhapLai.Text;
            if (mkc.Equals(""))
            {
                new ThongBao("Vui lòng nhập mật khẩu cũ!");
                return;
            }
            if (mkm.Equals(""))
            {
                new ThongBao("Vui lòng nhập mật khẩu mới!");
                return;
            }
            if (!mkm.Equals(nlmkm))
            {
                new ThongBao("Mật khẩu mới không trùng nhau!");
                return;
            }
            else
            {
                if (!CT_QuanLyVDHT.kTrDangNhap(CT_QuanLyVDHT.taiKhoan.tenDn, mkc))
                {
                    new ThongBao("Mật khẩu cũ không chính xác!");
                    return;
                }
                else
                {
                    CT_QuanLyVDHT.doiMk(CT_QuanLyVDHT.taiKhoan.idTk, CT_QuanLyVDHT.taiKhoan.tenDn, mkm);
                    new ThongBao("Thay đổi mật khấu thành công!");
                    this.Close();
                }
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
