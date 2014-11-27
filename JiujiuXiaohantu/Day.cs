using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiujiuXiaohantu
{

    [Table(Name = "Day")]
    public class Day
    {
        public Day()
        {
            this.IsSigned = false;
            this.Status = null;
            this.DayId = Guid.NewGuid();
            this.Date = DateTime.Today;
            this.DateStr = DateTime.Today.ToString("yyyy-MM-dd");
        }
        public Day(DateTime day)
        {
            this.IsSigned = false;
            this.Status = null;
            this.DayId = Guid.NewGuid();
            this.Date = day;
            this.DateStr = day.ToString("yyyy-MM-dd");
        }
        //让我想想应该怎么写这个表，首先要有日期，然后是，这天的记录，然后是，这天是冬天的第几天，然后，这天是否被
        private DateTime date;  //日期我想了想还是只能存储冬天之内的信息，不然数据库冗余，APP定位也不准确。
        [Column(Name = "Date")]
        public DateTime Date
        {
            get { return this.date; }
            // set { if (DateHelper.IsInWinter(value) == true) this.date = value; else throw new Exception("我不想存冬天之外的信息"); }
            set { this.date = value; }
        }

        [Column(Name = "DateStr")]
        public string DateStr { get; set; }

        [Column(IsPrimaryKey = true)]
        public Guid DayId { get; set; }

        [Column(Name = "IsSigned")]
        public bool IsSigned { get; set; }

        private string status;
        [Column(Name = "Status")]
        public string Status
        {
            get { return this.status; }
            set
            {
                if (value == null) this.status = "今天没有记录";
                else
                {
                    this.status = value;
                }
            }
        }
    }
}
