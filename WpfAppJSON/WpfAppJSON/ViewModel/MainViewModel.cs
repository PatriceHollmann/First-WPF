using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WpfAppJSON
{
    public class MainViewModel : BindableBase
    {
        public ObservableCollection<User>  users = new ObservableCollection<User>(new List<User>());
        public ObservableCollection<User> Users 
        { get 
            { 
                return this.users; 
            } 
          set 
            { 
                this.users = value;
            } 
        }
        public DataStore dataStore;
      
        public DelegateCommand GetData { get; }
        public DelegateCommand SendData { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand<DataStore> DeleteCommand { get; }

        private User selectedPerson;
        public User SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                if (value != null)
                {
                    SelectedHouse = dataStore.Houses.SingleOrDefault(a => a.UserId == value.Id);
                } 
                else 
                {
                    SelectedHouse = null;
                }
                RaisePropertyChanged("SelectedPerson");
            }
        }
        private House selectedHouse;
        public House SelectedHouse
        {
            get { return selectedHouse; }
            set
            {
                selectedHouse = value;
                RaisePropertyChanged("SelectedHouse");
            }
        }
        public MainViewModel()
        {
            GetData = new DelegateCommand(() =>
                {
                    if (dataStore is null)
                    {
                        this.dataStore = DataStore.Parsing();
                        UpdateUsers();
                    }
                });

            SendData = new DelegateCommand(() =>
            {
                this.dataStore.WriteToFile();
            });

            AddCommand = new DelegateCommand(() =>
            {
                var dataContext = new EditUserViewModel(dataStore); 
                var userWindow = new Window1(dataContext);
                userWindow.DataContext = dataContext;
                RaisePropertyChanged("Users");
                RaisePropertyChanged("Houses");
                userWindow.Closed += UserWindow_Closed;
                userWindow.ShowDialog();
            });

            EditCommand = new DelegateCommand(() =>
            {
                var dataContext = new EditUserViewModel(dataStore, this.SelectedPerson, this.SelectedHouse);
                var userWindow = new Window1(dataContext);
                userWindow.DataContext = dataContext;
                RaisePropertyChanged("Users");
                RaisePropertyChanged("Houses");
                userWindow.Closed += UserWindow_Closed;
                userWindow.ShowDialog();
            });

            DeleteCommand = new DelegateCommand<DataStore>(i =>
            {
                this.dataStore.Users.Remove(SelectedPerson);
                this.dataStore.Houses.Remove(SelectedHouse);
                this.Users.Remove(SelectedPerson);
                RaisePropertyChanged("Users");
                RaisePropertyChanged("Houses");
            });
        }
        public void UpdateUsers() {
            this.Users.Clear();
            foreach (var user in dataStore.Users)
            {
                this.Users.Add(user);
            }
            RaisePropertyChanged("Users");
        }
     
        private void UserWindow_Closed(object sender, EventArgs e)
        {
            UpdateUsers();
            RaisePropertyChanged("Users");
            RaisePropertyChanged("Houses");
        }
    }
}

