using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Data;

namespace SimpleNeuralNetwork.Core
{
    public class Model
    {
        public ImageDataSet DataSet { get; }

        public int SidePixelSize { get; }

        public double[] W { get; private set; }
        
        public double B { get; private set; }

        public Model(ImageDataSet dataSet, int sidePixelSize = 100)
        {
            DataSet = dataSet;
            SidePixelSize = sidePixelSize;
        }

        private void Prepare()
        {
            W = new double[SidePixelSize * SidePixelSize * 3];
            B = 0;
        }

        private void RunForwardPropagation()
        {
            
        }
    }
}
