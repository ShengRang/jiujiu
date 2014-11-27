using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JiujiuXiaohantu
{
    public partial class MainPage : PhoneApplicationPage
    {
        const string connStr = "Data Source=isostore:/MyContext.sdf";
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 将 listbox 控件的数据上下文设置为示例数据
            DataContext = App.ViewModel;

            using (MyContext db = new MyContext(connStr))
            {

                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                    db.SubmitChanges();
                    //MessageBox.Show("新建数据库成功");
                }
            }

            MyContext DayDB = new MyContext(connStr);

        }


        // 为 ViewModel 项加载数据
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            DateTime dt = DateTime.Today;
            tbDate.Text = "今天是" + dt.ToString("yyyy-MM-dd");
            //第二个TextBlock应该这样：如果正在冬天，显示正在冬天，这是冬天的第XXX天，如果过了冬天，显示，冬天已经过去，下次冬至：yyyy-MM-dd

            //tbDz.Text = "这次冬至：" + dz.ToString("yyyy-MM-dd");
            //出现界面有点卡顿，所以采取基于任务的异步编程
            var someTask = Task<DateTime>.Factory.StartNew(() => DateHelper.GetRightDz(dt));
            await someTask;
            DateTime dz = someTask.Result;

            var tskCalcDays = Task<int>.Factory.StartNew(() => DateHelper.GetDaysInWinter(dt));
            await tskCalcDays;
            if (DateHelper.IsInWinter(dt))
            {//正值冬天
                tbDz.Text = "正是冬天第" + tskCalcDays.Result.ToString() + "天";
            }
            else
            {//冬天之外，之后应该完善一下，如果接近冬天了，就说还差。。就冬天了。
                tbDz.Text = "冬天已经过去，期待下个冬天吧";
            }
            //UpdateState();

        }
        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string connStr = "Data Source=isostore:/MyContext.sdf";
            using(MyContext db = new MyContext (connStr))
            {
                
                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                    db.SubmitChanges();
                    MessageBox.Show("新建数据库成功");
                }
            }
        }
         //测试模块代码
         */
        /*
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            //string connStr = "Data Source=isostore:/MyContext.sdf";
            using (MyContext db = new MyContext(connStr))
            {
                MessageBox.Show(db.DatabaseExists().ToString());
                if (db.DatabaseExists() == true)
                {
                    db.DeleteDatabase();
                   // db.SubmitChanges();
                    MessageBox.Show("删除数据库成功");
                }
            }
        }
         //测试模块代码
         * */
        /*
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    Guid guid = Guid.NewGuid();
                    Random ro = new Random();
                    DateTime dt = new DateTime(2014,ro.Next(1,13), ro.Next(1, 29));
                    Day day = new Day(dt);
                    day.Status = "今天是:" + dt.ToString("yyyy-MM-dd");
                    day.IsSigned = (ro.Next(0,2)==0?false:true);
                    db.DayTable.InsertOnSubmit(day);
                    db.SubmitChanges();
                }
            }
        }
         //测试模块代码
         */
        /*
        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    //Thread thread = new Thread(new ThreadStart(doSomethins));
                    //thread.IsBackground = true;
                    //thread.Start();
                   //var list = (from day in db.DayTable select day).ToList();
                    List<Day> list = DayDAL.GetThisYear();
                    //回头断点一下，看看这个list是什么类型的。如果没有转化的haunted
                   lbDay.ItemsSource = list;
                }
                else
                {
                    lbDay.ItemsSource = null;
                }
                
            }
        }
         // 测试模块代码
        */
        //private void btnDraw_Click(object sender, RoutedEventArgs e)
        //{
        //    ////把亭的第一笔涂红
        //测试模块代码

        //    //WriteableBitmap wb = new WriteableBitmap(imgTest, null);
        //    //int[] ImageData = wb.Pixels;
        //    //WriteableBitmap wb_result = new WriteableBitmap(wb.PixelWidth, wb.PixelHeight);
        //    //for (int i = 0; i < wb.PixelHeight; i++)
        //    //{
        //    //    for (int j = 0; j < wb.PixelWidth; j++)
        //    //    {
        //    //        int curColor = ImageData[i * wb.PixelWidth + j];
        //    //        byte RedValue = (byte)(curColor >> 16 & 0xFF);  //0xFF实际上没有意义!!!
        //    //        byte GreenValue = (byte)(curColor >> 8 & 0xFF);
        //    //        byte BlueValue = (byte)(curColor & 0xFF);
        //    //        byte AlphaValue = (byte)(curColor >>24 & 0xFF);
        //    //        //获取RGB

        //    //        //生成byte的数组表示一个像素点，
        //    //        byte[] RedValueArr = new byte[4];
        //    //        RedValueArr[3] = AlphaValue;  //0x00
        //    //        RedValueArr[2] = 255;
        //    //        RedValueArr[1] = 0;
        //    //        RedValueArr[0] = 0;

        //    //        //把这个byte数组转化成那个像素点。
        //    //        int RedPixel = BitConverter.ToInt32(RedValueArr, 0); 
        //    //        unchecked
        //    //        {
        //    //            ////wb_gray.Pixels[i * wb.PixelWidth + j] = (int)0xFFFF0000;  //成功!!!
        //    //            wb_result.Pixels[i * wb_result.PixelWidth + j] = RedPixel;   //OK->成功啦!!!
        //    //        }
        //    //    }
        //    //}

        //    //wb_result.Invalidate();

        //    //亭1.Source = wb_result;
        //    ////亭1.Source = wb_result;
        //    //imgTest.Source = wb_result;
        //    ////imgTest2.Source = wb_result;




        //    ////经过测试，这个方法可以把一个笔画涂红。
        //    ////可以做一个封装，传入Image.Source,传出writeableBitmap

        //    WriteableBitmap wb = ImgHelper.GetImgPainted(imgTest);
        //    imgTest.Source = wb;

        //}

        private void btnInsert3k_Click(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    for (int i = 0; i < 3000; i++)
                    {
                        Guid guid = Guid.NewGuid();
                        Day day = new Day();
                        db.DayTable.InsertOnSubmit(day);
                        //MessageBox.Show("Date:" + day.Date.ToString() + ",DayId:" + day.DayId.ToString() + ",DateStr:" + day.DateStr + ",IsSigned:" + day.IsSigned.ToString() + "");
                    }
                    db.SubmitChanges();
                    MessageBox.Show("成功插入3000条数据~");
                }
            }
        }

        private void btnDelAll_Click(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    var list = (from day in db.DayTable select day).ToList();
                    foreach (Day day in list)
                    {
                        db.DayTable.DeleteOnSubmit(day);
                    }
                    //MessageBox.Show("Date:" + day.Date.ToString() + ",DayId:" + day.DayId.ToString() + ",DateStr:" + day.DateStr + ",IsSigned:" + day.IsSigned.ToString() + "");
                    db.SubmitChanges();
                }
            }
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            // List<bool[]> list = DayDAL.GetThisYear();


        }
        private void UpdateState()
        {
            List<Day> list = DayDAL.GetThisYear();
            foreach (Day day in list)
            {
                if (day.IsSigned == true)
                {
                    int x;
                    x = DateHelper.GetDaysInWinter(day.Date);

                    WriteableBitmap wb = ImgHelper.GetImgPainted(GetImage(x));
                    GetImage(x).Source = wb;
                }
            }
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    //Thread thread = new Thread(new ThreadStart(doSomethins));
                    //thread.IsBackground = true;
                    //thread.Start();
                    //var list = (from day in db.DayTable select day).ToList();
                    list = DayDAL.GetThisYear();
                    //回头断点一下，看看这个list是什么类型的。如果没有转化的haunted
                    lbDay.ItemsSource = list;
                }
                else
                {
                    lbDay.ItemsSource = null;
                }

            }
        }

        public Image GetImage(int x)
        {
            int y = x / 9;
            x %= 9;
            if (y == 0)
            {
                switch (x)
                {
                    case 0: return ting0;
                    case 1: return ting1;
                    case 2: return ting2;
                    case 3: return ting3;
                    case 4: return ting4;
                    case 5: return ting5;
                    case 6: return ting6;
                    case 7: return ting7;
                    case 8: return ting8;
                }
            }
            else if (y == 1)
            {
                switch (x)
                {
                    case 0: return qian0;
                    case 1: return qian1;
                    case 2: return qian2;
                    case 3: return qian3;
                    case 4: return qian4;
                    case 5: return qian5;
                    case 6: return qian6;
                    case 7: return qian7;
                    case 8: return qian8;
                }
            }
            else if (y == 2)
            {
                switch (x)
                {
                    case 0: return chui0;
                    case 1: return chui1;
                    case 2: return chui2;
                    case 3: return chui3;
                    case 4: return chui4;
                    case 5: return chui5;
                    case 6: return chui6;
                    case 7: return chui7;
                    case 8: return chui8;
                }
            }
            else if (y == 3)
            {
                switch (x)
                {
                    case 0: return liu0;
                    case 1: return liu1;
                    case 2: return liu2;
                    case 3: return liu3;
                    case 4: return liu4;
                    case 5: return liu5;
                    case 6: return liu6;
                    case 7: return liu7;
                    case 8: return liu8;
                }
            }
            else if (y == 4)
            {
                switch (x)
                {
                    case 0: return zhen0;
                    case 1: return zhen1;
                    case 2: return zhen2;
                    case 3: return zhen3;
                    case 4: return zhen4;
                    case 5: return zhen5;
                    case 6: return zhen6;
                    case 7: return zhen7;
                    case 8: return zhen8;
                }
            }
            else if (y == 5)
            {
                switch (x)
                {
                    case 0: return zhong0;
                    case 1: return zhong1;
                    case 2: return zhong2;
                    case 3: return zhong3;
                    case 4: return zhong4;
                    case 5: return zhong5;
                    case 6: return zhong6;
                    case 7: return zhong7;
                    case 8: return zhong8;
                }
            }
            else if (y == 6)
            {
                switch (x)
                {
                    case 0: return dai0;
                    case 1: return dai1;
                    case 2: return dai2;
                    case 3: return dai3;
                    case 4: return dai4;
                    case 5: return dai5;
                    case 6: return dai6;
                    case 7: return dai7;
                    case 8: return dai8;
                }
            }
            else if (y == 7)
            {
                switch (x)
                {
                    case 0: return chun0;
                    case 1: return chun1;
                    case 2: return chun2;
                    case 3: return chun3;
                    case 4: return chun4;
                    case 5: return chun5;
                    case 6: return chun6;
                    case 7: return chun7;
                    case 8: return chun8;
                }
            }
            else if (y == 8)
            {
                switch (x)
                {
                    case 0: return feng0;
                    case 1: return feng1;
                    case 2: return feng2;
                    case 3: return feng3;
                    case 4: return feng4;
                    case 5: return feng5;
                    case 6: return feng6;
                    case 7: return feng7;
                    case 8: return feng8;
                }
            }
            else
            {
                throw new Exception("这个数字超出了81，，必然有误，请检查");
            }

            return qian8;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        private void btnInsert1_Click(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    Guid guid = Guid.NewGuid();
                    Random ro = new Random();
                    DateTime dt = new DateTime(2014, 1, 13);
                    Day day = new Day(dt);
                    day.Status = "测试的`";
                    day.IsSigned = true;
                    db.DayTable.InsertOnSubmit(day);
                    db.SubmitChanges();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (MyContext db = new MyContext(connStr))
            {
                if (db.DatabaseExists() == true)
                {
                    Guid guid = Guid.NewGuid();
                    Day day = new Day();
                    day.Status = tbStatus.Text;
                    day.IsSigned = true;
                    db.DayTable.InsertOnSubmit(day);
                    db.SubmitChanges();
                }
            }
            UpdateState();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(700);
            UpdateState();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AboutPage.type = 1;
            NavigationService.Navigate(new Uri("/AboutPage.xaml?type=1", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AboutPage.type = 2;
            NavigationService.Navigate(new Uri("/AboutPage.xaml?type=2", UriKind.Relative));
        }
    }
}