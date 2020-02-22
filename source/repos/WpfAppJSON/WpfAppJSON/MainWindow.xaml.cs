using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.Specialized;

namespace WpfAppJSON
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    public class JSon1
    {
        public class Welcome
        {
            [JsonProperty("Q")]
            public string Q { get; set; }

            [JsonProperty("D")]
            public D D { get; set; }
        }

        public class D
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }

        public class Item
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("flors")]
            public long Flors { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
    public class JSon2
    {
        public class Welcome
        {
            [JsonProperty("Q")]
            public string Q { get; set; }

            [JsonProperty("D")]
            public D D { get; set; }
        }

        public class D
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }

        public class Item
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("login")]
            public string Login { get; set; }

            [JsonProperty("group")]
            public long Group { get; set; }

            [JsonProperty("phones")]
            public Phone[] Phones { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }

        public class Phone
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
    public class Model
    {
        public JSon1.Welcome Houses { get; set; }
        public JSon2.Welcome Users { get; set; }

        readonly List<string> ls = new List<string>();
        public Model(JSon1.Welcome _houses, JSon2.Welcome _users)
        {
            Users = _users;
            Houses = _houses;
        }
        public void Parsing()
        {
            using (StreamReader sr = new StreamReader("input"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.StartsWith("-"))
                    {
                        line.Trim();
                    }
                    else
                    {
                        ls.Add(line);
                    }
                }
                var desStringH = string.Join("", ls.TakeWhile(x => !x.StartsWith("/")).Where(y => !y.Contains("-")).ToList().ToArray());
                Houses = JsonConvert.DeserializeObject<JSon1.Welcome>(desStringH);
                var desStringU = string.Join("", ls.SkipWhile(x => !x.StartsWith("/")).Where(y => !y.Contains("-")).ToList().ToArray());
                Users = JsonConvert.DeserializeObject<JSon2.Welcome>(desStringU);
                ls.Clear();
            }
        }
        public void WriteToFile(JSon1.Welcome houses, JSon2.Welcome users)
        {
            using (StreamWriter sw = new StreamWriter("output"))
            {
                StringBuilder sb = new StringBuilder();
                sw.WriteLine("-----");

                string s = JsonConvert.SerializeObject(houses);
                sw.WriteLine(s);
                sw.WriteLine("/-----");
                sw.WriteLine("-----");

                string s1 = JsonConvert.SerializeObject(users);
                sw.WriteLine(s1);
                sw.WriteLine("/-----");
            }
        }
    }
    public class Collection: BindableBase
    {
        private readonly ObservableCollection<Model> _personCollection = new ObservableCollection<Model>();
        public ObservableCollection<Model> PersonCollection { get; }
        public Collection()
        {
            PersonCollection = new ObservableCollection<Model>(_personCollection);
        }
        public void AddPerson(Model model)
        {
            _personCollection.Add(model);
            RaisePropertyChanged("Person");
        }
        public void DeletePerson(int index)
        {
            if (index >= 0 && index < _personCollection.Count)
                _personCollection.RemoveAt(index);
            RaisePropertyChanged("Person");
        }
        public void EditPerson(Model model, int index)
        {
            if (index >= 0 && index < _personCollection.Count)
                _personCollection.RemoveAt(index);
            _personCollection.Add(model);
            RaisePropertyChanged("Person");
        }
    }
    public class ViewModel : BindableBase
    {
        public Collection collection = new Collection();
        public DelegateCommand AddCommand { get; }
        public DelegateCommand GetData { get; }
        public DelegateCommand SendData { get; }
        public DelegateCommand<Model> SaveData { get; }
        public DelegateCommand<Model> EditCommand { get; }
        public DelegateCommand<Model> DeleteCommand { get; }
        public ObservableCollection<Model> NewPersons => collection.PersonCollection;
        public ViewModel()
        {
            Window1 win1 = new Window1();
            MainWindow mw = new MainWindow();
            collection.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            AddCommand = new DelegateCommand(() =>
            {
                win1.TxtBox11.Text = null;
                win1.TxtBox12.Text = null;
                win1.TxtBox13.Text = null;
                win1.TxtBox14.Text = null;
                win1.TxtBox15.Text = null;
                win1.TxtBox16.Text = null;
                win1.TxtBox18.Text = null;
                win1.listBox1.Items.Clear();
                win1.Show();
            });
            EditCommand = new DelegateCommand<Model>(x =>
            {
                var a = mw.listBox1.SelectedIndex;
                win1.TxtBox11.Text = collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Name).ElementAt(a)).ToString();
                win1.TxtBox12.Text = collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Address).ElementAt(a)).ToString();
                win1.TxtBox13.Text = collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Type).ElementAt(a)).ToString();
                win1.TxtBox14.Text = collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Flors).ElementAt(a)).ToString();
                win1.TxtBox15.Text = collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Login).ElementAt(a)).ToString();
                win1.TxtBox16.Text = collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Group).ElementAt(a)).ToString();
                win1.TxtBox18.Text = collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Name).ElementAt(a)).ToString();
                win1.listBox1.Items.Add(collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Phones).ElementAt(a).Select(s=>s.Value)));
                win1.Show();
            });
            DeleteCommand = new DelegateCommand<Model>(i =>
            {
                var a = mw.listBox1.SelectedIndex;
                collection.DeletePerson(a);
            });
            SaveData = new DelegateCommand<Model>(z =>
           {
               long id = 1;
               collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Name).Append(win1.TxtBox11.Text));
               collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Address).Append(win1.TxtBox12.Text));
               if (Double.TryParse(win1.TxtBox13.Text, out double p))
               collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Type).Append((long)Math.Truncate(p)));
               if (Double.TryParse(win1.TxtBox14.Text, out double t))
               collection.PersonCollection.Select(y => y.Houses.D.Items.Select(n => n.Flors).Append((long)Math.Truncate(t)));
               collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Login).Append(win1.TxtBox15.Text));
               if (Double.TryParse(win1.TxtBox16.Text, out double s))
               collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Group).Append((long)Math.Truncate(s)));
               List<long> _Id = new List<long>();
               List<string> _Value = new List<string>();
               while (win1.listBox1.Items != null)
               {
                   for (int i = 0; i < win1.listBox1.Items.Count; i++)
                   {
                       collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Phones.Select(o => o.Value).Append(win1.listBox1.Items[i])));
                       collection.PersonCollection.Select(y => y.Users.D.Items.Select(n => n.Phones.Select(o => o.Id).Append(id)));
                       id++;
                   }
               }
           });
            GetData = new DelegateCommand(() =>
            {
                JSon1.Welcome houses = new JSon1.Welcome();
                JSon2.Welcome users = new JSon2.Welcome();
                Model model = new Model(houses, users);
                mw.listBox1.Items.Clear();
                mw.listBox2.Items.Clear();
                model.Parsing();
                //Добавлять строки
                foreach (var nameU in users.D.Items.Select(x => x.Name))
                {
                    mw.listBox1.Items.Add(nameU);
                }
                foreach (var adressU in houses.D.Items.Select(x => x.Address))
                {
                    mw.listBox2.Items.Add(adressU);
                }
            });
            SendData = new DelegateCommand(() =>
            {
                JSon1.Welcome houses = collection.PersonCollection.Select(y => y.Houses).FirstOrDefault();
                JSon2.Welcome users = collection.PersonCollection.Select(y => y.Users).FirstOrDefault();
                Model model = new Model(houses, users);
                model.WriteToFile(houses, users);
            });
            }
    }        //private static void PersonsCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
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
