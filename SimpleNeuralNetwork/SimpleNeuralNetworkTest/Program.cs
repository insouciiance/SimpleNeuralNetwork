using System;
using SimpleNeuralNetwork.Data;
using SimpleNeuralNetwork.Services;

namespace SimpleNeuralNetworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageDataSet dataSet = new(@"D:\Downloads\data\cropped", @"D:\Downloads\data\falsy");
            int[] colors = BitmapColorService.GetBitmapColors(dataSet.Entries[0].GetBitmap(100));
        }
    }
}
