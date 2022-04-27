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
using Project_QuanLyVideoHocTap.Database;
using Project_QuanLyVideoHocTap.UserControls;
using Project_QuanLyVideoHocTap.Controller;

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Videos.xaml
    /// </summary>
    public partial class UC_Videos : UserControl
    {
        public List<Video> oject_lL;
        UC_ListVideos usr;
        public UC_Videos()
        {
            InitializeComponent();
            oject_lL = CT_QuanLyVDHT.loadVideos();
            usr = new UC_ListVideos(oject_lL);
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Danh sách trống ");
                a.Foreground = Brushes.White;
                Grid_UC_Videos_List.Children.Add(a);
            }else Grid_UC_Videos_List.Children.Add(usr);
        }

        private void btn_TimKiem_Click(object sender, RoutedEventArgs e)
        {
            oject_lL = CT_QuanLyVDHT.TimKiemVideos(txb_TimKiem.Text);
            Grid_UC_Videos_List.Children.Clear();
            usr = new UC_ListVideos(oject_lL);
            if(oject_lL.Count()==0&& !oject_lL.Safe().Any())
            {
                var a =CT_QuanLyVDHT.getTxb("Danh sách trống ");
                a.Foreground = Brushes.White;
                Grid_UC_Videos_List.Children.Add(a);
            }
            else Grid_UC_Videos_List.Children.Add(usr);
        }

        private void txb_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            oject_lL = CT_QuanLyVDHT.TimKiemVideos(txb_TimKiem.Text);
            Grid_UC_Videos_List.Children.Clear();
            usr = new UC_ListVideos(oject_lL);
            if (oject_lL.Count() == 0 && !oject_lL.Safe().Any())
            {
                var a = CT_QuanLyVDHT.getTxb("Danh sách trống ");
                a.Foreground = Brushes.White;
                Grid_UC_Videos_List.Children.Add(a);
            }
            else Grid_UC_Videos_List.Children.Add(usr);
        }
    }
}
