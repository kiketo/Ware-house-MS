using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;
using System.IO;

namespace WHMS.Services
{
    public class DatabaseService : IDatabaseService
    {
        public string ExportToJson()
        {
            List<Address> addressList = new List<Address> { new Address {Text="Grafa" } };
            string address=JsonConvert.SerializeObject(addressList);
            File.WriteAllText("\\..\\file.json", address);

            return "Succesfully tranfered to JSON file!";
        }
    }
}
