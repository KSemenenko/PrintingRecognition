using System;
using System.Windows;


namespace UserAuth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel model = new MainViewModel();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}