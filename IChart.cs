using System.Drawing;

namespace MyChart
{
    public interface IChart
    {
        void GetData();
        Bitmap Draw();
    }
}