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

    /// </summary>
    public partial class MainWindow : Window
    {
        JSon1.Welcome houses = new JSon1.Welcome();
        JSon2.Welcome users = new JSon2.Welcome();
        List<string> ls = new List<string>();
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
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonGet_Click(object sender, System.EventArgs e)
        {
                Parsing();
                //Добавлять строки
                foreach (var nameH in houses.D.Items.Select(x => x.Name))
                {
                    listBox1.Items.Add(nameH);
                }
                foreach (var nameU in users.D.Items.Select(x => x.Name))
                {
                    listBox2.Items.Add(nameU);
                }
            //listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            ls.Clear();
        }

        private void MenuEdit1_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            var a = listBox1.SelectedIndex;
            win1.TxtBox10.Text = houses.D.Items.Select((x => x.Name)).ElementAt(a);
            win1.TxtBox11.Text = houses.D.Items.Select((x => x.Address)).ElementAt(a);
            win1.TxtBox12.Text = houses.D.Items.Select((x => x.Type)).ElementAt(a).ToString();
            win1.TxtBox13.Text = houses.D.Items.Select((x => x.Flors)).ElementAt(a).ToString();
            //win1.listBox2.Items.Add(houses.D.Items.Select((x => x.Name)).ElementAt(a));
            win1.Show();
        }
        private void MenuEdit11_Click(object sender, RoutedEventArgs e)
        {
            var a = listBox1.SelectedIndex;
            while (listBox1.SelectedItems.Count != 0)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[0]);
            }
        }
            private void ButtonSend_Click(object sender, System.EventArgs e)
        {
            //Добавлять строки в цикле
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                //ls1.Add(listBox1.Items[i].ToString());
            }
        }

        private void listBox1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {


            }
        }
    }
    public partial class Window1 : Window
    {
        MainWindow mw = new MainWindow();

    }
}
