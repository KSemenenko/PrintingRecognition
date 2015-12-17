using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserAuth
{
    /// <summary>
    ///     Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        private Detector detector = new Detector();

        private readonly List<Detector> list = new List<Detector>();
        private readonly List<List<bool>> listBool = new List<List<bool>>();
        private readonly List<List<double>> listfloat = new List<List<double>>();

        private readonly List<double> listTisf = new List<double>();


        public InputUserControl()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            detector.Stop();
            list.Add(detector);
            listBool.Add(detector.GetList());
            listfloat.Add(detector.GetListFloat());
            detector = new Detector();
            PasswordTextBox.Text = string.Empty;


            ///

            var itemscount = listBool.FirstOrDefault()?.Count ?? 0;

            listTisf.Clear();
            for (var depth = 0; depth < itemscount; depth++)
            {
                double val = 0;

                for (var j = 0; j < listBool.Count; j++)
                {
                    if (listBool[j][depth])
                    {
                        val += 1;
                    }
                    else
                    {
                        val -= 1;
                    }
                }

                listTisf.Add(val/listBool.Count);
            }

            detector.Run();
        }

        private void PasswordTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            detector.Add(e.Text);
        }

        private void PasswordTextBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GoButton_Click(null, null);
            }
        }

        private void PasswordTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            detector.PreAdd();
        }
    }
}