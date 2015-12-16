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
            detector.Run();
            
        }

        private void PasswordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            detector.NewLetter(e.Text);
        }

        private void PasswordTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                GoButton_Click(null, null);
        }
    }
}
