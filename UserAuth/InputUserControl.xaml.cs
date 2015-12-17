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

            detector.Stop();
            totalInfoList.Add(detector.Total);
            detector = new Detector();
            iterationCount--;

            PasswordTextBox.Text = string.Empty;

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
        }

        public List<TotalInfo> GetLearnResult()
        {
            return totalInfoList.ToList();
        }

        //        }
        //            val -= 1;
        //        {
        //        else
        //        }
        //            val += 1;
        //        {
        //        if (listBool[j][depth])
        //    {

        //    for (var j = 0; j < listBool.Count; j++)
        //    double val = 0;
        //{
        //for (var depth = 0; depth < itemscount; depth++)

        //listTisf.Clear();

        //var itemscount = listBool.FirstOrDefault()?.Count ?? 0;


        //PasswordTextBox.Text = string.Empty;
        //    }

        //    listTisf.Add(val/listBool.Count);
        //}

        //return new List<TotalInfo>();

        //public List<bool> GetList()
        //{
        //    var list = new List<bool>();

        //    long preview = 0;
        //    foreach (var item in Total.DifferentTime)
        //    {
        //        list.Add(item > preview);
        //        preview = item;
        //    }

        //    return list;
        //}

        //public List<double> GetListFloat()

        //{
        //    var list = new List<double>();

        //    long preview = 0;
        //    foreach (var item in Total.DifferentTime)
        //    {
        //        if (item == 0 || preview == 0)
        //        {
        //            preview = item;
        //            continue;
        //        }


        //        list.Add(preview / (double)item);


        //        preview = item;
        //    }

        //    return list;
        //}
    }
}