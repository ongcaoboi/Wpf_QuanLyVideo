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

namespace Project_QuanLyVideoHocTap.UserControls
{
    /// <summary>
    /// Interaction logic for UC_ListVideos.xaml
    /// </summary>
    public partial class UC_ListVideos : UserControl
    {
        public List<Video> oject_lL;
        public UC_ListVideos(List<Video> s)
        {
            InitializeComponent();
            oject_lL = s;
            ListViewVideo.ItemsSource = oject_lL;
        }

        private void ListViewVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new XemVideo((Video)oject_lL[ListViewVideo.SelectedIndex]);
        }
    }
}
