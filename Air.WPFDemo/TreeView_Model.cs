using Air.WPFDemo.Models;
using System.Collections.Generic;

namespace Air.WPFDemo
{
    public class TreeView_Model : ViewModel<TreeView>
    {
        public List<Room> Items { get; set; } = new()
        {
            new ()
            {
                Name = "Star",
                Size= 10,
                Rooms = new()
                {
                    new ()
                    {
                        Name="Living Room",Size=25
                    }
                }
            },
            new ()
            {
                Name = "Golden",Size=513,
                Rooms = new()
                {
                    new ()
                    {
                        Name="Bathroom",Size=10
                    }
                }
            },  new ()
            {
                Name = "King",Size=0x12,
                Rooms = new()
                {
                    new ()
                    {
                        Name="Kitchen",Size=15
                    }
                }
            },
        };
    }
}
