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
    /// Interaction logic for UC_ListKhoaHoc.xaml
    /// </summary>
    public partial class UC_ListKhoaHoc : UserControl
    {
        public string typeOf;
        public List<KhoaHoc> oject_lL;
        public UC_ListKhoaHoc(List<KhoaHoc> s, string t)
        {
            InitializeComponent();
            oject_lL = s; typeOf = t;
            List_KhoaHoc.ItemsSource = oject_lL;
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Danh sách trống!");
                a.Foreground = Brushes.White;
                Grid_Kh_Videos.Children.Add(a);
            }
        }

        private void List_KhoaHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = ((KhoaHoc)oject_lL[List_KhoaHoc.SelectedIndex]).idKh;
            Grid_Kh_Videos.Children.Add(new UC_PageVideos_InKh(CT_QuanLyVDHT.loadVideosTheoKh(a), a, typeOf));
        }
    }
}
