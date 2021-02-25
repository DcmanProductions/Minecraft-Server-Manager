using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using System.Collections.Generic;
using System.IO;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{

    /// <summary>
    /// A List of all server properties
    /// <br />
    /// For more information on server properties see <seealso cref="ServerPropertyModel"/>
    /// </summary>
    public class ServerPropertyList : List<ServerPropertyModel>
    {
        /// <summary>
        /// The server.properties file
        /// </summary>
        public string PropertyFile { get; private set; }

        public ServerPropertyList(string property_file, params ServerPropertyModel[] default_values)
        {
            PropertyFile = property_file;
            if (File.Exists(PropertyFile))
            {
                Read();
            }
            else
            {
                AddRange(default_values);
                Write();
            }
        }

        /// <summary>
        /// Reads the <see cref="PropertyFile"/> and adds it to the list
        /// </summary>
        public void Read()
        {
            Clear();
            using (StreamReader reader = new StreamReader(PropertyFile))
            {
                while (!reader.EndOfStream)
                {
                    string text = reader.ReadLine();
                    if (text.StartsWith("#"))
                    {
                        continue;
                    }

                    if (text.Contains("#"))
                    {
                        text = text.Split("#")[0].Replace("#", "");
                    }

                    Add(new ServerPropertyModel(text.Split('=')[0].Replace("=", ""), text.Split('=')[1].Replace("=", "")));
                }
            }
        }

        /// <summary>
        /// Sets the Property value of specified key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
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
            {
                Add(new ServerPropertyModel(key, value));
            }

            Write();
        }

        /// <summary>
        /// Gets the value of a property based on property name
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Exports the properties to the <see cref="PropertyFile"/>
        /// </summary>
        public void Write()
        {
            using (StreamWriter writer = new StreamWriter(PropertyFile, false))
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
