using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Castle.Components.DictionaryAdapter.Xml;

namespace SequenceGenerator
{
    public static class JsonController
    {
        static Dictionary<string, JsonAction> ActionCollection = new Dictionary<string, JsonAction>();

        public static void RegisterJsonAction(JsonAction action)
        {
            ActionCollection.Add(action.ActionType(), action);
        }

        public static void RegisterJsonActionGroup(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {

            }
        }

        public static void CreateDummyFolder()
        {
            Console.WriteLine($"checking {AppContext.BaseDirectory}\\dummy");

            if (Directory.Exists($"{AppContext.BaseDirectory}\\dummy"))
            {
                Console.WriteLine($"Dummy folder exists at {AppContext.BaseDirectory}\\dummy , please remove dummy folder.");
                return;
            }

            Directory.CreateDirectory($"{ AppContext.BaseDirectory}\\dummy\\");

            foreach(var kvp in ActionCollection)
            {
                string Filename = $"Dummy{kvp.Key}.json";

                JsonAction action = (JsonAction)kvp.Value.CreateDummyAction();
                string contents = JsonConvert.SerializeObject(action);
                File.WriteAllText($"{ AppContext.BaseDirectory}\\dummy\\{Filename}", contents);
            }

            

        }
    }
}
