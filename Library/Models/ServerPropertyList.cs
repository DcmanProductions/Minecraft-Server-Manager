using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    public class ServerPropertyList : List<ServerPropertyModel>
    {
        public string PropertyFile { get; private set; }

        public ServerPropertyList(string property_file, params ServerPropertyModel[] default_values)
        {
            PropertyFile = property_file;
            if (File.Exists(PropertyFile))
                Read();
            else
            {
                AddRange(default_values);
                Write();
            }
        }

        public void Read()
        {
            Clear();
            using (var reader = new StreamReader(PropertyFile))
            {
                while (!reader.EndOfStream)
                {
                    string text = reader.ReadLine();
                    if (text.StartsWith("#")) continue;
                    if (text.Contains("#"))
                        text = text.Split("#")[0].Replace("#", "");
                    Add(new ServerPropertyModel(text.Split('=')[0].Replace("=", ""), text.Split('=')[1].Replace("=", "")));
                }
            }
        }

        public void SetValue(string key, object value)
        {

            bool found = false;
            ForEach(p =>
            {
                if (p.Key.Equals(key))
                {
                    p.Value = value;
                    found = true;
                    return;
                }
            });
            if (!found)
                Add(new ServerPropertyModel(key, value));
            Write();
        }

        public ServerPropertyModel GetPropertyByKey(string key, string default_value = "")
        {
            ServerPropertyModel property = new ServerPropertyModel(key, default_value);
            ForEach(p =>
            {
                if (p.Key.Equals(key))
                {
                    property = p;
                }
            });
            return property;
        }

        public void Write()
        {
            using (var writer = new StreamWriter(PropertyFile, false))
            {
                writer.WriteLine("#Minecraft server properties");
                writer.WriteLine($"#Generated with {Values.ApplicationName}");
                ForEach(p =>
                {
                    writer.WriteLine($"{p.Key}={p.Value}");
                });
                writer.Flush();
                writer.Dispose();
                writer.Close();
            }
        }

    }
}
