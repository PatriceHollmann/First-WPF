using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppJSON
{
    public static class ViewModelBase
    {
        public static Window wnd;
        public static UserControlModel userControl;
        public static void CloseWindow()
        {
            try
            {
                ((Window)wnd).Close();
            }
            catch (Exception e)
            {
                //    string value = e.Message.ToString();
            }
        }
        public static void OpenWindow(UserControlModel userControl)
        {

            wnd.DataContext = userControl;
            wnd.ShowDialog();
        }
    }
}
