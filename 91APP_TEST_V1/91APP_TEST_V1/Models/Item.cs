using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _91APP_TEST_V1.Models
{
    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int cost { get; set; }
        public string status { get; set; }

        public Item()
        {
            id = string.Empty;
            name = string.Empty;
            price = 0;
            cost = 0;
            status = string.Empty;
        }

        public Item(string _id, string _name, int _price, int _cost, string _status)
        {
            id = _id;
            name = _name;
            price = _price;
            cost = _cost;
            status = _status;
        }
    }
}
