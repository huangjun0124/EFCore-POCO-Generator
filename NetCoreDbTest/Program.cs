using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetCoreDbTest.Entity;

namespace NetCoreDbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Repository().CreateDbContext())
            {
                var jobs = context.JobClass.ToList();
                JobClass job = new JobClass()
                {
                    StateId = 123,
                    Arguments = "thisis my wod",
                    CreatedAt = DateTime.Now,
                    InvocationData = "",
                    StateName = "my state"
                };
                context.JobClass.Add(job);
                context.SaveChanges();
                jobs = context.JobClass.ToList();
                var xxx = (from j in context.JobClass
                    where j.ExpireAt > DateTime.ParseExact("2018-04-07 14:58:35.222", "yyyy-MM-dd HH:mm:ss.fff",
                              CultureInfo.InvariantCulture)
                    select j).ToList();
                MyTable ta = new MyTable()
                {
                    MyId = "luckhere",
                    dec1 = null,
                    MyTable1 = "samewithtbname",
                    str1 = "nihao",
                    str2 = "tayehao",
                    varcharmax = "洗哦按时打发第三方打算"
                };
                context.MyTables.Add(ta);
                context.SaveChanges();
                var tas = context.MyTables.ToList();
            }

            HangFire_AggregatedCounter ac = new HangFire_AggregatedCounter()
            {
                ExpireAt = DateTime.Now
            };
        }
    }
}
