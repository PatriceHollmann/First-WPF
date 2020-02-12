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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppJSON
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml

    namespace QuickType
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

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

        public class Converter
        {
            private void Reader(string path)
            {
                List<JSon1.Welcome> strings = new List<JSon1.Welcome>();
                List<JSon2.Welcome> strings2 = new List<JSon2.Welcome>();
                string json = null;
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.ReadLine().Equals('/'))
                    {
                        json = sr.ReadLine().TrimStart(new Char[] { '-' });
                        JSon1.Welcome part1 = JsonConvert.DeserializeObject<JSon1.Welcome>(json);
                        strings.Add(part1);
                    }
                    sr.ReadLine().TrimStart(new Char[] { '/' });
                    while (!sr.ReadLine().Equals('/'))
                    {
                        json = sr.ReadLine().TrimStart(new Char[] { '-' });
                        JSon2.Welcome part2 = JsonConvert.DeserializeObject<JSon2.Welcome>(json);
                        strings2.Add(part2);
                    }
                }
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
    }
}
