using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiujiuXiaohantu
{

    [Database(Name = "MyContext")]
    class MyContext : DataContext
    {
        public MyContext(string connStr) : base(connStr) { }

        public Table<Day> DayTable;

    }
}
