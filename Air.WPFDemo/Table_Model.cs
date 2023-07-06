﻿using Air.WPFDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air.WPFDemo
{
    public class Table_Model : ViewModel<Table>
    {
        public ObservableCollection<User> Users =>
            new()
            {
                new User(1,"Jack",new DateTime(1991,1,1)),
                new User(3,"Desmond",new DateTime(1954,3,23)),
                new User(5,"Malen",new DateTime(2000,11,12)),
            };
    }
}