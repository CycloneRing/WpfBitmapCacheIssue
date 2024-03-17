using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfBitmapCacheIssue
{
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
            if (!FindWpfGfxBaseAddress())
            {
                Console.WriteLine($"Error : FindDirtyRegionAddress Failed.");
            }
        }

        private void EnableDirtyRect(object sender, RoutedEventArgs e)
        {
            if (PatchDirtyRegionEnabled(true))
                Console.WriteLine($" >> Dirty Rect Rendering Enabled.");
            else
                Console.WriteLine($"Error : EnableDirtyRect::PatchDirtyRegionEnabled Failed.");

        }
        private void DisableDirtyRect(object sender, RoutedEventArgs e)
        {
            if (PatchDirtyRegionEnabled(false))
                Console.WriteLine($" >> Dirty Rect Rendering Disabled.");
            else
                Console.WriteLine($"Error : DisableDirtyRect::PatchDirtyRegionEnabled Failed.");
        }

        // Patcher
        internal static IntPtr WpfGfxBaseAdress = IntPtr.Zero;
        internal const int DirtyRegion_Enabled_Offset = 0x170190; // Extracted by OffsetExtractor
        internal bool FindWpfGfxBaseAddress()
        {
            try
            {
                Process process = Process.GetCurrentProcess();

                foreach (ProcessModule module in process.Modules)
                {
                    if (module.ModuleName.ToLower() == "wpfgfx_v0400.dll")
                    {
                        WpfGfxBaseAdress = module.BaseAddress;
                        Console.WriteLine("Base address of wpfgfx_v0400.dll: " + WpfGfxBaseAdress.ToString("X"));
                        return true;
                    }
                }

                return false;
            }
            catch (Exception error)
            {
                Console.WriteLine($"FindDirtyRegionAddress Error:\n{error.Message}");
                return false;
            }
        }
        internal bool PatchDirtyRegionEnabled(bool Enabled)
        {
            try
            {
                if (WpfGfxBaseAdress == IntPtr.Zero) return false;
                Marshal.WriteInt32(WpfGfxBaseAdress, DirtyRegion_Enabled_Offset, Enabled ? 1 : 0);
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine($"FindDirtyRegionAddress Error:\n{error.Message}");
                return false;
            }
        }
    }
}
