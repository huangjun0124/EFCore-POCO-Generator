using System;
using System.Globalization;
using System.Linq;
using System.Text;
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
                var jobs = context.Jobs.ToList();
                Job job = new Job()
                {
                    StateId = 345,
                    Arguments = "thisis my wod",
                    CreatedAt = DateTime.Now,
                    InvocationData = "",
                    StateName = "my state"
                };
                context.Jobs.Add(job);
                context.SaveChanges();
                jobs = context.Jobs.ToList();
                var xxx = (from j in context.Jobs
                           where j.ExpireAt > DateTime.ParseExact("2018-04-07 14:58:35.222", "yyyy-MM-dd HH:mm:ss.fff",
                              CultureInfo.InvariantCulture)
                    select j).ToList();
                MyTable ta = new MyTable()
                {
                    MyId = "345345",
                    Dec1 = null,
                    MyTable_ = "samewithtbname",
                    Str1 = "nihao",
                    Str2 = "tayehao",
                    Varcharmax = "洗哦按时打发第三方打算"
                };
                //context.MyTables.Add(ta);
                //context.SaveChanges();
                var tas = context.MyTables.ToList();

                var views = context.JobParamViews.ToList();
                var xviews = context.View2.ToList();
                var xxxx = context.Testtypenotnulls.ToList();
                var xxxnull = context.Testtypenulls.ToList();

                var test = new Testtypenotnull()
                {
                    C1 = 1231231,
                    C2 = Encoding.UTF8.GetBytes("12345"),
                    C34 = "nihao",
                    Date = DateTime.Now,
                    Ddf = false,
                    Dec = 123.45657M,
                    Dt = DateTime.Now.AddDays(-10),
                    Dt2 = DateTime.Now.AddMonths(3),
                    Flo = 1234.32,
                    Img = Encoding.UTF8.GetBytes("sadfasdfdsafsd"),
                    Mnax = "sdfsdafsdafdsf",
                    Money = 1234.567M,
                    Nc = "dd",
                    Ntx = "34534534",
                    Numr = 123123.345M,
                    Nvar = "dfsd",
                    Pkid = 687687,
                    Real = 123.34f,
                    Sd = DateTime.Now,
                    Si = 123,
                    Sm = 123243.456M,
                    Tex = "dsfsdafdt45645523423",
                    Uid = new Guid(),
                    Vb = Encoding.UTF8.GetBytes("vdfg"),
                    Vbm = Encoding.UTF8.GetBytes("fg"),
                    Vc = "2131232435fdgfhfghgj",
                    Vm = "dsfasdfdsafdsafa",
                    Xml = "dfsdafsdafsddsf23423",
                    Yin = 255              
                };
                context.Testtypenotnulls.Add(test);
                context.SaveChanges();

                var testnull = new Testtypenull()
                {
                    Pkid = 676,
                    Real = 123.34f,
                    Sd = DateTime.Now,
                    Si = 123,
                    Sm = 123243.456M,
                    Tex = "dsfsdafdt45645523423",
                    Uid = new Guid(),
                    Vb = Encoding.UTF8.GetBytes("vdfg"),
                    Vbm = Encoding.UTF8.GetBytes("fg"),
                    Vc = "2131232435fdgfhfghgj",
                    Vm = "dsfasdfdsafdsafa",
                    Xml = "dfsdafsdafsddsf23423",
                    Yin = 255
                };
                context.Testtypenulls.Add(testnull);
                context.SaveChanges();
            }
        }
    }
}
