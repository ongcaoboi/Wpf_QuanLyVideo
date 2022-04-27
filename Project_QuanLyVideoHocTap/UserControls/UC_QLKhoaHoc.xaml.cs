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
    /// Interaction logic for UC_QLKhoaHoc.xaml
    /// </summary>
    public partial class UC_QLKhoaHoc : UserControl
    {
        List<KhoaHoc> list;
        List<KhoaHoc> list2;
        private string dau = "idKh";
        private string cuoi = "Tang";
        public UC_QLKhoaHoc()
        {
            InitializeComponent();
            list = CT_QuanLyVDHT.loadKhoaHoc();
            lsv_Kh.ItemsSource = list;
        }

        private void btn_Them_Click(object sender, RoutedEventArgs e)
        {
            if (txb_TenKh.Text.Equals(""))
            {
                new ThongBao("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (CT_QuanLyVDHT.themKh(txb_TenKh.Text))
                new ThongBao("Thêm Khoá học thành công!");
            else new ThongBao("Thêm Khoá học thất bại!");
            list = CT_QuanLyVDHT.loadKhoaHoc();
            lsv_Kh.ItemsSource = list;
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.xoaKhoaHoc(list[lsv_Kh.SelectedIndex].idKh);
            new ThongBao("Xoá khoá học thành công!");
            list = CT_QuanLyVDHT.loadKhoaHoc();
            lsv_Kh.ItemsSource = list;
        }

        private void btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            if (txb_TenKh.Text.Equals(""))
            {
                new ThongBao("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (CT_QuanLyVDHT.suaKh(list[lsv_Kh.SelectedIndex].idKh,txb_TenKh.Text))
                new ThongBao("Sửa Khoá học thành công!");
            else new ThongBao("Sửa Khoá học thất bại!");
            list = CT_QuanLyVDHT.loadKhoaHoc();
            lsv_Kh.ItemsSource = list;
        }
        private void sapXep(string s)
        {
            if (list is null)
                list2 = CT_QuanLyVDHT.loadKhoaHoc();
            else list2 = list;
            list = new List<KhoaHoc>();
            switch (s)
            {
                case "idKhTang": var a = list2.OrderBy(p => p.idKh);
                    foreach (var t in a) list.Add(t); break;
                case "idKhGiam": var a1 = list2.OrderByDescending(p => p.idKh);
                    foreach (var t in a1) list.Add(t); break;
                case "tenTang": var a2 = list2.OrderBy(p => p.tenKh);
                    foreach (var t in a2) list.Add(t); break;
                case "tenGiam": var a3 = list2.OrderByDescending(p => p.tenKh);
                    foreach (var t in a3) list.Add(t); break;
                case "ngayTaoTang": var a4 = list2.OrderBy(p => p.ngayTao);
                    foreach (var t in a4) list.Add(t); break;
                case "ngayTaoGiam": var a5 = list2.OrderByDescending(p => p.ngayTao);
                    foreach (var t in a5) list.Add(t); break;
                default: break;
            }
            lsv_Kh.ItemsSource = list;
        }

        private void idKh_Checked(object sender, RoutedEventArgs e)
        {
            var a = sender as RadioButton;
            dau = a.Name.ToString();
            sapXep(dau + cuoi);
        }

        private void Tang_Checked(object sender, RoutedEventArgs e)
        {
            var a = sender as RadioButton;
            cuoi = a.Name.ToString();
            sapXep(dau + cuoi);
        }
    }
}
