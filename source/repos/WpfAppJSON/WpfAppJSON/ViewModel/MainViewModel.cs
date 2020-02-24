using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<DataStore> SaveData { get; }
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

        //private string address;
        //public string Address
        //{
        //    get { return address; }
        //    set
        //    {
        //        this.address = value;
        //        RaisePropertyChanged("Address");
        //    }
        //}
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
        
            AddCommand = new DelegateCommand(() =>
            {
                
                //var address = this.Address;
                //var addressName = this.Address;
                //long typ;
                //collection.AddPerson(name, address, addressName, type, long flors, string login, long group, List<string> phones);
            });
            EditCommand = new DelegateCommand<DataStore>(x =>
            {
                var a =this.SelectedPerson;
            });
            DeleteCommand = new DelegateCommand<DataStore>(i =>
            {
                var selectedPerson = this.SelectedPerson;
               // collection.DeletePerson(this.SelectedPerson);
            });
            SaveData = new DelegateCommand<DataStore>(z =>
            {
             });

        }
    }
}

