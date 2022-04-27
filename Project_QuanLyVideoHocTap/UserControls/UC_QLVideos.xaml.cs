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
using Microsoft.Win32;
using Project_QuanLyVideoHocTap.Controller;
using Project_QuanLyVideoHocTap.Database;

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_QLVideos.xaml
    /// </summary>
    public partial class UC_QLVideos : UserControl
    {
        List<Video> list;
        List<Video> list2;
        private string dau = "idVd";
        private string cuoi = "Tang";
        public UC_QLVideos()
        {
            InitializeComponent();
            list = CT_QuanLyVDHT.loadVideos();
            lsv_Vd.ItemsSource = list;
        }
        private void clearData()
        {
            txb_idKh.Text = "";
            txb_TenVd.Text = "";
            txb_urlThumbails.Text = "";
            txb_urlVd.Text = "";
        }
        private void btn_Them_Click(object sender, RoutedEventArgs e)
        {
            int a = txb_idKh.Text.Equals("") ? 0 : int.Parse(txb_idKh.Text);
            if (a != 0)
            {
                if (CT_QuanLyVDHT.ktrKhTonTai(a))
                {
                    new ThongBao("Khoá học không tồn tại");
                    txb_idKh.Text = "";
                    return;
                }
            }
            if (!ktr())
            {
                new ThongBao("Hãy nhập đầy đủ thông tin!");
                return;
            }
            if (CT_QuanLyVDHT.themVd(txb_TenVd.Text, txb_urlThumbails.Text, txb_urlVd.Text, a))
                new ThongBao("Thêm video thành công!");
            else new ThongBao("Thêm video thất bại!");

            list = CT_QuanLyVDHT.loadVideos();
            lsv_Vd.ItemsSource = list;
            clearData();
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.xoaVideo(list[lsv_Vd.SelectedIndex].idVd);
            new ThongBao("Xoá video thành công!");

            list = CT_QuanLyVDHT.loadVideos();
            lsv_Vd.ItemsSource = list;
            clearData();
        }
        private bool ktr()
        {
            if (txb_TenVd.Text.Equals("") | txb_urlThumbails.Text.Equals("") | txb_urlVd.Text.Equals(""))
                return false;
            return true;
        }
        private void btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            int a = txb_idKh.Text.Equals("") ? 0 : int.Parse(txb_idKh.Text);
            if (a != 0)
            {
                if (CT_QuanLyVDHT.ktrKhTonTai(a))
                {
                    new ThongBao("Khoá học không tồn tại");
                    txb_idKh.Text = "";
                    return;
                }
            }
            if (!ktr())
            {
                new ThongBao("Hãy nhập đầy đủ thông tin!");
                return;
            }
            if (CT_QuanLyVDHT.suaVd(list[lsv_Vd.SelectedIndex].idVd,txb_TenVd.Text, txb_urlThumbails.Text, txb_urlVd.Text, a))
                new ThongBao("Sửa video thành công!");
            else new ThongBao("Sửa video thất bại!");

            list = CT_QuanLyVDHT.loadVideos();
            lsv_Vd.ItemsSource = list;
            clearData();
        }

        private void btn_ChonFVd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == true)
            {
                txb_urlVd.Text = openFileDialog1.FileName;
            }
        }

        private void btn_ChonFT_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == true)
            {
                txb_urlThumbails.Text = openFileDialog1.FileName;
            }
        }
        private void sapXep(string s)
        {
            if (list is null)
                list2 = new List<Video>();
            else list2 = list;
            list = new List<Video>();
            switch (s)
            {
                case "idVdTang": var a = list2.OrderBy(p => p.idVd); 
                    foreach (var t in a) list.Add(t); break;
                case "idVdGiam": var a1 = list2.OrderByDescending(p => p.idVd); 
                    foreach (var t in a1) list.Add(t); break;
                case "tenTang": var a2 = list2.OrderBy(p => p.tenVd);
                    foreach (var t in a2) list.Add(t); break;
                case "tenGiam": var a3 = list2.OrderByDescending(p => p.idVd);
                    foreach (var t in a3) list.Add(t); break;
                case "luotXemTang": var a4 = list2.OrderBy(p => p.luotXem);
                    foreach (var t in a4) list.Add(t); break;
                case "luotXemGiam": var a5 = list2.OrderByDescending(p => p.luotXem);
                    foreach (var t in a5) list.Add(t); break;
                case "idKhTang": var a6 = list2.OrderBy(p => p.idKh);
                    foreach (var t in a6) list.Add(t); break;
                case "idKhGiam": var a7 = list2.OrderByDescending(p => p.idKh); 
                    foreach (var t in a7) list.Add(t); break;
                case "ngayDangTang":
                    var a8 = list2.OrderBy(p => p.ngayDang);
                    foreach (var t in a8) list.Add(t); break;
                case "ngayDangGiam": var a9 = list2.OrderByDescending(p => p.ngayDang);
                    foreach (var t in a9) list.Add(t); break;
                default: break;
            }
            lsv_Vd.ItemsSource = list;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
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

        private void txb_idKh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(!string.IsNullOrEmpty(txb_idKh.Text) && txb_idKh.Text.All(Char.IsDigit)))
                txb_idKh.Text = "";
        }
    }
}
