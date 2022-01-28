using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Data
{
    public class ImageDataSet
    {
        public string TruthyDirectory { get; }

        public string FalsyDirectory { get; }

        public List<ImageDataSetEntry> Entries { get; } = new ();

        public ImageDataSet(string truthyDirectory, string falsyDirectory, int sideSize, int? imagesLimit = null)
        {
            if (!Directory.Exists(truthyDirectory))
            {
                throw new DirectoryNotFoundException();
            }

            if (!Directory.Exists(falsyDirectory))
            {
                throw new DirectoryNotFoundException();
            }

            TruthyDirectory = truthyDirectory;
            FalsyDirectory = falsyDirectory;

            string[] truthyImagePaths = Directory.GetFiles(TruthyDirectory);
            string[] falsyImagePaths = Directory.GetFiles(FalsyDirectory);

            int truthyImagesCount = 0;
            int falsyImagesCount = 0;

            foreach (string truthyImagePath in truthyImagePaths)
            {
                if (truthyImagesCount >= imagesLimit)
                {
                    break;
                }

                Entries.Add(new ImageDataSetEntry(truthyImagePath, sideSize, true));
                truthyImagesCount++;
            }

            foreach (string falsyImagePath in falsyImagePaths)
            {
                if (falsyImagesCount >= imagesLimit)
                {
                    break;
                }

                Entries.Add(new ImageDataSetEntry(falsyImagePath, sideSize, false));
                falsyImagesCount++;
            }
        }
    }
}
