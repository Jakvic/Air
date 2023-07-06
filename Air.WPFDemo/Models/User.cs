using System;

namespace Air.WPFDemo.Models
{
    public class User
    {
        public User(int id, string name, DateTime birthday)
        {
            Id=id;
            Name=name;
            Birthday=birthday;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
