using System;
using System.Windows;


namespace UserAuth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel model; 

        public MainWindow()
        {
            InitializeComponent();
            model =new MainViewModel(LerningControl, LoginControl);
            this.DataContext = model;
        }
    }
}