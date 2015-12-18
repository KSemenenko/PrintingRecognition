using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UserAuth.Model;

namespace UserAuth
{
    /// <summary>
    ///     Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        private readonly List<TotalInfo> totalInfoList = new List<TotalInfo>();
        private Detector detector = new Detector();

        private bool isLogin;

        private int iterationCount = -1;

        public EventHandler<EventArgs> LearnComplete;
        public EventHandler<EventArgs> LoginComplete;

        private string password = string.Empty;


        public InputUserControl()
        {
            InitializeComponent();
        }

        public void StartLearn(int сount)
        {
            isLogin = false;
            iterationCount = сount;
            detector.Start();
        }

        public void StartLogin()
        {
            isLogin = true;
            iterationCount = 1;
            detector.Start();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (iterationCount == -1 || !detector.IsEnter())
            {
                return;
            }

            var pass = detector.Stop();

            PasswordTextBox.Text = string.Empty;

            if (!string.IsNullOrEmpty(pass))
            {
                if (string.IsNullOrEmpty(password))
                {
                    password = pass;
                }

                if (password == pass)
                {
                    totalInfoList.Add(detector.Total);

                    iterationCount--;

                    if (iterationCount == 0)
                    {
                        if (isLogin)
                        {
                            LoginComplete?.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            LearnComplete?.Invoke(this, new EventArgs());
                        }
                        return;
                    }
                }
            }

            detector = new Detector();
            detector.Start();
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

        public void SetFocus()
        {
            PasswordTextBox.Focus();
        }

        public void CleanData()
        {
            iterationCount = -1;
            detector = new Detector();
            totalInfoList.Clear();
            password = string.Empty;
        }

        public List<TotalInfo> GetLearnResult()
        {
            return totalInfoList.ToList();
        }
    }
}