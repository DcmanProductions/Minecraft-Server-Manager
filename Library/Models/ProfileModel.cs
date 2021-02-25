using System;
using System.Collections.Generic;
using System.Text;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    /// <summary>
    /// Template for Profiles to create a server from.
    /// </summary>
    public class ProfileModel
    {
        /// <summary>
        /// The human readable name displayed in the UI.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The archive file name that will be extracted to server environment.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// If true will use URL to download archive file and create a server from it.
        /// </summary>
        public bool IsRemote { get; set; }
        /// <summary>
        /// If IsRemote is true, this will be used as the direct download link for the server profile archive file.
        /// </summary>
        public string URL { get; set; }
    }
}
