using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppJSON
{
    public class ItemsContainerWithLable<T>
    {
        [JsonProperty("Q")]
        public string Q { get; set; }

        [JsonProperty("D")]
        public ItemsContainer<T> D { get; set; }
    }
    public class ItemsContainer<T>
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
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
        public override string ToString()
        {
            return Value;
        }
    }
    public class House
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("flors")]
            public long Flors { get; set; }
            [JsonProperty("user_id")]
            public int UserId { get; set; }

        public override string ToString()
            {
                return Name;
            }
        }
    
}
