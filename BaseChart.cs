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

        public BaseChart()
        {
            Data = new Dictionary<DateTime, float>();
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
