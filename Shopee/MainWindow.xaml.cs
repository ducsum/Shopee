using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using KAutoHelper;

namespace Shopee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region data
        Bitmap TOP_UP_BMP;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            TOP_UP_BMP = (Bitmap)Bitmap.FromFile("Data//TopUp.png");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() => {
                isStop = false;
                Auto();
            });
            t.Start();
        }



        bool isStop = false;
        void Auto()
        {
            // lấy ra danh sách devices id để dùng
            List<string> devices = new List<string>();
            devices = KAutoHelper.ADBHelper.GetDevices();

            // chạy từng device để điều khiển theo kịch bản bên trong
            foreach (var deviceID in devices)
            {
                // tạo ra một luồng xử lý riêng biệt để xử lý cho device này
                Task t = new Task(() => {
                    // lặp kịch bản quài quài
                    while (true)
                    {
                        plan(deviceID);
                    }
                });
                t.Start();
            }
        }

        void Delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                delay--;
                if (isStop)
                    break;
            }
        }

        void plan(string deviceID)
        {
            while (true)
            {
                // nếu có lệnh stop thì dừng toàn bộ luồng chạy
                if (isStop)
                    return;
                // click vào webbrowser
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 8.2, 88.7);
                Delay(3);

                if (isStop)
                    return;
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 86.1, 70);
                Delay(3);

            }

        }

        void vaofarm(string deviceID)
        {

            clear_data(deviceID);

            KAutoHelper.ADBHelper.Key(deviceID, KAutoHelper.ADBKeyEvent.KEYCODE_MENU);

            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.2, 97.3);
            Delay(10);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 30.4, 75.1);
            Delay(30);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 71.1, 26.4);
            Delay(30);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 10.9, 55.3);
            Delay(50);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.6, 87.1);
            Delay(30);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 12.7, 22.5);
            Delay(30);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 75.6, 14.2);
            Delay(30);

        }
        void clear_data(string deviceID)
        {
            isStop = true;
            Delay(20);
            isStop = false;

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.8, 97.3);
            Delay(10);

            KAutoHelper.ADBHelper.SwipeByPercent(deviceID, 40, 42, 1, 42, 200);
            Delay(10);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.7, 87.7);
            Delay(10);

        }
        void sogiday(string deviceID)
        {


        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Clear data.
        {
            clear_data("KR9HJBGM65Q4ROH6");
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //vaofarm.
        {
            vaofarm("KR9HJBGM65Q4ROH6");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //vaofarm.
        {
            clear_data("KR9HJBGM65Q4ROH6");
            sogiday("KR9HJBGM65Q4ROH6");
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)   //Stop
        {
            isStop = true;
        }
    }
}
