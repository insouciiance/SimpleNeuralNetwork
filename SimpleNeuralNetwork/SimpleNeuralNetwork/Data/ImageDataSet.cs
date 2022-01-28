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

        public ImageDataSet(string truthyDirectory, string falsyDirectory)
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
        }

        public void Load()
        {
            string[] truthyImagePaths = Directory.GetFiles(TruthyDirectory);
            string[] falsyImagePaths = Directory.GetFiles(FalsyDirectory);

            foreach (string truthyImagePath in truthyImagePaths)
            {
                Entries.Add(new ImageDataSetEntry(truthyImagePath, true));
            }

            foreach (string falsyImagePath in falsyImagePaths)
            {
                Entries.Add(new ImageDataSetEntry(falsyImagePath, false));
            }
        }
    }
}
