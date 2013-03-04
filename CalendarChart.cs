using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyChart
{
    public class CalendarChart : BaseChart
    {
        public int Year { get; set; }

        public CalendarChart(int year, int width, int height)
            : base(width, height)
        {
            Year = year;
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
            Bitmap b = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(b);
            graphics.DrawImage(b, 0, 0);

            int width = (int)(0.9 * Width / 4);
            int height = Height / 3;

            for (int m = 0; m < 12; m++)
            {
                int y = (m) / 4;
                int x = m % 4;
                Rectangle rect = new Rectangle(x * width, y * height, x + width - width / 20, y + height - height / 20);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(m * 255 / 12, m * 255 / 12, m * 255 / 12)), rect);

                Font drawFont = new Font("Arial", 8, FontStyle.Bold);
                StringFormat drawFormat = new StringFormat();
                //drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                int days = DateTime.DaysInMonth(Year, m + 1);
                DateTime firstDayOfMonth = new DateTime(Year, m + 1, 1);
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
                    DateTime curDate = new DateTime(Year, m + 1, d + 1);
                    var dow = curDate.DayOfWeek;
                    var doy = curDate.DayOfYear;
                    var week = doy / 7;

                    var val = (from da in Data where da.Key.Subtract(curDate).Days == 0 select da.Value).FirstOrDefault();

                    if (val == null) continue;

                    int dd = d + deltaD;
                    double v = (val - min)/(max - min);
                    if (v < 0) v = 0;
                    int size = (int)(v * (0.10 * height)) + 1;
                    Color col = Color.FromArgb((int)((1-v)*255), (int)((v)*128), 0);
                    Pen pen = new Pen(new SolidBrush(col));
                    
                    //if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday) col = Color.Red;

                    Rectangle ell = new Rectangle((int) (0.2*width + x*width + (0.12*width)*(int) (dd/7)),
                                                  (int)(0.12*height + y*height + (dd%7)*(0.12*height)), size, size);
                    graphics.FillEllipse(new SolidBrush(col), ell);
                }

                graphics.DrawString(months[m], drawFont, new SolidBrush(Color.DarkOrange), x * width + 15, y * height + 0, drawFormat);
            }

            graphics.Dispose();
            return b;
        }
    }
}
