﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MVVMBase;
using Newtonsoft.Json;
using UserAuth.Model;

namespace UserAuth
{
    public class MainViewModel : BaseViewModel
    {
        private readonly InputUserControl ControlForLerning;
        private readonly InputUserControl ControlForLogin;
        private readonly string fileName = "config.json";

        private User currentUser;
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public MainViewModel(InputUserControl controlForLerning, InputUserControl controlForLogin)
        {
            Load();
            ControlForLerning = controlForLerning;
            ControlForLogin = controlForLogin;

            ControlForLerning.LearnComplete += LearnComplete;
            ControlForLogin.LoginComplete += LoginComplete;
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(() => Users);
            }
        }

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                Save();
                OnPropertyChanged(() => CurrentUser);
                OnPropertyChanged(() => CreatePasswordCommand);
            }
        }

        public ICommand AddNewUserCommand
        {
            get
            {
                return new DelegateCommand(executedParam =>
                {
                    var user = new User();
                    user.Name = "New User";
                    Users.Add(user);
                    CurrentUser = user;
                },
                    canExecutedParam => true);
            }
        }

        public ICommand CreatePasswordCommand
        {
            get
            {
                return new DelegateCommand(executedParam =>
                {
                    MessageBox.Show("Привет! Введите ваш пароль в синее поле 3 раз подряд.\nПосле ввода каждого пароля нажмите Enter");
                    ControlForLerning.IsEnabled = true;
                    ControlForLerning.StartLearn(3);
                    ControlForLerning.Focus();
                    ControlForLerning.SetFocus();
                },
                    canExecutedParam => CurrentUser?.Infos?.Count == 0);
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand(executedParam =>
                {
                    MessageBox.Show("Приготовтесь введить пароль!");
                    ControlForLogin.IsEnabled = true;
                    ControlForLogin.StartLogin();
                    ControlForLogin.Focus();
                    ControlForLogin.SetFocus();
                },
                    canExecutedParam => true);
            }
        }

        private void LoginComplete(object sender, EventArgs eventArgs)
        {
            var control = (InputUserControl)sender;
            var data = control.GetLearnResult();
            control.IsEnabled = false;
            control.CleanData();
            var name = CheckLogin(data.FirstOrDefault());
            MessageBox.Show("Ваше имя:" + name);
        }

        private string CheckLogin(TotalInfo info)
        {
            var userList = new List<Tuple<string, double>>();

            foreach (var user in Users)
            {
                foreach (var item in user.Infos.Where(w => w.Word == info.Word))
                {
                    userList.Add(new Tuple<string, double>(user.Name, Distance(info.DifferentTime, item.DifferentTime)));
                }
            }

            var min = userList.Min(m => m.Item2);
            var result = userList.FirstOrDefault(w => w.Item2 == min)?.Item1;
            return result ?? "no user";
        }

        private void LearnComplete(object sender, EventArgs eventArgs)
        {
            var control = (InputUserControl)sender;

            CurrentUser.Infos = control.GetLearnResult();
            CurrentUser.AverageValue = Average(CurrentUser.Infos);
            CurrentUser.Password = CurrentUser.Infos.FirstOrDefault()?.Word ?? string.Empty;

            control.IsEnabled = false;
            control.CleanData();
            OnPropertyChanged(() => CurrentUser);
            OnPropertyChanged(() => CreatePasswordCommand);
            OnPropertyChanged(() => Users);
            Save();
        }

        private TotalInfo Average(List<TotalInfo> items)
        {
            var result = new TotalInfo();
            result.DelayTime = Convert.ToInt64(items.Average(a => a.DelayTime));
            result.CharPerMinute = items.Average(a => a.CharPerMinute);
            result.TotalTime = Convert.ToInt64(items.Average(a => a.TotalTime));

            List<List<long>> listItem;

            listItem = items.Select(item => item.KeyPressTime).ToList();
            result.KeyPressTime = AverageArray(listItem);

            listItem = items.Select(item => item.DifferentTime).ToList();
            result.DifferentTime = AverageArray(listItem);

            listItem = items.Select(item => item.Time).ToList();
            result.Time = AverageArray(listItem);

            return result;
        }

        private List<long> AverageArray(List<List<long>> items)
        {
            var result = new List<long>();
            var itemscount = items.FirstOrDefault()?.Count ?? 0;

            for (var depth = 0; depth < itemscount; depth++)
            {
                var list = new List<long>();

                for (var i = 0; i < items.Count; i++)
                {
                    list.Add(items[i][depth]);
                }
                result.Add(Convert.ToInt64(list.Average()));
            }

            return result;
        }

        private double Distance(List<long> first, List<long> second)
        {
            if (first.Count != second.Count)
            {
                return double.MaxValue;
            }

            double acc = 0;

            for (var i = 0; i < first.Count; i++)
            {
                var temp = Convert.ToDouble(second[i] - first[i]);
                acc += temp*temp;
            }

            return Math.Sqrt(acc);
        }

        private void Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Users);
                File.WriteAllText(fileName, json);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
        }

        private void Load()
        {
            try
            {
                var json = File.ReadAllText(fileName);
                Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось загузить из файла");
            }

            if (Users == null)
            {
                Users = new ObservableCollection<User>();
            }
        }
    }
}