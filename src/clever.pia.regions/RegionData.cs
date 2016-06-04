using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace clever.pia.regions
{
    public static class RegionData
    {
        public const string Path = @"C:\Program Files\pia_manager\data\region_data.txt";

        public static void Read()
        {
            var data = JObject.Parse(File.ReadAllText(Path));
            foreach (var i in data)
            {
                Console.WriteLine(i.Key);
            }
        }

        public static void Exclude(params string[] countries)
        {
            var text = File.ReadAllText(Path);
            var data = JObject.Parse(text);
            foreach (var c in countries)
            {
                data.Remove(c);
                var auto = data["info"]["auto_regions"];
                var item = auto.Children().FirstOrDefault(s => s.ToString() == c);
                if (item != null)
                {
                    item.Remove();
                }
            }
            using (StreamWriter file = File.CreateText(Path))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    data.WriteTo(writer);
                }
            }
        }
    }
}
