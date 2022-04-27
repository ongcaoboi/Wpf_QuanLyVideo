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
    /// Interaction logic for ThongBao.xaml
    /// </summary>
    public partial class ThongBao : Window
    {
        public ThongBao(string tieuDe, string noiDung)
        {
            InitializeComponent();
            txb_TieuDe.Text = tieuDe;
            txb_ThongBao.Text = noiDung;
            this.ShowDialog();
        }
        public ThongBao(string noiDung)
        {
            InitializeComponent();
            txb_TieuDe.Text = "Thông báo";
            txb_ThongBao.Text = noiDung;
            this.ShowDialog();
        }

        private void btn_ThongBao_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
