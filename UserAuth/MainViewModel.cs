using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase;
using UserAuth.Model;

namespace UserAuth
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(()=> Users);
            }
        }

        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(() => CurrentUser);
            }
        }

        public ICommand AddNewCommand
        {
            get
            {
                return new DelegateCommand((executedParam) =>
                {
                    User user = new User();
                    user.Name = "New User";
                    Users.Add(user);
                    CurrentUser = user;
                },
                (canExecutedParam) =>
                {
                    //CanCxecuted code
                    return true;
                });
            }
        }


    }
}
