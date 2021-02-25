namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    /// <summary>
    /// A Template for server.property
    /// </summary>
    public class ServerPropertyModel
    {
        /// <summary>
        /// The Property Name
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// The Property Value
        /// </summary>
        public object Value { get; set; }

        public ServerPropertyModel(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
