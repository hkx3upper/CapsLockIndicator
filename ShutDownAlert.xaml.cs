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
using System.Diagnostics;
using System.Windows.Threading;

namespace CapsLockIndicator
{
    /// <summary>
    /// ShutDownAlert.xaml 的交互逻辑
    /// </summary>
    public partial class ShutDownAlert : Window
    {

        DispatcherTimer _timer;
        TimeSpan _time;

        public ShutDownAlert()
        {
            InitializeComponent();
            this.Left = 52;
            this.Top = 672;

            _time = TimeSpan.FromSeconds(60);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "-a");
            this.Close();
        }
    }
}
