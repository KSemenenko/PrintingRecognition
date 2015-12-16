using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace UserAuth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Detector detector = new Detector();

        List<Detector> list= new List<Detector>();
        List<List<bool>> listBool = new List<List<bool>>();
        List<List<double>> listfloat = new List<List<double>>();

        List<double> listTisf = new List<double>();


        private ActivationNetwork network;

        private bool isdetect;


        public MainWindow()
        {
            InitializeComponent();
            PasswordTextBox.Focus();
            
            detector.Run();

          
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            detector.Stop();
            list.Add(detector);
            listBool.Add(detector.GetList());
            listfloat.Add(detector.GetListFloat());
            detector = new Detector();
            PasswordTextBox.Text = String.Empty;


            ///

            var itemscount =  listBool.FirstOrDefault()?.Count ?? 0;

            listTisf.Clear();
            for (int depth = 0; depth < itemscount; depth++)
            {
                double val = 0;

                for (int j = 0; j < listBool.Count; j++)
                {

                    if (listBool[j][depth])
                        val += 1;
                    else
                        val -= 1;
                }

                listTisf.Add(val / listBool.Count);
            }

            detector.Run();

            if (isdetect)
            {
                var x = network.Compute(listfloat.FirstOrDefault().ToArray());
                listfloat.Clear();
            }

            
        }

        private void PasswordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Console.WriteLine("PreviewTextInput");
            detector.Add(e.Text);
        }

        private void PasswordTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("PreviewKeyUp");
            if (e.Key == Key.Enter)
                GoButton_Click(null, null);
        }

        private void PasswordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("PreviewKeyDown");
            detector.PreAdd();
        }

        void Teach(List<List<double>> inList)
        {
            bool needToStop = false;


            int inCount = inList.First().Count;


            // initialize input and output values
            double[][] input = inList.Select(a => a.ToArray()).ToArray();

            double[][] output = new double[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = new double[] {1};
            }

            // create neural network
            network = new ActivationNetwork(
                new SigmoidFunction(2),
                inCount, // two inputs in the network
                inCount * 2, // two neurons in the first layer
                1); // one neuron in the second layer
                    // create teacher
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            // loop
            while (!needToStop)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input, output);
                // check error value to see if we need to stop
                // ...
                if (error < 0.05)
                    needToStop = true;
            }





        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Teach(listfloat);
            isdetect = true;
            listfloat.Clear();
        }
    }
}
