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
using System.Runtime.InteropServices; 
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;


namespace CapsLockIndicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        public static int GetCapsLockState()
        {
            return (GetKeyState(0x14) & 0x0001);
        }

        //实现键盘监听的循环函数
        private void Routine()
        {
            int Ret, LastRet = GetCapsLockState();

            while (true)
            {
                Ret = GetCapsLockState();

                if(Ret==LastRet)
                {
                    Thread.Sleep(200);
                    continue;
                }

                if (Ret != 0)
                {
                    //线程访问主线程中的控件
                    this.Dispatcher.Invoke(new Action(() => {
                        TextFlag.Text = "ABC";
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        TextFlag.Text = "abc";
                    })); 
                }

                LastRet = Ret;
                Thread.Sleep(200);
            }
        }
      

        public MainWindow()
        {
            InitializeComponent();

            this.Left = 1;
            this.Top = SystemParameters.WorkArea.Height - 56;

            //不在任务栏中显示图标
            this.ShowInTaskbar = false;

            //设置窗口位于顶层
            this.Topmost = true;

            var th = new Thread(Routine);
            th.Start();
        }

        //关闭本程序
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }

        //隐身
        private void Hiding_Click(object sender, RoutedEventArgs e)
        {
            if (!Hiding_Menu.IsChecked)
            {
                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8F1E0"));
                Hiding_Menu.Header = "隐身";
                return;
            }

            this.Background = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0));
            Hiding_Menu.Header = "取消隐身";
        }

        //左键单击返回桌面，摸鱼神器
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Type oleType = Type.GetTypeFromProgID("Shell.Application");
            object oleObject = System.Activator.CreateInstance(oleType);
            oleType.InvokeMember("ToggleDesktop", BindingFlags.InvokeMethod, null, oleObject, null);
        }

        //锁定
        [DllImport("User32.DLL")]
        public static extern void LockWorkStation();

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            LockWorkStation();
        }

        //关机
        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            //var result = MessageBox.Show("Shutdown in 15 sencond.", "Warning",MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            //if(result == MessageBoxResult.OK)
            //{
                Process.Start("shutdown", "-s -t 60");
            //}

            ShutDownAlert shutDownAlert = new ShutDownAlert();
            shutDownAlert.Show();
        }

        //关闭代理服务器
        private void ProxyDisable_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey InternetSettings = hkcu.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            InternetSettings.SetValue("ProxyEnable", 0);

            InternetSettings.Close();
            hkcu.Close();
        }
    }
}
