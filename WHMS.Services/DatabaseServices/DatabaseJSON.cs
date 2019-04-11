using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WHMS.Services.DatabaseServices
{
    public class DatabaseJSON<T>
    {
        public List<T> Read(string jPath, string jName)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($"{jPath}\\{jName}"));
        }

        public void Write(List<T> list, string jPath, string jName)
        {
            File.WriteAllText($"{jPath}\\{jName}", JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
