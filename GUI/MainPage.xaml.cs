using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NeuralNetwork;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<double> neuron_values = new List<double>();
        List<double> output_values = new List<double>();
        List<Neuron> inputs = new List<Neuron>();
        List<Neuron> neurons = new List<Neuron>();
        List<Neuron> outputs = new List<Neuron>();
        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            //Learning part of the algo
            if (inputs.Count == 0)
            {
                inputs.Add(input1.neuron);
                inputs.Add(input2.neuron);

                input1.currentValueString = textBoxInput1.Text;
                input1.neuron.currentValue = float.Parse(input1.currentValueString);

                input2.currentValueString = textBoxInput2.Text;
                input2.neuron.currentValue = float.Parse(input2.currentValueString);
                Random rand = new Random();

                input1.neuron.weights.Add(rand.NextDouble());
                input1.neuron.weights.Add(rand.NextDouble());
                input1.neuron.weights.Add(rand.NextDouble());

                input2.neuron.weights.Add(rand.NextDouble());
                input2.neuron.weights.Add(rand.NextDouble());
                input2.neuron.weights.Add(rand.NextDouble());

            }

            //Testing time!
            else
            {
                input1.currentValueString = textBoxInput1.Text;
                input1.neuron.currentValue = float.Parse(input1.currentValueString);

                input2.currentValueString = textBoxInput2.Text;
                input2.neuron.currentValue = float.Parse(input2.currentValueString);
            }

            if (neurons.Count == 0)
            {
                neurons.Add(Neuron1.neuron);
                neurons.Add(Neuron2.neuron);
                neurons.Add(Neuron3.neuron);

                Random rand = new Random();

                Neuron1.neuron.weights.Add(rand.NextDouble());
                Neuron2.neuron.weights.Add(rand.NextDouble());
                Neuron3.neuron.weights.Add(rand.NextDouble());

            }
            
            if(outputs.Count == 0)
                outputs.Add(output.neuron);

            neuron_values = calculateValue(inputs, neurons);

            for(int item = 0; item< neurons.Count; item++)
                neurons[item].currentValue = sigmoid(neuron_values[item]);  

            output_values = calculateValue(neurons, outputs);
                        
            outputs[0].currentValue = sigmoid(output_values[0]);

            displayValues(inputs, neurons, outputs);
       
         }

        public List<double> calculateValue(List<Neuron> inputs, List<Neuron> neurons)
        {
            List<double> returnValue = new List<double>();

            int input_counter = 0;
            foreach (Neuron neuron in neurons)
            {
                double value = 0;
               foreach(Neuron inputNeuron in inputs)
                {
                    value += inputNeuron.currentValue * inputNeuron.weights.ElementAt(input_counter);
                }
                //value = sigmoid(value);
                returnValue.Add(value);
                input_counter++;
            }

            return returnValue;
        }
        public double sigmoid(double value)
        {
            return (1 / (1 + Math.Exp(0 - value)));
        }

        public void displayValues(List<Neuron> Inputs, List<Neuron> Neurons, List<Neuron> Outputs)
        {            
            int item=0;

            textBlockI1w1.Text = input1.neuron.weights[item++].ToString();
            textBlockI1w2.Text = input1.neuron.weights[item++].ToString();
            textBlockI1w3.Text = input1.neuron.weights[item].ToString();

            item = 0;
            textBlockI2w1.Text = input2.neuron.weights[item++].ToString();
            textBlockI2w2.Text = input2.neuron.weights[item++].ToString();
            textBlockI2w3.Text = input2.neuron.weights[item].ToString();

            item = 0;
            textBlockn1w1.Text = Neuron1.neuron.weights[item].ToString();
            textBlockn2w1.Text = Neuron2.neuron.weights[item].ToString();
            textBlockn3w1.Text = Neuron3.neuron.weights[item].ToString();

            item = 0;
            Neuron1TextBox.Text = Neuron1.neuron.currentValue.ToString();
            Neuron2TextBox.Text = Neuron2.neuron.currentValue.ToString();
            Neuron3TextBox.Text = Neuron3.neuron.currentValue.ToString();
            
            item = 0;

            OutputTextBox.Text = output.neuron.currentValue.ToString();

        }

        public double returningExpectedOutput(double input1 , double input2)
        {
            if (input1 == 1 && input2 == 1)
                return 4.0;
            else if (input1 == 4 && input2 == 4)
                return 4.0;
            else
                return 1.0;

        }

        public List<double> updateValues(List<double> deltaSum, List<Neuron> hidden, List<double> neuron_values)
        {
            List<double> deltaSumHidden = new List<double>();
            double value = 0.0; 
            for (int i =0; i<hidden.Count; i++)
            {
                value = (deltaSum[0] / hidden[i].weights[0]) * sprime(neuron_values[i]);
                deltaSumHidden.Insert(i, value);
            }

            return deltaSumHidden;
        }

        public double sprime(double v)
        {
            return (sigmoid(v) * (1 - sigmoid(v)));
        }

        public void updateWeights(List<double> deltaSum, List<Neuron> neurons)
        {
            int j = 0;
                      
            foreach(Neuron LayerNeurons in neurons)
            {
                j = 0;
                for (int i = 0; i < LayerNeurons.weights.Count; i++)
                    LayerNeurons.weights[i] = LayerNeurons.weights[i] + (deltaSum[j++] / LayerNeurons.currentValue);
            } 

        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            double expectedOutput = returningExpectedOutput(inputs[0].currentValue, inputs[1].currentValue);
            int item = 0;

            double errorOutput = expectedOutput - outputs[item].currentValue;

            List<double> deltaSum = new List<double>();
            deltaSum.Add(sprime(output_values[0]) * errorOutput);

            List<Neuron> hiddenCopy = new List<Neuron>(neurons);

            updateWeights(deltaSum, neurons);

            deltaSum = updateValues(deltaSum, hiddenCopy, neuron_values);

            updateWeights(deltaSum, inputs);

            neuron_values = calculateValue(inputs, neurons);

            for (item = 0; item < neurons.Count; item++)
                neurons[item].currentValue = sigmoid(neuron_values[item]);
            
            output_values = calculateValue(neurons, outputs);

            item = 0;

            outputs[item].currentValue = sigmoid(output_values[item]);

            displayValues(inputs, neurons, outputs);

        }
    }
}
