using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.BackgroundJobs
{
    //Periyodik olarak çalışan job türü.Periyodu biz belirliyoruz
    public class RecuringJob
    {
        public static void ReportingJob()
        {
            //Son parametrede Crontab periyodu linux tabanlı belirleyen bir işaretlemedir.Ben burada dk da 1 çalışacak şekilde ayarladım
            //İlk parametre bu metodu değiştirmek için verdiğim bir ID dir
            Hangfire.RecurringJob.AddOrUpdate("reportJob1", () => EmailReport(), "* * * * *");
        }

        //Job içerisinde kullanacağımız metotlar public olmalıdır
        public static void EmailReport()
        {
            Debug.WriteLine("Rapor Gönderildi :" + DateTime.Now);
        }
    }
}
