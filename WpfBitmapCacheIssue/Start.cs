using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfBitmapCacheIssue
{
    internal class WpfProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Windows...");
            FirstWindow firstWindow = new FirstWindow();
            SecondWindow secondWindow = new SecondWindow();
            ThirdWindow thirdWindow = new ThirdWindow();

            Console.WriteLine("Positioning Windows...");
            double totalWidth = firstWindow.Width + secondWindow.Width + thirdWindow.Width;
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double leftMargin = (screenWidth - totalWidth) / 2;
            firstWindow.Left = leftMargin;
            secondWindow.Left = leftMargin + firstWindow.Width;
            thirdWindow.Left = leftMargin + firstWindow.Width + secondWindow.Width;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double topMargin = (screenHeight - Math.Max(firstWindow.Height, Math.Max(secondWindow.Height, thirdWindow.Height))) / 2;
            firstWindow.Top = secondWindow.Top = thirdWindow.Top = topMargin;

            Console.WriteLine("Showing Windows...");
            firstWindow.Show();
            secondWindow.Show();
            thirdWindow.Show();

            Console.WriteLine("Creating BitmapCache...");
            BitmapCache bitmapCache = new BitmapCache() { RenderAtScale = 1.0, EnableClearType = true, SnapsToDevicePixels = true };
            secondWindow.CacheMode = bitmapCache;

            // Fix 1 : Cache First Created Window Will Fix BitmapCache Bug
            // This is a Bad Fix
            // It seems First Window Created only receive correct messages after Windows lock
            firstWindow.CacheMode = bitmapCache;

            Console.WriteLine("Entering Message Loop...");
            Dispatcher.Run();
        }
    }
}
