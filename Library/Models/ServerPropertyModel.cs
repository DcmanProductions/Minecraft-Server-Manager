using System;
using System.Collections.Generic;
using System.Text;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    public class ServerPropertyModel
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public ServerPropertyModel(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public ServerPropertyModel()
        {
        }
    }
}
