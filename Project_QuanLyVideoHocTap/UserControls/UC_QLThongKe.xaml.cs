using Project_QuanLyVideoHocTap.Controller;
using System;
using System.IO;
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

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_QLThongKe.xaml
    /// </summary>
    public partial class UC_QLThongKe : UserControl
    {
        private DateTime ngayBd;
        private DateTime ngayKt;
        public UC_QLThongKe()
        {
            InitializeComponent();
        }

        private void btn_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[11];
            lines[0] = "                  Thống kê!";
            lines[1] = "";
            lines[2] = "Ngày bắt đầu:                    " + txb_NgayBatDau.Text;
            lines[3] = "Ngày kết thúc:                   " + txb_NgayKetThuc.Text;
            lines[4] = "Tổng lượt xem:                   " + txb_TongView.Text;
            lines[5] = "Tổng bình luận:                  " + txb_TongBL.Text;
            lines[6] = "Video được xem nhiều nhất:       " + txb_VdXemNN.Text;
            lines[7] = "Video có nhiều bình luận nhất:   " + txb_VdBlNN.Text;
            lines[8] = "Khoá học có nhiều lượt xem nhất: " + txb_KhViewNN.Text;
            lines[9] = "Tài khoản xem nhiều nhất:        " + txb_TKXemNN.Text;
            lines[10] = "Tài khoản bình luận nhiều nhẩt: " + txb_TkBlNN.Text;

            SaveFileDialog saveFileDialog = new SaveFileDialog(); 
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                System.IO.File.WriteAllLines(saveFileDialog.FileName, lines);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            ngayBd = (DateTime)date;
            updateData();
        }

        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            ngayKt = (DateTime)date;
            updateData();
        }
        private void updateData()
        {
            txb_NgayBatDau.Text = layNgay(ngayBd);
            txb_NgayKetThuc.Text = layNgay(ngayKt);
            var a = CT_QuanLyVDHT.LayTk(ngayBd, ngayKt);
            txb_TongView.Text = a.tongView.ToString();
            txb_TongBL.Text = a.tongBl.ToString();
            txb_VdXemNN.Text = a.tenVdXemNn;
            txb_VdBlNN.Text = a.tenVdBlNn;
            txb_KhViewNN.Text = a.tenKhXemNn;
            txb_TKXemNN.Text = a.tenTkBlNn;
            txb_TkBlNN.Text = a.tenTkBlNn;
        }
        private string layNgay(DateTime date)
        {
            return date == null ? "" : date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();
        }
    }
}
