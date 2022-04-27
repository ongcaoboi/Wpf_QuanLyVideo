using Project_QuanLyVideoHocTap.Controller;
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

namespace Project_QuanLyVideoHocTap
{
    /// <summary>
    /// Interaction logic for Create_User.xaml
    /// </summary>
    public partial class Create_User : Window
    {
        public Create_User()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void Btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            string tk = txb_TaiKhoan.Text;
            string mk = txb_MatKhau.Text;
            if (tk.Equals(""))
            {
                new ThongBao("Vui lòng nhập tài khoản!");
            }
            if (tk.Equals(""))
            {
                new ThongBao("Vui lòng nhập mật khẩu!");
            }
            if (CT_QuanLyVDHT.createUser(tk, mk))
            {
                new ThongBao("Tạo tài khoản thành công!");
            }
            else
            {
                new ThongBao("Tài khoản đã tồn tại!");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }   
    }
}
