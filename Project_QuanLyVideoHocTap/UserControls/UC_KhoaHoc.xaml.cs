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
using Project_QuanLyVideoHocTap.Database;

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_KhoaHoc.xaml
    /// </summary>
    public partial class UC_KhoaHoc : UserControl
    {
        public List<KhoaHoc> oject_lL;
        UC_ListKhoaHoc usr;
        public UC_KhoaHoc()
        {
            InitializeComponent();
            oject_lL = CT_QuanLyVDHT.loadKhoaHoc();
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHoc");
            Grid_UC_KhoaHoc_List.Children.Add(usr);
        }

        private void btn_TimKiem_Click(object sender, RoutedEventArgs e)
        {
            oject_lL = CT_QuanLyVDHT.TimKiemKhoaHoc(txb_TimKiem.Text);
            Grid_UC_KhoaHoc_List.Children.Clear();
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHoc");
            Grid_UC_KhoaHoc_List.Children.Add(usr);
        }

        private void btn_QuayLai_Click(object sender, RoutedEventArgs e)
        {
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHoc");
            Grid_UC_KhoaHoc_List.Children.Clear();
            Grid_UC_KhoaHoc_List.Children.Add(usr);
        }

        private void txb_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            oject_lL = CT_QuanLyVDHT.TimKiemKhoaHoc(txb_TimKiem.Text);
            Grid_UC_KhoaHoc_List.Children.Clear();
            usr = new UC_ListKhoaHoc(oject_lL, "ListKhoaHoc");
            Grid_UC_KhoaHoc_List.Children.Add(usr);
        }
    }
}
