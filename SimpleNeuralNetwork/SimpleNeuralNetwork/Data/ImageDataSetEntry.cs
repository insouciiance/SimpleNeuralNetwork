using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Data
{
    public record ImageDataSetEntry(string Path, bool Truthy)
    {
        public Bitmap GetBitmap(int size)
        {
            Bitmap original = (Bitmap) Image.FromFile(Path);
            Bitmap resized = new (original, new Size(size, size));
            return resized;
        }
    }
}
