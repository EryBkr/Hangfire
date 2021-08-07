using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.BackgroundJobs
{
    //Yapılacak işlemin başlangıç zamanını belirlediğimiz süre kadar öteleyebiliriz
    public class DelayedJob
    {
        public static string AddWaterMarkJob(string fileName, string watermarkText)
        {
            //Çağrıldıktan 30 sn sonra tetiklenecek metodumuzu oluşturduk,kullanıcı kaydı işlemi böylece hemen bitebilecektir
            //Geriye string tipinde Unique Job ID dönecektir
            //Job ID Continuations Job kullanmak istersek işimize yarayacaktır.
            return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(fileName, watermarkText), TimeSpan.FromSeconds(20));
        }

        //Resme yazı ekleyeceğiz
        //Job içerisinde kullanacağımız metotlar public olmalıdır
        public static void ApplyWatermark(string fileName, string watermarkText)
        {
            //Resim yolunu aldık
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new Bitmap(width: bitmap.Width, height: bitmap.Height))
                {
                    using (Graphics graph = Graphics.FromImage(tempBitmap))
                    {
                        graph.DrawImage(bitmap, 0, 0);
                        var font = new Font(FontFamily.GenericSerif, 25, FontStyle.Bold);
                        var color = Color.FromArgb(255, 0, 0);
                        var brush = new SolidBrush(color);
                        var point = new Point(20, bitmap.Height - 50);

                        //Verdiğimiz özelliklerle belirlediğimiz yazıyı yazdırıyoruz
                        graph.DrawString(watermarkText, font, brush, point);
                        //Yazı yazılmış resmi klasöre ekledim
                        tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/watermarks", fileName));
                    }
                }
            }
        }
    }
}
