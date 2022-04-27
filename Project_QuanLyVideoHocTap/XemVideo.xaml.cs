using Project_QuanLyVideoHocTap.Database;
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
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace Project_QuanLyVideoHocTap
{
    /// <summary>
    /// Interaction logic for XemVideo.xaml
    /// </summary>
    public partial class XemVideo : Window
    {
        Video a;
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        public XemVideo(Video vd)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            Thread xem = new Thread(xemVd);
            xem.Start();
            a = vd;
            Grid_Video.DataContext = vd;
            txb_TenKhCuaVd.Text = CT_QuanLyVDHT.layTenKhCuaVd(vd);
            if (CT_QuanLyVDHT.isAdmin)
            {
                btn_XoaVideo.Visibility = Visibility.Visible;
                if(!(a.idKh is null))
                    btn_XoaVideoKhoiKhoaHoc.Visibility = Visibility.Visible;
            }
            txb_Tk.Text = CT_QuanLyVDHT.taiKhoan.tenDn;
            ListViewBinhLuan.ItemsSource = CT_QuanLyVDHT.layNDBinhLuan(vd.idVd);
            mePlayer.Play();
            this.ShowDialog();
        }
        public void xemVd()
        {
            Thread.Sleep(5000);
            CT_QuanLyVDHT.xemVd(a.idVd);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }
        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_BinhLuan_Click(object sender, RoutedEventArgs e)
        {
            if (txb_NoiDungBl.Text.Equals("")) return;
            CT_QuanLyVDHT.binhLuanVoVd(a.idVd, txb_NoiDungBl.Text);
            ListViewBinhLuan.ItemsSource = CT_QuanLyVDHT.layNDBinhLuan(a.idVd);
            txb_NoiDungBl.Text = "";
        }

        private void btn_ThemKhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            int? idKh = a.idKh;
            if (idKh is null) new ThongBao("Video này chưa có khoá học!");
            else
            {
                if (CT_QuanLyVDHT.dkKhoaHoc(int.Parse(idKh.ToString()), CT_QuanLyVDHT.taiKhoan.idTk))
                    new ThongBao("Đăng Ký Khoá học thành công!");
                else
                    new ThongBao("Khoá học này đã có trong danh sách của bạn!");
            }
        }

        private void btn_XoaVideoKhoiKhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.xoaVideoKhoiKh(a.idVd);
            new ThongBao("Đã xoá video Khỏi khoá học!");
            CT_QuanLyVDHT.reloadData();
        }

        private void btn_XoaVideo_Click(object sender, RoutedEventArgs e)
        {
            CT_QuanLyVDHT.xoaVideo(a.idVd);
            new ThongBao("Đã xoá video!");
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
