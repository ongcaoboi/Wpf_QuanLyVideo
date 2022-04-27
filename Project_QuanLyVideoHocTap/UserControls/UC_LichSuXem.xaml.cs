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
    /// Interaction logic for UC_LichSuXem.xaml
    /// </summary>
    public partial class UC_LichSuXem : UserControl
    {
        public List<Video> oject_lL;
        UC_PageVideos_InKh usr;
        public UC_LichSuXem()
        {
            InitializeComponent();
            oject_lL = CT_QuanLyVDHT.dsVdDaCoi(CT_QuanLyVDHT.taiKhoan.idTk);
            usr = new UC_PageVideos_InKh(oject_lL);
            Grid_List_LichSuCoi.Children.Add(usr);
            txb_TongSo.Text = CT_QuanLyVDHT.demTongLuotXem(CT_QuanLyVDHT.taiKhoan.idTk).ToString();
        }
    }
}
