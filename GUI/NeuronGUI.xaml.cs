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
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GUI
{
    public sealed partial class NeuronGUI : UserControl
    {
        public string currentValueString { get; set; }
        public Neuron neuron = new Neuron();

        public NeuronGUI()
        {
            this.InitializeComponent();
            //neuron.currentValue = 0;
            //neuron.weights.Add(0.0);
            
        }

        internal void SetValue(object dp, int v)
        {
            //throw new NotImplementedException();
            
        }
    }
}
