using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegSearch
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Searching registry for StagePlayer...");
            string[] results = RegSearchAll("StagePlayer");
            Console.WriteLine("Search complete. Press enter to view results...");
            Console.ReadLine();
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("Press enter to delete found registries...");
            Console.ReadLine();
            RegNukeAll("StagePlayer");
            Console.WriteLine("Registries deleted.");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        //Calls RegSearch on all currently loaded registry hives.
        public static string[] RegSearchAll(string searchTerm)
        {
            RegSearchResults.Clear();

            RegSearchInternal(Registry.ClassesRoot, searchTerm.ToLower());
            RegSearchInternal(Registry.CurrentConfig, searchTerm.ToLower());
            RegSearchInternal(Registry.LocalMachine, searchTerm.ToLower());
            RegSearchInternal(Registry.PerformanceData, searchTerm.ToLower());
            RegSearchInternal(Registry.Users, searchTerm.ToLower());

            string[] output = RegSearchResults.ToArray();

            RegSearchResults.Clear();

            return output;
        }
        //Finds all refrences to a given search term in Key names, Value names, or the text of string values.
        public static string[] RegSearch(RegistryKey target, string searchTerm)
        {
            RegSearchResults.Clear();

            RegSearchInternal(target, searchTerm.ToLower());

            string[] output = RegSearchResults.ToArray();

            RegSearchResults.Clear();

            return output;
        }
        private static object RegSearchResultsLock = new object();
        private static List<string> RegSearchResults = new List<string>();
        private static void RegSearchInternal(RegistryKey target, string searchTerm)
        {
            if (target.Name.ToLower().Contains(searchTerm))
            {
                RegSearchResults.Add($"Key: {target.Name}");
            }

            try
            {
                string[] valueNames = target.GetValueNames();

                foreach (string valueName in valueNames)
                {
                    if (valueName.ToLower().Contains(searchTerm))
                    {
                        RegSearchResults.Add($"Value: {valueName}");
                        continue;
                    }

                    try
                    {
                        RegistryValueKind valueKind = target.GetValueKind(valueName);

                        if (valueKind is RegistryValueKind.ExpandString || valueKind is RegistryValueKind.ExpandString)
                        {
                            string value = (string)target.GetValue(valueName);

                            if (value.ToLower().Contains(searchTerm))
                            {
                                RegSearchResults.Add($"Value: {valueName}");
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }

            string[] subKeyNames = target.GetSubKeyNames();

            foreach (string subKeyName in subKeyNames)
            {
                try
                {
                    RegistryKey subKey = target.OpenSubKey(subKeyName);

                    RegSearchInternal(subKey, searchTerm);

                    subKey.Close();

                    subKey.Dispose();
                }
                catch
                {

                }
            }
        }

        //Calls RegNuke on all currently loaded registry hives.
        public static void RegNukeAll(string searchTerm)
        {
            RegNuke(Registry.ClassesRoot, searchTerm.ToLower());
            RegNuke(Registry.CurrentConfig, searchTerm.ToLower());
            RegNuke(Registry.LocalMachine, searchTerm.ToLower());
            RegNuke(Registry.PerformanceData, searchTerm.ToLower());
            RegNuke(Registry.Users, searchTerm.ToLower());
        }
        //Deletes all refrences to a given search term in Key names, Value names, or the text of string values.
        public static void RegNuke(RegistryKey target, string searchTerm)
        {
            try
            {
                string[] valueNames = target.GetValueNames();

                foreach (string valueName in valueNames)
                {
                    if (valueName.ToLower().Contains(searchTerm))
                    {
                        target.DeleteValue(valueName);
                        continue;
                    }

                    try
                    {
                        RegistryValueKind valueKind = target.GetValueKind(valueName);

                        if (valueKind is RegistryValueKind.ExpandString || valueKind is RegistryValueKind.ExpandString)
                        {
                            string value = (string)target.GetValue(valueName);

                            if (value.ToLower().Contains(searchTerm))
                            {
                                target.DeleteValue(valueName);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }

            string[] subKeyNames = target.GetSubKeyNames();

            foreach (string subKeyName in subKeyNames)
            {
                try
                {
                    if (subKeyName.ToLower().Contains(searchTerm))
                    {
                        target.DeleteSubKeyTree(subKeyName);
                    }
                    else
                    {
                        RegistryKey subKey = target.OpenSubKey(subKeyName);

                        RegNuke(subKey, searchTerm);

                        subKey.Close();

                        subKey.Dispose();
                    }
                }
                catch
                {

                }
            }
        }
    }
}
