using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyChart
{
    public class BaseChart : IChart
    {
        public Dictionary<DateTime, float> Data;
        public int Width { get; set; }
        public int Height { get; set; }

        public BaseChart(int width, int height)
        {
            Data = new Dictionary<DateTime, float>();
            Width = width;
            Height = height;
        }

        public virtual void GetData()
        {
        }

        public virtual Bitmap Draw()
        {
            return null;
        }
    }
}
