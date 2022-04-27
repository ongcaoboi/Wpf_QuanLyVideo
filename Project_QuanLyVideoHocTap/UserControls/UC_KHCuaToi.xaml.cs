using Project_QuanLyVideoHocTap.Controller;
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

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_KHCuaToi.xaml
    /// </summary>
    public partial class UC_KHCuaToi : UserControl
    {
        public List<KhoaHoc> oject_lL;
        UC_ListKhoaHoc usr;
        public UC_KHCuaToi()
        {
            InitializeComponent();
            oject_lL = CT_QuanLyVDHT.dsKhoaHocCuaToi(CT_QuanLyVDHT.taiKhoan.idTk);
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHocCuaToi");
            txb_TongSo.Text = CT_QuanLyVDHT.tongSoKhoaHocDK(CT_QuanLyVDHT.taiKhoan.idTk).ToString();
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Bạn chưa đăng ký khoá học nào!");
                a.Foreground = Brushes.White;
                Grid_List_KhoaHocCuaToi.Children.Add(a);
            }
            else Grid_List_KhoaHocCuaToi.Children.Add(usr);
        }

        private void btn_QuayLai_Click(object sender, RoutedEventArgs e)
        {
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHocCuaToi");
            Grid_List_KhoaHocCuaToi.Children.Clear();
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Bạn chưa đăng ký khoá học nào!");
                a.Foreground = Brushes.White;
                Grid_List_KhoaHocCuaToi.Children.Add(a);
            }
            else Grid_List_KhoaHocCuaToi.Children.Add(usr);
            txb_TongSo.Text = CT_QuanLyVDHT.tongSoKhoaHocDK(CT_QuanLyVDHT.taiKhoan.idTk).ToString();
        }
    }
}
