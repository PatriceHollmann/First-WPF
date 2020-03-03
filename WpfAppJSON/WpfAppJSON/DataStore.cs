using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppJSON
{
    public class DataStore
    {
        public List<House> Houses { get; set; }
        public List<User> Users { get; set; }

    
        public DataStore(List<House> _houses, List<User> _users)
        {
            Users = _users??new List<User>();
            Houses = _houses ?? new List<House>();
        }
        public static DataStore Parsing()
        {
            var lines = File.ReadAllLines("input");
            var json = new StringBuilder();
            var jsonStart = false;
            var index = 0;
            ItemsContainerWithLable<House> houses =null;
            ItemsContainerWithLable<User> users=null;
            foreach (var line in lines)
            {

                if (line.Trim() == "/-----")
                {
                    jsonStart = false;
                    var jsonString = json.ToString().Trim();
                    json.Clear();
                    if (jsonString.Length > 0)
                    {
                        switch (index)
                        {
                            case 0:

                                 houses = JsonConvert.DeserializeObject<ItemsContainerWithLable<House>>(jsonString);
                                break;

                            case 1:
                                 users = JsonConvert.DeserializeObject<ItemsContainerWithLable<User>>(jsonString);
                                
                                break;
                            default:

                                break;
                        }
                        json.Clear();
                        index++;
                    }
                }
                if (jsonStart)
                {
                    json.Append(line);
                }
                if (line.Trim() == "-----")
                {
                    jsonStart = true;
                }
            }
            return new DataStore(houses?.D?.Items, users?.D?.Items);
        }
        public void WriteToFile()
        {
            using (StreamWriter sw = new StreamWriter("output"))
            {
                StringBuilder sb = new StringBuilder();
                sw.WriteLine("-----");
                var wrapperdHouses = new ItemsContainerWithLable<House>
                {
                    Q = "HOUSES",
                    D = new ItemsContainer<House>
                    {
                        Items = this.Houses
                    }
                };
                string s = JsonConvert.SerializeObject(wrapperdHouses);
                sw.WriteLine(s);
                sw.WriteLine("/-----");
                sw.WriteLine("-----");
                var wrapperdUsers = new ItemsContainerWithLable<User>
                {
                    Q = "USERS",
                    D = new ItemsContainer<User>
                    {
                        Items = this.Users
                    }
                };
                string s1 = JsonConvert.SerializeObject(wrapperdUsers);
                sw.WriteLine(s1);
                sw.WriteLine("/-----");
            }
        }
    }
}
