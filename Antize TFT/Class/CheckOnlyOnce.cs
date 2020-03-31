using System;
using System.Threading;
using System.Windows;

namespace Antize_TFT.Class
{
    class CheckOnlyOnce
    {
        private const string appName = "AntizeTFT";
        private static readonly Mutex mutex;

        static CheckOnlyOnce()
        {
            mutex = new Mutex(false, appName);
        }

        [STAThread]
        public static bool MakeCheckOnlyOnce()
        {
            if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                MessageBox.Show("Another instance is already running.", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();

                return true;
            }
            else
                return false;
        }
    }
}