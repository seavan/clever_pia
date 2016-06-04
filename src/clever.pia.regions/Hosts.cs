using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace clever.pia.regions
{
    public class Hosts
    {
        public const string Path = @"C:\Windows\System32\drivers\etc\hosts";
        public static Dictionary<string, string> Read()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Trim();
                    if (String.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }
                    var parts = line.Split(' ', '\t');
                    if (parts.Length != 2)
                    {
                        continue;
                    }
                    results[parts[1]] = parts[0];
                }
            }
            return results;
        }

        public static void Merge(Dictionary<string, string> merge)
        {
            var src = Hosts.Read();
            using (StreamWriter w = File.AppendText(Hosts.Path))
            {
                foreach (var i in merge)
                {
                    if (src.ContainsKey(i.Key))
                    {
                        if (i.Value != src[i.Key])
                        {
                            Console.WriteLine("Already contains {0}: {1}. Please remove them manually if needed", i.Key,
                                i.Value);
                        }
                        continue;
                    }
                    Console.WriteLine("Appending {0} {1} to hosts file", i.Value, i.Key);
                    w.WriteLine("{0} {1}", i.Value, i.Key);
                }
            }
        }
    }
}
