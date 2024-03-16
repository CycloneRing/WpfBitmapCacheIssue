using Microsoft.Windows.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBitmapCacheIssue
{
    /// <summary>
    /// Interaction logic for FirstWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
        }

        private void ActionClick(object sender, RoutedEventArgs e)
        {
            // Note : HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Avalon.Graphics\EnableDebugControl must be set to 1 to make this method work

            try
            {
                Console.WriteLine("Creating MediaControl Gate...");
                if (MediaControl.CanControl)
                {
                    MediaControl mediaCtrl = MediaControl.Attach(Process.GetCurrentProcess().Id, ClrVersion.V4);
                    if (mediaCtrl == null) throw new Exception("Failed To Attach.");
                    Console.WriteLine("MediaControl Connected.");

                    Console.WriteLine("Configuring Render Engine...");
                    mediaCtrl.DisableDirtyRegionSupport = true;
                    Console.WriteLine("Render Engine Config Changed.");
                }
                else
                {
                    throw new Exception("Cannot Create MediaControl Gate.");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Fatal Error :\n{error.Message}");
                Console.WriteLine($"Call Stack :\n{error.StackTrace}");
            }
            
        }
    }
}
