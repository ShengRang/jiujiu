using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiujiuXiaohantu
{
    public class DayDAL
    {
        const string connStr = "Data Source=isostore:/MyContext.sdf";
        public static int InsertDay(Day day)    //增
        {
            //1表示插入成功，0表示失败。
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {
                    //Guid guid = Guid.NewGuid();
                    //Day day = new Day { Date = DateTime.Today, DayId = guid, IsSigned = false, DateStr = DateTime.Today.ToString("yyyy-MM-dd") };
                    db.DayTable.InsertOnSubmit(day);
                    //MessageBox.Show("Date:" + day.Date.ToString() + ",DayId:" + day.DayId.ToString() + ",DateStr:" + day.DateStr + ",IsSigned:" + day.IsSigned.ToString() + "");
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;

        }
        public static int DeleteById(Guid id)   //      删，这里不用软删除了，因为没必要
        {
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {

                    var list = (from day in db.DayTable where day.DayId == id select day).ToList();
                    if (list.Count > 0)
                    {
                        db.DayTable.DeleteOnSubmit(list[0]);
                    }
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public static int UpdateStatusById(Guid id, string status)   //根据id和status，更改那天心情，这个功能有待考虑是否加入
        {
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {
                    Day day0 = GetById(id);
                    day0.Status = status;
                    db.SubmitChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public static Day GetById(Guid id)      //根据Id查询到day
        {
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {

                    var list = (from day in db.DayTable where day.DayId == id select day).ToList();
                    if (list.Count == 1)
                    {
                        return list[0];
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public static List<Day> ListAll()   //举出全部，不过描线还是用列举冬天的比较好
        {
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {

                    var list = (from day in db.DayTable select day).ToList();
                    return list;
                }
            }
            catch
            {
                throw new Exception("试图列举全部，发生异常");
            }
            return null;
        }

        //这里再写一个，list出这个冬天的81个Signed状态，用list？还是数组？
        public static List<Day> GetThisYear()
        {//这个查询异步我还真不会写，LINQ太连续了，回头看看效果再说吧，，想死的心都有了
            //用多线程做。
            try
            {
                MyContext db = new MyContext(connStr);
                if (db.DatabaseExists() == true)
                {
                    //首先举出这年冬天的冬至日。
                    DateHelper.GetRightDz(DateTime.Today);
                    //list是返回的集合，eachN代表每个九
                    //接着做查询的操作。
                    var list = (from day in db.DayTable select day).ToList();
                    List<Day> result = new List<Day>();
                    foreach (Day x in list)
                    {
                        if (DateHelper.IsInThisWinter(x.Date) == true)
                        {
                            result.Add(x);
                        }
                    }
                    return result;

                }
            }
            catch
            {
                throw new Exception("试图列举本年Signed状态，发生异常");
            }
            return null;
        }

    }
}
