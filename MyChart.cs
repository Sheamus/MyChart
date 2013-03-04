using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyChart
{
    public class CalendarChart : BaseChart
    {
        private int year;

        public CalendarChart(int Year) : base()
        {
            year = Year;
        }

        public override void GetData()
        {
            base.GetData();
        }

        public override Bitmap Draw()
        {
            var max = (from r in Data select r.Value).Max();
            var min = (from r in Data select r.Value).Min();

            String[] months = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            Bitmap b = new Bitmap(400, 300);
            Graphics graphics = Graphics.FromImage(b);
            graphics.DrawImage(b, 0, 0);

            int width = 90;
            int height = 100;

            for (int m = 0; m < 12; m++)
            {
                int y = (m) / 4;
                int x = m % 4;
                Rectangle rect = new Rectangle(x * width, y * height, x + width - 5, y + height - 5);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(m * 255 / 12, m * 255 / 12, m * 255 / 12)), rect);

                Font drawFont = new Font("Arial", 8, FontStyle.Bold);
                StringFormat drawFormat = new StringFormat();
                //drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                int days = DateTime.DaysInMonth(year, m + 1);
                DateTime firstDayOfMonth = new DateTime(year, m + 1, 1);
                var fdow = firstDayOfMonth.DayOfWeek;
                int deltaD = 0;
                switch (fdow)
                {
                    case DayOfWeek.Monday: deltaD = 0; break;
                    case DayOfWeek.Tuesday: deltaD = 1; break;
                    case DayOfWeek.Wednesday: deltaD = 2; break;
                    case DayOfWeek.Thursday: deltaD = 3; break;
                    case DayOfWeek.Friday: deltaD = 4; break;
                    case DayOfWeek.Saturday: deltaD = 5; break;
                    case DayOfWeek.Sunday: deltaD = 6; break;
                }

                for (int d = 0; d < days; d++)
                {
                    DateTime curDate = new DateTime(year, m + 1, d + 1);
                    var dow = curDate.DayOfWeek;
                    var doy = curDate.DayOfYear;
                    var week = doy / 7;

                    var val = (from da in Data where da.Key.Subtract(curDate).Days == 0 select da.Value).FirstOrDefault();

                    if (val == null) continue;

                    int dd = d + deltaD;
                    Color col = Color.FromArgb(d*255/days, 127, 0);
                    
                    if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday) col = Color.Red;
                    
                    Pen pen = new Pen(new SolidBrush(col));

                    int size = (int)((val - min)/(max - min)*10)+1;

                    Rectangle ell = new Rectangle(x*width + dd/7*12, 12 + y*height + (dd%7)*12, size, size);
                    graphics.FillEllipse(new SolidBrush(col), ell);
                }

                graphics.DrawString(months[m], drawFont, new SolidBrush(Color.DarkOrange), x * width + 15, y * height + 0, drawFormat);
            }

            graphics.Dispose();
            return b;
        }
    }
}
