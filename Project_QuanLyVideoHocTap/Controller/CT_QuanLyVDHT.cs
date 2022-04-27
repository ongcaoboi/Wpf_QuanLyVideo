using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Project_QuanLyVideoHocTap.Database;
using Project_QuanLyVideoHocTap.UserControls;

namespace Project_QuanLyVideoHocTap.Controller
{
    /* Đây là lớp thực hiện các truy vấn tới cơ sở dữ liệu và là nơi lưu trữ các dữ liệu trung gian
     * của các cửa sổ chức năng.
     */
    public static class CT_QuanLyVDHT
    {
        
        static DataQLBHDataContext db = new DataQLBHDataContext();
        public static Login cuaSoLogin;
        public static UC_TrangChu trangChu;
        public static UC_Videos videos;
        public static UC_KhoaHoc khoaHoc;
        public static UC_KHCuaToi khoaHocCuaToi;
        public static UC_LichSuXem lichSuCoi;
        public static MainWindow Main;
        public static bool isAdmin = false;
        public static TaiKhoan taiKhoan;

        public static void reloadData()
        {
            UC_TrangChu trangChu = new UC_TrangChu();
            UC_Videos videos = new UC_Videos();
            UC_KhoaHoc khoaHoc = new UC_KhoaHoc();
            UC_KHCuaToi khoaHocCuaToi = new UC_KHCuaToi();
            UC_LichSuXem lichSuCoi = new UC_LichSuXem();
        }
        public static List<Video> TimKiemVideos(string a)
        {
            return new List<Video> (db.sp_TimVd(a).ToList());
        }
        public static List<Video> loadVideos()
        {
            return new List<Video> (db.sp_XemDsVd().ToList());
        }
        public static List<KhoaHoc> loadKhoaHoc()
        {
            return new List<KhoaHoc> (db.sp_XemDsKh().ToList());
        }
        public static List<KhoaHoc> TimKiemKhoaHoc(string a)
        {
            return new List<KhoaHoc>(db.sp_TimKh(a).ToList());
        }
        public static List<KhoaHoc> dsKhoaHocCuaToi(int id)
        {
            return new List<KhoaHoc>(from kh in db.KhoaHocs
                    join dk in db.DKKHs on kh.idKh equals dk.idKh
                    where dk.idTk == id
                    select kh);
        }
        public static int tongSoKhoaHocDK(int id)
        {
            return (int)db.func_DemKhoaHocDk(id);
        }
        public static List<Video> loadVideosTheoKh(int id)
        {
            return new List<Video>(db.sp_XemDsVdTheoKh(id).ToList());
        }
        public static TaiKhoan getTaiKhoan(int id)
        {
            var a = db.sp_XemTTTK(id).ToList();
            TaiKhoan b = new TaiKhoan();
            foreach (var t in a)
            {
                b = t;
            }
            return b;
        }
        public static int demSumVideos(int s)
        {
            return (int)db.func_DemVdTrongKh(s);
        }
        public static bool dangNhap(string tk, string mk)
        {
            int a = (int)db.func_DangNhap(tk, mk);
            if(a == 0)
                return false;
            else
            {
                if (tk.Equals("admin")) isAdmin = true;
                else isAdmin = false;
                taiKhoan = getTaiKhoan(a);
                return true;
            }
        }
        public static bool kTrDangNhap(string tk, string mk)
        {
            int a = (int)db.func_DangNhap(tk, mk);
            if (a == 0)
                return false;
            else
                return true;
        }
        public static void doiMk(int id, string tk, string mk)
        {
            db.sp_DoiMk(id, tk, mk);
        }
        public static bool kTraDangKy(int idKh, int idTk)
        {
            if ((int)db.func_KiemTraDKKH(idKh, idTk) == 1)
                return false;
            else return true;
        }
        public static bool huyDangKy(int idKh, int idTk)
        {
            if ((int)db.pc_HuyDKKH(idKh, idTk) == 0)
                return false;
            else return true;
        }
        public static bool dkKhoaHoc(int idKh, int idTk)
        {
            if (kTraDangKy(idKh, idTk))
            {
                var a = db.sp_Themkh(idKh, idTk).ToList();
                return true;
            }
            else return false;
        }
        public static int demTongLuotXem(int idTk)
        {
            return (int)db.func_DemTongLuotXem(idTk);
        }
        public static List<Video> dsVdDaCoi(int id)
        {
            return new List<Video>(from vd in db.Videos
                                     join ls in db.LichSuCois on vd.idVd equals ls.idVd
                                     where ls.idTk == id
                                     select vd);
        }
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }
        public static TextBlock getTxb(string s)
        {
            TextBlock a = new TextBlock();
            a.Text = s;
            a.Margin = new Thickness(10);
            a.HorizontalAlignment = HorizontalAlignment.Center;
            a.VerticalAlignment = VerticalAlignment.Center;
            a.FontSize = 30;
            return a;
        }
        public static string layTenKhCuaVd(Video vd)
        {
            int? id = vd.idKh;
            if(id is null) return "Video này chưa có khoá học";
            string a = db.func_LayTenKhCuaVd(id);
            Console.WriteLine(a);
            return a;
        }
        public static List<NDBinhLuan> layNDBinhLuan(int id)
        {
            var a = db.sp_LayBinhLuan(id).ToList();
            List<NDBinhLuan> list = new List<NDBinhLuan>();
            foreach(var t in a)
            {
                list.Add(new NDBinhLuan(t.id, t.noiDung, t.ngay));
            }
            return list;
        }
        public static void binhLuanVoVd(int idVd,  string nd)
        {
            db.sp_BinhLuanVoVd(nd, idVd, taiKhoan.idTk);
        }
        public static void xemVd(int idVd)
        {
            if (db.func_KiemTraDaCoiVdChua(idVd, taiKhoan.idTk)==0)
                db.sp_XemVd(idVd, taiKhoan.idTk);
            db.SubmitChanges();
        }
        public static bool themVd(string ten, string urlT, string urlVd, int idKh)
        {
            int? id = idKh;
            if (idKh == 0)
            {
                db.sp_TaoVdNoIdKh(ten, urlT, urlVd);
                return true;
            }
            if (db.func_KtrVdTonTaiChua(ten, urlT, urlVd, id) == 0)
            {
                db.sp_TaoVd(ten, urlT, urlVd, idKh);
                return true;
            }
            return false;
        }
        public static bool suaVd(int id, string ten, string urlT, string urlVd, int idKh)
        {
            if (db.func_KtrVdTonTaiChua(ten, urlT, urlVd, idKh) == 0)
            {
                db.sp_SuaVd(id, ten, urlT, urlVd, idKh);
                return true;
            }
            return false;
        }
        public static void xoaVideo(int id)
        {
            db.sp_XoaVideo(id);
        }
        public static bool themKh(string ten)
        {
            if (db.func_KtraKhTonTaiChua(ten) == 0)
            {
                db.sp_TaoKhoaHoc(ten);
                return true;
            }
            return false;
        }
        public static bool suaKh(int id, string ten)
        {
            if (db.func_KtraKhTonTaiChua(ten) == 0)
            {
                db.sp_SuaKh(id, ten);
                return true;
            }
            return false;
        }
        public static void xoaKhoaHoc(int id)
        {
            db.sp_XoaKhoaHoc(id);
        }
        public static void xoaVideoKhoiKh(int id)
        {
            db.sp_XoaVideoKhoiKh(id);
        }
        public static bool ktrKhTonTai(int id)
        {
            if (db.func_KtraKhTonTaiId(id)==0)
                return true;
            return false;
        }
        public static ThongKe LayTk(DateTime ngayBd, DateTime ngayKt)
        {
            var a = db.fuc_ThongKe(ngayBd, ngayKt).ToList();
            ThongKe b =  new ThongKe();
            b.tongView = (int)a.ElementAt(0).tongView;
            b.tongBl = (int)a.ElementAt(0).tongBl;
            b.tenVdXemNn = a.ElementAt(0).tenVdXemNn;
            b.tenVdBlNn = a.ElementAt(0).tenVdBlNn;
            b.tenKhXemNn = a.ElementAt(0).tenKhXemNn;
            b.tenTkXemNn = a.ElementAt(0).tenTkXemNn;
            b.tenTkBlNn = a.ElementAt(0).tenTkBlNn;
            return b;
        }
        public static bool createUser(string name, string matKhau)
        {
            if (db.func_CheckUser(name) == 0)
            {
                db.sp_TaoUser(name, matKhau);
                return true;
            }
            else return false;
        }

    }
}
