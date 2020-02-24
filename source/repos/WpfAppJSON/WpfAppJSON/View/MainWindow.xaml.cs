
using System.Windows;


namespace WpfAppJSON
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
  
       //private static void PersonsCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add: // если добавление
        //            Model newPerson = e.NewItems[0] as Model;
        //            break;
        //        case NotifyCollectionChangedAction.Remove: // если удаление
        //            Model oldPerson = e.OldItems[0] as Model;
        //            break;
        //        case NotifyCollectionChangedAction.Replace: // если замена
        //            Model replacedPerson = e.OldItems[0] as Model;
        //            Model replacingPerson = e.NewItems[0] as Model;
        //            break;
        //    }
        /// </summary>
        public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        //private void ButtonGet_Click(object sender, System.EventArgs e) //добавление данных в форму
        //{
        //    listBox1.Items.Clear();
        //    Model.Parsing();
        //    //Добавлять строки
        //    foreach (var nameH in houses.D.Items.Select(x => x.Name))
        //    {
        //        listBox1.Items.Add(nameH);
        //    }
        //    foreach (var nameU in users.D.Items.Select(x => x.Name))
        //    {
        //        listBox2.Items.Add(nameU);
        //    }
        //    ls.Clear();
            //    }
            //    private void MenuEdit_Click(object sender, RoutedEventArgs e) //добавление нового houses
            //    {
            //        Window1 win1 = new Window1(); 
            //        win1.Show();
            //        long z = 0;
            //        Double value = 0;

            //       houses.D.Items.Select((x => x.Name)).Append(win1.TxtBox10.Text);
            //       houses.D.Items.Select((x => x.Address)).Append(win1.TxtBox11.Text);
            //        if (Double.TryParse(win1.TxtBox12.Text, out value))
            //        {
            //            z = (long)Math.Truncate(value);
            //        }
            //        houses.D.Items.Select((x => x.Type)).Append(z);
            //        if (Double.TryParse(win1.TxtBox13.Text, out value))
            //        {
            //            z = (long)Math.Truncate(value);
            //        }
            //       houses.D.Items.Select((x => x.Flors)).Append(z);

            //    }
            //    private void MenuEdit1_Click(object sender, RoutedEventArgs e) //редактирование houses
            //    {
            //        Window1 win1 = new Window1();
            //        var a = listBox1.SelectedIndex;
            //        win1.TxtBox10.Text = houses.D.Items.Select((x => x.Name)).ElementAt(a);
            //        win1.TxtBox11.Text = houses.D.Items.Select((x => x.Address)).ElementAt(a);
            //        win1.TxtBox12.Text = houses.D.Items.Select((x => x.Type)).ElementAt(a).ToString();
            //        win1.TxtBox13.Text = houses.D.Items.Select((x => x.Flors)).ElementAt(a).ToString();
            //        //win1.listBox2.Items.Add(houses.D.Items.Select((x => x.Name)).ElementAt(a));
            //        win1.Show();
            //    }
            //    private void MenuEdit11_Click(object sender, RoutedEventArgs e) //удаление houses
            //    {
            //        var a = listBox1.SelectedIndex;
            //        while (listBox1.SelectedItems.Count != 0)
            //        {
            //            listBox1.Items.Remove(listBox1.SelectedItems[0]);

            //           // houses.D.Items[a] = null;
            //        }
            //    }
            //        private void ButtonSend_Click(object sender, System.EventArgs e) //запись в файл
            //    {
            //        //Добавлять строки в цикле
            //        houses = null;
            //        users = null;
            //        for (int i=0; i<listBox1.Items.Count; i++)
            //        {
            //          // houses.D.Items.Append()
            //        }
            //        WriteToFile(houses, users);
            //    }
            //    private void listBox1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            //    {
            //        var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            //        if (item != null)
            //        {
            //        }
            //    }
            //}
            //public partial class Window1 : Window
            //{
            //    MainWindow mw = new MainWindow();
            //    private void ButtonSave_Click(object sender, System.EventArgs e) //запись в файл
            //    {

            //    }
            //    private void ButtonCancel_Click(object sender, System.EventArgs e) //запись в файл
            //    {

            //    }
        }
}
