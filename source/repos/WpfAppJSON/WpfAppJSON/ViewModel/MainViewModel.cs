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
        public DelegateCommand<DataStore> AddCommand { get; }
        public DelegateCommand<DataStore> EditCommand { get; }
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
        private User newPerson;
        private House newHouse;
        public User NewPerson
        {
            get { return newPerson; }
            set
            {
                newPerson = value;
                RaisePropertyChanged("NewPerson");
                if (NewPerson.Phones != null)
                {
                    var phoneId = 1;
                    foreach (var phone in NewPerson.Phones)
                    {
                        this.NewPerson.Phones.Select(x => x.Id).Append(phoneId);
                        phoneId++;
                    }
                }
            }
        }
        public House NewHouse
        {
            get { return newHouse; }
            set
            {
                newHouse = value;
                RaisePropertyChanged("NewHouse");
            }
        }
        public MainViewModel()
        {
            GetData = new DelegateCommand(() =>
                {
                    this.dataStore = DataStore.Parsing();
                    foreach(var user in dataStore.Users)
                    {
                        this.Users.Add(user);
                        RaisePropertyChanged("Users");
                    }
                });

            SendData = new DelegateCommand(() =>
            {
                this.dataStore.WriteToFile();
            });

            AddCommand = new DelegateCommand<DataStore>(j =>
            {
                this.dataStore.Users.Append(NewPerson);
                this.dataStore.Houses.Append(NewHouse);
            });

            EditCommand = new DelegateCommand<DataStore>(x =>
            {

            });

            DeleteCommand = new DelegateCommand<DataStore>(i =>
            {
                this.dataStore.Users.Remove(this.SelectedPerson);
                this.dataStore.Houses.Remove(this.SelectedHouse);
            });
        }
    }
}

