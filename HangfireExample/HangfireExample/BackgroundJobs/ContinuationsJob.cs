using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.BackgroundJobs
{
    //Herhangi bir job sonrası tetiklenebilen bir job çeşitidir
    public class ContinuationsJob
    {
        public static void WriteWatermarkInformation(string id, string fileName)
        {
            Hangfire.BackgroundJob.ContinueJobWith(parentId: id, () => Debug.WriteLine("Resim oluşturuldu: "+fileName));
        }
    }
}
