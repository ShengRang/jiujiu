using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiujiuXiaohantu
{
    class DateHelper
    {
        //chinesecalendar可以获取农历信息，回头完善。
        //[Y*D+C]-L

        //公式解读：Y=年数后2位，D=0.2422，L=闰年数，21世纪C=21.94，20世纪=22.60。

        //举例说明：2088年冬至日期=[88×0.2422+21.94]-[88/4]=43-22=21，12月21日冬至。

        //例外：1918年和2021年的计算结果减1日。
        public static int CalcDzDate(int yy)
        {//传入int类型的年份，返回这年的冬至日是哪天（21,22,23之类的。）
            int Y = yy % 100;
            double D = 0.2422;
            double C = 21.94;
            int L = Y / 4;
            if (yy != 2021)
            {
                return (int)(Y * D + C) - L;
            }
            else
            {
                return (int)(Y * D + C) - L - 1;
            }

        }

        public static int JudgeYear(DateTime dt)
        {
            //根据传入的DateTime算出这个冬天的冬至日应该是这一年还是上一年。
            //最后决定，除非是要开始第二个冬至日了，不然一直保留上一次冬天
            int yy = dt.Year;
            DateTime refDt = new DateTime(yy, 12, CalcDzDate(yy));
            //refDt是这年的冬至日日期
            TimeSpan tp = dt.Subtract(refDt);
            //如果间隔大于等于0了，那么就，开始新的冬天啦。
            //否则还是去年冬天的状态。
            if (tp.TotalDays >= 0)
            {
                return yy;
            }
            else
            {
                return yy - 1;
            }

        }

        public static DateTime GetRightDz(DateTime dt)
        {
            int yy = JudgeYear(dt);
            int dd = CalcDzDate(yy);
            DateTime dz = new DateTime(yy, 12, dd);
            // MessageBox.Show("正确冬至:" + dz.ToString());
            return dz;
        }

        public static bool IsInWinter(DateTime dt)
        {//判断是否在冬日
            DateTime dz = GetRightDz(dt);
            TimeSpan tp = dt.Subtract(dz);
            if (tp.TotalDays < 81)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsInThisWinter(DateTime dt)
        {//判断是否在这次的（如果在冬至前，就是去年的，否则是今年的）冬日
            DateTime dz = GetRightDz(DateTime.Today);
            TimeSpan tp = dt.Subtract(dz);
            if (tp.TotalDays < 81 && tp.TotalDays >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetDaysInWinter(DateTime dt)
        {
            DateTime dz = GetRightDz(dt);
            TimeSpan tp = dt.Subtract(dz);
            return (int)tp.TotalDays;
        }

    }
}
