namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    public class ModModel
    {
        // Private Variables
        private bool _enabled = true;


        // Public Variables
        /// <summary>
        /// The path to 
        /// </summary>
        public string Path { get; set; }
        public string FileName => System.IO.Path.GetFileName(Path);
        public bool IsEnabled
        {
            get => _enabled;
            set
            {
                if (value)
                {
                    if (System.IO.File.Exists(Path))
                    {

                    }
                }
                _enabled = value;
            }
        }

    }
}
