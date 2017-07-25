using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace Viewer
{
    class Program
    {
        [DllImport("user32.dll")]//获取窗口句柄
        public static extern IntPtr FindWindow(
         string lpClassName,
         string lpWindowName
         );

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
               IntPtr hwnd
               );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
               IntPtr hdc, // handle to DC
               int nWidth, // width of bitmap, in pixels
               int nHeight // height of bitmap, in pixels
               );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
                IntPtr hdc // handle to DC
                );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
               IntPtr hdc, // handle to DC
               IntPtr hgdiobj // handle to object
               );

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
               IntPtr hwnd, // Window to copy,Handle to the window that will be copied. 
               IntPtr hdcBlt, // HDC to print into,Handle to the device context. 
               UInt32 nFlags // Optional flags,Specifies the drawing options. It can be one of the following values. 
               );

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
               IntPtr hdc // handle to DC
               );

        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(
               IntPtr hdc
               );

        static void Main(string[] args)
        {
            IntPtr hWnd = IntPtr.Zero;
            string savePath = "";

            hWnd = FindWindow(null, "沙漠枭雄");//得到名称为“记事本”的窗口句柄。

            savePath = "D:\\tmp\\temp.bmp";//设置图片的临时保存路径。

            Bitmap img = GetImg(hWnd, 1000, 1000);//X,Y为所要获取截图的窗口宽度和高度。

            img.Save(savePath);//保存得到的截图。
        }

        public static Bitmap GetImg(IntPtr hWnd, int Width, int Height)//得到窗口截图
        {
            IntPtr hscrdc = GetWindowDC(hWnd);
            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, Width, Height);
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);//删除用过的对象
            DeleteObject(hbitmap);//删除用过的对象
            DeleteDC(hmemdc);//删除用过的对象
            return bmp;
        }
        
    }
}
