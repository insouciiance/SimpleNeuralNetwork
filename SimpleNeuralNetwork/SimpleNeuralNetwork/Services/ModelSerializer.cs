using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Core;

namespace SimpleNeuralNetwork.Services
{
    public static class ModelSerializer
    {
        public static async Task SerializeToFileAsync(Model model, string fileName)
        {
            await using StreamWriter writer = new(fileName);

            await writer.WriteLineAsync(model.W.Length.ToString());

            foreach (double wi in model.W)
            {
                await writer.WriteLineAsync(wi.ToString(CultureInfo.InvariantCulture));
            }

            await writer.WriteLineAsync(model.B.ToString(CultureInfo.InvariantCulture));
        }

        public static async Task<Model> DeserializeFromFileAsync(string fileName)
        {
            using StreamReader reader = new(fileName);
            string wSizeString = await reader.ReadLineAsync() ?? throw new InvalidOperationException();
            int wSize = int.Parse(wSizeString);

            double[] w = new double[wSize];

            for (int i = 0; i < wSize; i++)
            {
                string wiString = await reader.ReadLineAsync() ?? throw new InvalidOperationException();
                double wi = double.Parse(wiString, CultureInfo.InvariantCulture);
                w[i] = wi;
            }

            string bString = await reader.ReadLineAsync() ?? throw new InvalidOperationException();
            double b = double.Parse(bString, CultureInfo.InvariantCulture);

            return new Model(w, b);
        }
    }
}
