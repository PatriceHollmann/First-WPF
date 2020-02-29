using Prism.Commands;
using System.Linq;
using System.Windows;

namespace WpfAppJSON
{
   public class UserControlModel : MainViewModel
    {
        //private Window wnd = null;
        public DelegateCommand CommandClose { get; }
        public DelegateCommand<DataStore> AddPerson { get; }
        public DelegateCommand<DataStore> EditPerson { get; }

        public UserControlModel()
        {
            AddPerson = new DelegateCommand<DataStore>(j =>
                {
                    this.SelectedPerson = NewPerson;
                    this.SelectedHouse = NewHouse;
                    this.dataStore.Users.Append(NewPerson);
                    this.dataStore.Houses.Append(NewHouse);
                });
            EditPerson = new DelegateCommand<DataStore>(j =>
            {
                RaisePropertyChanged("Users");
                RaisePropertyChanged("Houses");
            });
            CommandClose = new DelegateCommand(() =>
            {
                if (ViewModelBase.wnd != null)
                    ViewModelBase.CloseWindow();
            });
        }
    }
}
