using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        public double currentValue;
        public List<double> weights = new List<double>();
        public Neuron()
        {
            //currentValue = 0.0;
            //weights.Add(0.0);
        }
    }
}
