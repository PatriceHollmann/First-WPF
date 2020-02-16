using System.Windows.Input;

namespace WpfAppJSON
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            GetData = new RoutedCommand("GetData", typeof(MainWindow));
        }
        public static RoutedCommand GetData { get; set; }
    }
}
