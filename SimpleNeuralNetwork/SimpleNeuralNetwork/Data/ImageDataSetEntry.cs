using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Services;

namespace SimpleNeuralNetwork.Data
{
    public class ImageDataSetEntry
    {
        public int[] Colors { get; }

        public bool Truthy { get; }

        public ImageDataSetEntry(string path, int sideSize, bool truthy)
        {
            Truthy = truthy;
            Bitmap original = (Bitmap)Image.FromFile(path);
            Bitmap resized = new(original, new Size(sideSize, sideSize));
            Colors = BitmapColorService.GetBitmapColors(resized);
        }
    }
}
