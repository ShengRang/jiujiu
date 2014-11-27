using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JiujiuXiaohantu
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        public static int type;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (type == 1)
            {
                tbTitle.Text = "关于";
                tbDetail.Text = "    我并不会WP的应用开发,这是我第一次做手机开发，其实我只会一点点C#，XAML，数据库。Linq都是现学的\n    想做这个APP源于某人..，整个应用很多地方非常别扭，代码非常丑陋，加上时间很少，有些需求太奇葩，满满的都是问题\n    不好之处请多包容，日后有时间会回来更新.\n  \t\t           --shengrang";
            }
            else
            {
                tbTitle.Text = "说明";
                tbDetail.Text = "    从冬至那天起就算进“九”了，在冬至民间有贴绘“九九消寒图”的习俗，消寒图是记载进九以后天气阴晴的“日历”，人们寄望于它，来预卜来年丰欠，是一种很有传统特色的、好看的日历。它一共有九九八十一个单位，所以才叫做“九九消寒图”。从冬至那天算起，以九天作一单元，连数九个九天，到九九共八十一天，冬天就过去了。\n\n    九九消寒图是中国北方的一项民俗，与数九的民俗密切相关。九九消寒图是一幅双钩描红书法“亭前垂柳珍重待春风”，均为繁体字，九字每字九划共九九八十一划，从冬至开始每天按照笔画顺序填充一个笔画，每过一九填充好一个字，直到九九之后春回大地，一幅九九消寒图才算大功告成。也称作“写九”。一般而言在九九消寒图的一侧还应写有《数九歌》。\n\n    灵感来自某同学，在那81天里，可以在第一页的文本里写入当天心情然后点下方按钮即可记录，记录之后对应那一天的那一个笔画就会被描红。\n\n    以此纪念这个传统,纪念某个冬天";
            }

        }
        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {

            e.Handled = true;

            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            NavigationService.GoBack();

            // Navigate to a page

        }
    }
}