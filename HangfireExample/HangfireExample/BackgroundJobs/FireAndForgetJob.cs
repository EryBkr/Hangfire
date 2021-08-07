using HangfireExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.BackgroundJobs
{
    //Bu job türü bir kez çalışır ve devamlılığı yoktur
    public class FireAndForgetJob
    {
        public static void EmailSendToUserJob(string userId,string message)
        {
            //Interface DI ile burada oluşacaktır
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(act => act.Sender(userId, message));
        }
    }
}
