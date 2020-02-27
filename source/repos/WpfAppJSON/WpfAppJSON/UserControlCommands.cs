using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppJSON
{
    class UserControlCommands
    {
        public class ShowDialogService
        {
            public void ShowPatientWindow()
            {
                var userControl = new UserControlModel();
                var userWindow = new Window1();

                userWindow.DataContext = userControl;

                userWindow.ShowDialog();
            }
        }
    }
}
