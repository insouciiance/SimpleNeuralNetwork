using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Services
{
    public static class BitmapColorService
    {
        public static (int, int, int) GetPixelColors(Bitmap bitmap, int x, int y)
        {
            Color pixel = bitmap.GetPixel(x, y);
            return (pixel.R, pixel.G, pixel.B);
        }

        public static int[] GetBitmapColors(Bitmap bitmap)
        {
            int colorsSize = bitmap.Width * bitmap.Height * 3;
            int[] colors = new int[colorsSize];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixel = bitmap.GetPixel(i, j);
                    colors[i * bitmap.Width + j] = pixel.R;
                    colors[bitmap.Width * bitmap.Height + i * bitmap.Width + j] = pixel.G;
                    colors[2 * bitmap.Width * bitmap.Height + i * bitmap.Width + j] = pixel.B;
                }
            }

            return colors;
        }
    }
}
