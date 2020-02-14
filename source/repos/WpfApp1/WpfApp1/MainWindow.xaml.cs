using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Money;
using Newtonsoft.Json;

namespace WpfApp1
{
    [JsonObject]
    public class Currency
    {
        private string id;
        private string numCode;
        private string charCode;
        private string nominal;
        private string name;
        private double value;
        private double previous;

        [JsonProperty("ID")]
        public string Id { get => id; set => id = value; }
        [JsonProperty("NumCode")]
        public string NumCode { get => numCode; set => numCode = value; }
        [JsonProperty("CharCode")]
        public string CharCode { get => charCode; set => charCode = value; }
        [JsonProperty("Nominal")]
        public string Nominal { get => nominal; set => nominal = value; }
        [JsonProperty("Name")]
        public string Name { get => name; set => name = value; }
        [JsonProperty("Value")]
        public double Value { get => value; set => this.value = value; }
        [JsonProperty("Previous")]
        public double Previous { get => previous; set => previous = value; }
    }
    public class DayCurrency
    {
        private DateTime date;
        private DateTime previousDate;
        private string previousUrl;
        private DateTime timestamp;
        private Dictionary<string, Currency> valute;

        [JsonProperty("Date")]
        public DateTime Date { get => date; set => date = value; }
        [JsonProperty("PreviousDate")]
        public DateTime PreviousDate { get => previousDate; set => previousDate = value; }
        [JsonProperty("PreviousURL")]
        public string PreviousUrl { get => previousUrl; set => previousUrl = value; }
        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        [JsonProperty("Valute")]
        public Dictionary<string, Currency> Valute { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CurencyProvider curencyProvider;
        private DayCurrency data;
        public CurencyProvider CurencyProvider { get => curencyProvider; set => curencyProvider = value; }
        public DayCurrency Data { get => data; set => data = value; }

        public MainWindow()
        {
            InitializeComponent();
            CurencyProvider = new CurencyProvider();
        }

        private static async Task<DayCurrency> RequestAsync()
        {
            string json = null;
            WebRequest request = (WebRequest)WebRequest.Create("https://www.cbr-xml-daily.ru/daily_json.js");
            WebResponse response = (WebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            DayCurrency currency = JsonConvert.DeserializeObject<DayCurrency>(json);
            response.Close();
            return currency;
        }

        public static DayCurrency GetCurrency()
        {
            var result = RequestAsync().GetAwaiter().GetResult();
            return result;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data = GetCurrency();
            foreach (var item in Data.Valute)
            {
                dataView.Text = String.Format("Code: {0} \n", item.Key);
            }                    
        }
    }
}
