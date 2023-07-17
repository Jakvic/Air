using System.Collections.Generic;

namespace Air.WPFDemo.Models
{
    public class Room
    {
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public List<Room> Rooms
        {
            get; set;
        } = new();
    }
}
