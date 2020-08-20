using System;
using System.Globalization;
using System.IO;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities
{
    public static class FileUtilities
    {
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
                    long num = ( long.Parse(format.Replace("MB", "")) * 1024 ) * 1024;
                    return num;
                }
                else if (format.EndsWith("GB"))
                {
                    long num = ( ( long.Parse(format.Replace("GB", "")) * 1024 ) * 1024 ) * 1024;
                    return num;
                }
                else if (format.EndsWith("TB"))
                {
                    long num = ( ( ( long.Parse(format.Replace("TB", "")) * 1024 ) * 1024 ) * 1024 ) * 1024;
                    return num;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }
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
                        long num = ( long.Parse(format.Replace("GB", "")) * 1024 ) * 1024;
                        return num;
                    }
                    else if (format.EndsWith("TB"))
                    {
                        long num = ( ( long.Parse(format.Replace("TB", "")) * 1024 ) * 1024 ) * 1024;
                        return num;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{format} can not be converted to Bytes");
                }
                catch { }
                return 0;
            }
            catch (FormatException)
            {
                Console.WriteLine($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }
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
                    long num = ( long.Parse(format.Replace("TB", "")) * 1024 ) * 1024;
                    return num;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }
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
                Console.WriteLine($"{format} can not be converted to Bytes");
            }
            catch { }
            return 0;
        }


        public static double SizeKB(double file)
        {
            double num = file / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double SizeMB(double file)
        {
            double num = SizeKB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }
        public static double SizeGB(double file)
        {
            double num = SizeMB(file) / 1024;
            double.TryParse("" + num, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            num = Math.Round(num, 2);
            return num;
        }

        public static string AdjustedSize(string file)
        {
            return ( FileSizeBytes(file) < 1024 ) ? FileSizeBytes(file) + "B" : ( SizeKB(file) < 1024 ) ? SizeKB(file) + "KB" : ( SizeMB(file) < 1024 ) ? SizeMB(file) + "MB" : SizeGB(file) + "GB";
        }

        public static string AdjustedSize(double bytes)
        {
            return ( Math.Round(double.Parse("" + bytes, NumberStyles.Any, CultureInfo.InvariantCulture), 2) < 1024 ) ? Math.Round(double.Parse("" + bytes, NumberStyles.Any, CultureInfo.InvariantCulture), 2) + "B" : ( Math.Round(SizeKB(bytes), 2) < 1024 ) ? Math.Round(SizeKB(bytes), 2) + "KB" : ( Math.Round(SizeMB(bytes), 2) < 1024 ) ? Math.Round(SizeMB(bytes), 2) + "MB" : Math.Round(SizeGB(bytes), 2) + "GB";
        }
    }
}
