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

        private void EnableDirtyRect(object sender, RoutedEventArgs e)
        {
            if (WpfProgram.mediaCtrl != null)
            {
                WpfProgram.mediaCtrl.DisableDirtyRegionSupport = false;
                Console.WriteLine(" * DisableDirtyRegionSupport Set to False");
            }
        }
        private void DisableDirtyRect(object sender, RoutedEventArgs e)
        {
            if (WpfProgram.mediaCtrl != null)
            {
                WpfProgram.mediaCtrl.DisableDirtyRegionSupport = true;
                Console.WriteLine(" * DisableDirtyRegionSupport Set to True");
            }
        }
    }
}
