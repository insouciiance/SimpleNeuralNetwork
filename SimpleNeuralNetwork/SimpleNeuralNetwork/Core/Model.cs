using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Data;
using SimpleNeuralNetwork.Services;

namespace SimpleNeuralNetwork.Core
{
    public class Model
    {
        public ImageDataSet DataSet { get; }

        public int SidePixelSize { get; }

        public double[] W { get; private set; }
        
        public double B { get; private set; }

        private int[][] _imagesColors;
        private double[] _y;

        public Model(ImageDataSet dataSet, int sidePixelSize = 100)
        {
            DataSet = dataSet;
            SidePixelSize = sidePixelSize;
        }

        public void Train(int iterationsCount = 1000)
        {
            Prepare();

            int iterationsDone = 0;

            Parallel.For(0, iterationsCount, i =>
            {
                RunForwardPropagation();
                double avgLoss = RunBackwardPropagation();

                Console.WriteLine($"i == {iterationsDone++} loss == {avgLoss}");
            });
        }

        private void Prepare()
        {
            W = new double[SidePixelSize * SidePixelSize * 3];
            B = 0;

            _y = new double[DataSet.Entries.Count];
            _imagesColors = new int[DataSet.Entries.Count][];

            for (int i = 0; i < DataSet.Entries.Count; i++)
            {
                ImageDataSetEntry entry = DataSet.Entries[i];
                _imagesColors[i] = entry.Colors;
            }
        }

        private void RunForwardPropagation()
        {
            Parallel.For(0, DataSet.Entries.Count, i =>
            {
                int[] x = _imagesColors[i];

                double wx = Matrix.Multiply(new Matrix(W), new Matrix(x).Transpose())[0, 0];
                double z = wx + B;

                double sigmoid = Sigmoid(z);

                _y[i] = sigmoid;
            });
        }

        private double RunBackwardPropagation(double learningRate = 0.005)
        {
            double totalLoss = 0;
            double[] totalDw = new double[SidePixelSize * SidePixelSize * 3];
            double totalDb = 0;

            Parallel.For(0, DataSet.Entries.Count, i =>
            {
                double y = DataSet.Entries[i].Truthy ? 1 : 0;
                double a = _y[i];

                double loss = -y * Math.Log(a) - (1 - y) * Math.Log(1 - a);

                double dz = a - y;

                double[] dw = new double[SidePixelSize * SidePixelSize * 3];

                for (int j = 0; j < _imagesColors[i].Length; j++)
                {
                    dw[j] = _imagesColors[i][j] * dz;
                }

                double db = dz;

                totalLoss += loss;

                for (int j = 0; j < dw.Length; j++)
                {
                    totalDw[j] += dw[j];
                }

                totalDb += db;
            });

            double avgLoss = totalLoss / DataSet.Entries.Count;
            
            double[] avgDw = new double[SidePixelSize * SidePixelSize * 3];

            for (int i = 0; i < totalDw.Length; i++)
            {
                avgDw[i] = totalDw[i] / DataSet.Entries.Count;
            }

            double avgDb = totalDb / DataSet.Entries.Count;

            for (int i = 0; i < W.Length; i++)
            {
                W[i] -= avgDw[i] * learningRate;
            }

            B -= avgDb * learningRate;

            return avgLoss;
        }

        public double Predict(ImageDataSetEntry entry)
        {
            double wx = Matrix.Multiply(new Matrix(entry.Colors), new Matrix(W).Transpose())[0, 0];
            double z = wx + B;
            return Sigmoid(z);
        }

        private double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));
    }
}
