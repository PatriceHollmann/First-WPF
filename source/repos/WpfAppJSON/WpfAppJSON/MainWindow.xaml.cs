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
        JSon1.Welcome houses = new JSon1.Welcome();
        JSon2.Welcome users = new JSon2.Welcome();
        List<string> ls = new List<string>();
        public Model(JSon1.Welcome _houses, JSon2.Welcome _users)
        {
            users = _users;
            houses = _houses;
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
                houses = JsonConvert.DeserializeObject<JSon1.Welcome>(desStringH);
                var desStringU = string.Join("", ls.SkipWhile(x => !x.StartsWith("/")).Where(y => !y.Contains("-")).ToList().ToArray());
                users = JsonConvert.DeserializeObject<JSon2.Welcome>(desStringU);
                //foreach (var item in users.D.Items)
                //{
                //    var a = users.D.Items.SelectMany(x => x.Name);
                //}
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
    public class Changes
    {
        JSon1.Welcome houses = new JSon1.Welcome();
        JSon2.Welcome users = new JSon2.Welcome();
        Persons ps = new Persons();
        private void PersonsCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Person newPerson = e.NewItems[0] as Person;
                    users.D.Items.Append()
                        .Select(x=>x.Name).Last().Append(ps.PersonCollection.)
                        ps.PersonCollection 
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    Person oldPerson = e.OldItems[0] as Person;
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    Person replacedPerson = e.OldItems[0] as Person;
                    Person replacingPerson = e.NewItems[0] as Person;
                    break;
            }
        }
    }
    public class Phone
    {
        JSon2.Welcome _users = new JSon2.Welcome();
        List<long> Id = new List<long>();
        List<string> Value = new List<string>();
        public Phone(List<long> id, List<string> value)
        {
            foreach (var item in (_users.D.Items).Select(x => x.Phones))
            {
                id = _users.D.Items.Select(x => x.Phones).FirstOrDefault().Select(x => x.Id).ToList();
                value = _users.D.Items.Select(x => x.Phones).FirstOrDefault().Select(x => x.Value).ToList();
            }
            Id = id;
            Value = value;
        }
    }
    public class Person
    {
        JSon1.Welcome _houses = new JSon1.Welcome();
        JSon2.Welcome _users = new JSon2.Welcome();
        public Phone Phone { get; }
        public string Name { get; }
        public string Login { get; }
        public long Group { get; }
        public string Adress { get; }
        public long Type { get; }
        public long Flors { get; }
        public Person(Phone phone, string name, string adress, long type, long flors, string login, long group)
        {
            // Model mod = new Model(_houses, _users);
            foreach (var item in _users.D.Items)
            {
                Name = _users.D.Items.Select(x => x.Name).FirstOrDefault();
                Login = _users.D.Items.Select(x => x.Login).FirstOrDefault();
                Group = _users.D.Items.Select(x => x.Group).FirstOrDefault();
            }
            foreach (var item in _houses.D.Items)
            {
                Adress = _houses.D.Items.Select(x => x.Address).FirstOrDefault();
                Type = _houses.D.Items.Select(x => x.Type).FirstOrDefault();
                Flors = _houses.D.Items.Select(x => x.Flors).FirstOrDefault();
            }
            phone = Phone;
            name = Name;
            login = Login;
            group = Group;
            adress = Adress;
            type = Type;
            flors = Flors;
        }
    }
    //public class PersonVM
    //{
    //    public Person Person { get; }
    //    public Phone Phone => Person.Phone;
    //    public string Name => Person.Name;
    //    public string Login => Person.Login;
    //    public long Group => Person.Group;
    //    public string Adress => Person.Adress;
    //    public long Type => Person.Type;
    //    public long Flors => Person.Flors;
    //    public PersonVM(Person person)
    //    {
    //        Person = person;
    //    }
    //}
    public class Persons : BindableBase
    {
        //protected bool add { get; set; }
        //protected bool edit { get; set; }
        //protected bool delete { get; set; }

        private readonly ObservableCollection<Person> _personCollection = new ObservableCollection<Person>();
        public ObservableCollection<Person> PersonCollection { get; }
        public Persons()
        {
            PersonCollection = new ObservableCollection<Person>(_personCollection);
            //add = false;
            //edit = false;
            //delete = false;
        }
        public void AddPerson(Person personVM)
        {
            _personCollection.Add(personVM);
            //add = true;
            RaisePropertyChanged("Person");
        }
        public void DeletePerson(int index)
        {
            if (index >= 0 && index < _personCollection.Count)
                _personCollection.RemoveAt(index);
            //delete = true;
            RaisePropertyChanged("Person");
        }
        public void EditPerson(Person personVM, int index)
        {
            if (index >= 0 && index < _personCollection.Count)
                _personCollection.RemoveAt(index);
            _personCollection.Add(personVM);
            //edit = true;
            RaisePropertyChanged("Person");
        }
    }
    public class ViewModel : BindableBase
    {
        readonly Persons prs = new Persons();
        public DelegateCommand<Person> AddCommand { get; }
        public DelegateCommand<Person> SaveData { get; }
        public DelegateCommand<Person> EditCommand { get; }
        public DelegateCommand<Person> DeleteCommand { get; }
        public ObservableCollection<Person> NewPersons => prs.PersonCollection;
        public ViewModel()
        {
            Window1 win1 = new Window1();
            MainWindow mw = new MainWindow();
            prs.PropertyChanged +=
            prs.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            AddCommand = new DelegateCommand<Person>(x =>
            {
                win1.TxtBox11.Text = null;
                win1.TxtBox12.Text = null;
                win1.TxtBox13.Text = null;
                win1.TxtBox14.Text = null;
                win1.TxtBox15.Text = null;
                win1.TxtBox16.Text = null;
                win1.listBox1.Items.Clear();
                win1.Show();
            });
            EditCommand = new DelegateCommand<Person>(x =>
            {
                long z = 0;
                Double value = 0;
                var a = mw.listBox1.SelectedIndex;
                win1.TxtBox11.Text = prs.PersonCollection.Select(y => y.Name).ElementAt(a);
                win1.TxtBox12.Text = prs.PersonCollection.Select(y => y.Adress).ElementAt(a);
                win1.TxtBox13.Text = prs.PersonCollection.Select(y => y.Type).ElementAt(a).ToString();
                win1.TxtBox14.Text = prs.PersonCollection.Select(y => y.Flors).ElementAt(a).ToString();
                win1.TxtBox15.Text = prs.PersonCollection.Select(y => y.Login).ElementAt(a);
                win1.TxtBox16.Text = prs.PersonCollection.Select(y => y.Group).ElementAt(a).ToString();
                win1.listBox1.Items.Add(prs.PersonCollection.Select(y => y.Phone.Value).ElementAt(a));
                win1.Show();
            });
            DeleteCommand = new DelegateCommand<Person>(i =>
            {
                var a = mw.listBox1.SelectedIndex;
                prs.DeletePerson(a);
            });
            SaveData = new DelegateCommand<Person>(z =>
           {
               Double t;
               Double p;
               Double s;
               string Name = win1.TxtBox11.Text;
               string Adress = win1.TxtBox12.Text;
               long Type=0;
               long Flors = 0;
               long Group = 0;
               if (Double.TryParse(win1.TxtBox13.Text, out p))
                   Type = (long)Math.Truncate(p);
               if (Double.TryParse(win1.TxtBox14.Text, out t))
                   Flors = (long)Math.Truncate(t);
               //long Flors = win1.TxtBox14.Text;
               string Login = win1.TxtBox15.Text;
               //var Group = win1.TxtBox16.Text;
               if (Double.TryParse(win1.TxtBox16.Text, out s))
                   Group = (long)Math.Truncate(s);
               List<long> _Id = new List<long>();
               List<string> _Value = new List<string>();
               while (win1.listBox1.Items != null)
               {
                   for (int i = 0; i < win1.listBox1.Items.Count; i++)
                   {
                       long Id = 1;
                       string Value = win1.listBox1.Items[i].ToString();
                       _Id.Add(Id);
                       _Value.Add(Value);
                   }
               }
               Phone phones = new Phone(_Id, _Value);
               Person pr = new Person(phones, Name, Adress, Type, Flors, Login, Group);
               prs.AddPerson(pr);
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
        //    private void ButtonGet_Click(object sender, System.EventArgs e) //добавление данных в форму
        //    {
        //        listBox1.Items.Clear();
        //        Parsing();
        //            //Добавлять строки
        //            foreach (var nameH in houses.D.Items.Select(x => x.Name))
        //            {
        //                listBox1.Items.Add(nameH);
        //            }
        //            foreach (var nameU in users.D.Items.Select(x => x.Name))
        //            {
        //                listBox2.Items.Add(nameU);
        //            }
        //        //listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        //        ls.Clear();
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
