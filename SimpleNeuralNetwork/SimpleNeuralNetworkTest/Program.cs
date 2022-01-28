using System;
using SimpleNeuralNetwork.Core;
using SimpleNeuralNetwork.Data;
using SimpleNeuralNetwork.Services;

namespace SimpleNeuralNetworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageDataSet dataSet = new(@"D:\Downloads\data\cropped", @"D:\Downloads\data\falsy", 100, 100);
            Model model = new (dataSet);
            model.Train();

            while (true)
            {
                Console.WriteLine("Path to file:");
                string path = Console.ReadLine();
                Console.WriteLine("Truthy?");
                bool truthy = bool.Parse(Console.ReadLine());
                ImageDataSetEntry entry = new(path, 100, truthy);

                double prediction = model.Predict(entry);

                Console.WriteLine($"Prediction: {prediction}");
                Console.WriteLine($"Difference: {Math.Abs(prediction - (truthy ? 1 : 0))}");
            }
        }
    }
}
