using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        }

        public class Phone
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
    public static class Methods
        {
        public static IEnumerable<T> DeserealizeJson<T>(this IEnumerable<string> en)
        {
            return en.Select(a => JsonConvert.DeserializeObject<T>(a));
        }
    }
    public class Converter
    {
        List<string> strings = new List<string>();
        public List<string> Reader(string path)
        {
 
            //List<JSon2.Welcome> strings2 = new List<JSon2.Welcome>();
            string json = null;
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    while (!sr.ReadLine().Equals('/'))
                    {
                        var line = sr.ReadLine();
                        json = line.TrimStart(new Char[] { '-' });
                        JSon1.Welcome part1 = JsonConvert.DeserializeObject<JSon1.Welcome>(json);
                      // Methods.DeserealizeJson<JSon1.Welcome>(json);
                            strings.Add(part1.ToString());
                    }
                    sr.ReadLine().TrimStart(new Char[] { '/' });
                    while (!sr.ReadLine().Equals('/'))
                    {
                        var line = sr.ReadLine();
                        json = line.TrimStart(new Char[] { '-' });
                        JSon2.Welcome part2 = JsonConvert.DeserializeObject<JSon2.Welcome>(json);
                         strings.Add(part2.ToString());
                    }
                }
            }
            return strings;
        }
        public List<T> Writer(string path)
        {
            List<JSon1.Welcome> strings = new List<JSon1.Welcome>();
            List<JSon2.Welcome> strings2 = new List<JSon2.Welcome>();
            using (StreamWriter sw = new StreamWriter(path))
            {
                var convert1 = JsonConvert.SerializeObject(strings);
                foreach (var item in convert1)
                {
                    sw.WriteLine(convert1);
                }
                    strings.Clear();
                var convert2 = JsonConvert.SerializeObject(strings2);
                foreach (var item in convert2)
                {
                    sw.WriteLine(convert2);
                }
                strings2.Clear(); 
            }
        }
    }
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GetData(object sender, ExecutedRoutedEventArgs e)
        {
            Converter cnv = new Converter();
           // List<string> mas1 = new List<string>();
            listBox.Items.Add(cnv.Reader("C\\Documents"));


        }
        //private void ButtonGet_Click (object sender, System.EventArgs e)
        //{
           
        //}
    }
    public class WindowCommands
    {
        static WindowCommands()
        {
            GetData = new RoutedCommand("GetData", typeof(MainWindow));
        }
        public static RoutedCommand GetData { get; set; }
    }
}
