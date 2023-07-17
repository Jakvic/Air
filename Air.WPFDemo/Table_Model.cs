using System;
using System.Collections.ObjectModel;
using Air.WPFDemo.Models;

namespace Air.WPFDemo;

public class Table_Model : ViewModel<Table>
{
    public ObservableCollection<User> Users =>
        new()
        {
            new User(1, "Jack", new DateTime(1991, 1, 1)),
            new User(2, "Jack", new DateTime(1991, 1, 1)),
            new User(3, "Jack", new DateTime(1991, 1, 1)),
            new User(4, "Jack", new DateTime(1991, 1, 1)),
            new User(12, "Jack", new DateTime(1991, 1, 1)),
            new User(123, "Jack", new DateTime(1991, 1, 1)),
            new User(43, "Desmond", new DateTime(1954, 3, 23)),
            new User(415, "Malen", new DateTime(2000, 11, 12)),
            new User(45, "Malen", new DateTime(2000, 11, 12)),
            new User(34, "Malen", new DateTime(2000, 11, 12)),
            new User(15, "Malen", new DateTime(2000, 11, 12)),
            new User(51, "Malen", new DateTime(2000, 11, 12)),
            new User(15, "Malen", new DateTime(2000, 11, 12)),
            new User(55, "Malen", new DateTime(2000, 11, 12)),
            new User(515, "Malen", new DateTime(2000, 11, 12)),
            new User(5615, "Malen", new DateTime(2000, 11, 12)),
            new User(615, "Malen", new DateTime(2000, 11, 12)),
            new User(617, "Ivan", new DateTime(1984, 7, 21))
        };
}