using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Magicdoes.Districts.Tests.Helper
{
    public class TestConfigHelper
    {
        public static AmapSettting LoadConfig(string name)
        {
            var config = new AmapSettting();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), name + ".json");
            if (File.Exists(filePath))
            {
                config = JsonConvert.DeserializeObject<AmapSettting>(File.ReadAllText(filePath));
            }
            else
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(config), Encoding.UTF8);
            }
            return config;
        }
    }
}
