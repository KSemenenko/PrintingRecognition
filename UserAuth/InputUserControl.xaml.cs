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
        private Detector detector = new Detector();

        private int iterationCount = -1;

        public EventHandler<EventArgs> LearnComplete;

        private readonly List<TotalInfo> totalInfoList = new List<TotalInfo>();


        public InputUserControl()
        {
            InitializeComponent();
        }

        public void StartLearn(int сount)
        {
            iterationCount = сount;
            detector.Start();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (iterationCount == -1)
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
                LearnComplete?.Invoke(this, new EventArgs());
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


        //PasswordTextBox.Text = string.Empty;

        //var itemscount = listBool.FirstOrDefault()?.Count ?? 0;

        //listTisf.Clear();
        //for (var depth = 0; depth < itemscount; depth++)
        //{
        //    double val = 0;

        //    for (var j = 0; j < listBool.Count; j++)
        //    {
        //        if (listBool[j][depth])
        //        {
        //            val += 1;
        //        }
        //        else
        //        {
        //            val -= 1;
        //        }
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