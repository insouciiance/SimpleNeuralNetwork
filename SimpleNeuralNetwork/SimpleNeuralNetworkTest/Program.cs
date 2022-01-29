using System;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Core;
using SimpleNeuralNetwork.Data;
using SimpleNeuralNetwork.Services;

namespace SimpleNeuralNetworkTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ImageDataSet dataSet = new("truthy", "falsy", 100, 250);
            Model model = new (dataSet);
            model.Train();

            await ModelSerializer.SerializeToFileAsync(model, "model.txt");

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
