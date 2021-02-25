
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using System;
using System.Globalization;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities
{
    /// <summary>
    /// A Utility for handling files
    /// <br/>
    /// Ex: Converts bytes to human readabe numbers ending in KB, MB, GB
    /// </summary>
    public static class FileUtilities
    {
        private static readonly ChaseLabs.CLLogger.Interfaces.ILog log = ChaseLabs.CLLogger.LogManger.Init().SetLogDirectory(Values.Singleton.LogFile);
        /// <summary>
        /// Gets the File Size in Bytes based on String input
        /// <br/>
        /// Ex: 2KB = 2048
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static long FileSizeBytes(string format)
        {
            try
            {
                if (format.EndsWith("KB"))
                {
                    long num = long.Parse(format.Replace("KB", "")) * 1024;
                    return num;
                }
                else if (format.EndsWith("MB"))
                {
                    long num = (long.Parse(format.Replace("MB", "")) * 1024) * 1024;
                    return num;
                }
                else if (format.EndsWith("GB"))
                {
                    long num = ((long.Parse(format.Replace("GB", "")) * 1024) * 1024) * 1024;
                    return num;
                }
                else if (format.EndsWith("TB"))
                {
                    long num = (((long.Parse(format.Replace("TB", "")) * 1024) * 1024) * 1024) * 1024;
                    return num;
                }
            }
            catch (FormatException)
            {
                log.Info($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }

        /// <summary>
        /// Gets the File Size in Kilobytes based on String input
        /// <br/>
        /// Ex: 2MB = 2048
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static long SizeKB(string format)
        {
            try
            {

                try
                {
                    if (format.EndsWith("MB"))
                    {
                        long num = long.Parse(format.Replace("MB", "")) * 1024;
                        return num;
                    }
                    else if (format.EndsWith("GB"))
                    {
                        long num = (long.Parse(format.Replace("GB", "")) * 1024) * 1024;
                        return num;
                    }
                    else if (format.EndsWith("TB"))
                    {
                        long num = ((long.Parse(format.Replace("TB", "")) * 1024) * 1024) * 1024;
                        return num;
                    }
                }
                catch (FormatException)
                {
                    log.Info($"{format} can not be converted to Bytes");
                }
                catch { }
                return 0;
            }
            catch (FormatException)
            {
                log.Info($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }
        /// <summary>
        /// Gets the File Size in Megabytes based on String input
        /// <br/>
        /// Ex: 2MB = 2048
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double SizeMB(string format)
        {

            try
            {
                if (format.EndsWith("GB"))
                {
                    long num = long.Parse(format.Replace("GB", "")) * 1024;
                    return num;
                }
                else if (format.EndsWith("TB"))
                {
                    long num = (long.Parse(format.Replace("TB", "")) * 1024) * 1024;
                    return num;
                }
            }
            catch (FormatException)
            {
                log.Info($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }

        /// <summary>
        /// Gets the File Size in Gigabytes based on String input
        /// <br/>
        /// Ex: 2GB = 2048
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double SizeGB(string format)
        {

            try
            {
                if (format.EndsWith("TB"))
                {
                    long num = long.Parse(format.Replace("TB", "")) * 1024;
                    return num;
                }
            }
            catch (FormatException)
            {
                log.Info($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }



        /// <summary>
        /// Gets the File Size in Kilobytes based on Bytes
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double SizeKB(double file)
        {
            double num = file / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        /// <summary>
        /// Gets the File Size in Megabytes based on Bytes
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double SizeMB(double file)
        {
            double num = SizeKB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }

        /// <summary>
        /// Gets the File Size in Gigabytes based on Bytes
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double SizeGB(double file)
        {
            double num = SizeMB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }


        /// <summary>
        /// Gets the File Size in Highest abbreviation form based on string
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string AdjustedSize(string file)
        {
            return (FileSizeBytes(file) < 1024) ? FileSizeBytes(file) + "B" : (SizeKB(file) < 1024) ? SizeKB(file) + "KB" : (SizeMB(file) < 1024) ? SizeMB(file) + "MB" : SizeGB(file) + "GB";
        }

        /// <summary>
        /// Gets the File Size in Highest abbreviation form based on bytes
        /// <br />
        /// Ex: 2147483648 = 2GB
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string AdjustedSize(double bytes)
        {
            return (Math.Round(double.Parse("" + bytes, NumberStyles.Any, CultureInfo.InvariantCulture), 2) < 1024) ? Math.Round(double.Parse("" + bytes, NumberStyles.Any, CultureInfo.InvariantCulture), 2) + "B" : (Math.Round(SizeKB(bytes), 2) < 1024) ? Math.Round(SizeKB(bytes), 2) + "KB" : (Math.Round(SizeMB(bytes), 2) < 1024) ? Math.Round(SizeMB(bytes), 2) + "MB" : Math.Round(SizeGB(bytes), 2) + "GB";
        }
    }
}
