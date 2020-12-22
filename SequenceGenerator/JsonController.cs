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
        static Dictionary<string, JsonAction> ActionCollection = new Dictionary<string, JsonAction>(); //Registers type.
        static Dictionary<string, JsonAction> SequenceCollection = new Dictionary<string, JsonAction>(); //registers tables.


        public static void RegisterJsonAction(JsonAction action)
        {
            ActionCollection.Add(action.ActionType(), action);
        }

        public static void RegisterJsonActionGroup(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {
                foreach(string file in Directory.GetFiles(folderPath))
                {
                    Console.WriteLine(file);
                    JsonAction target = JsonConvert.DeserializeObject<JsonAction>(File.ReadAllText(file));



                    string ActionName = file.Substring(file.LastIndexOf('\\') + 1 , file.LastIndexOf('.') - file.LastIndexOf('\\') - 1); 
                    SequenceCollection.Add(ActionName, target);

                    Console.WriteLine($"Loaded {ActionName} as {ActionCollection[ActionName].ActionType()}");

                }
            } 
            else
            {
                Console.WriteLine($"Error: Folder '{folderPath}' not found.");
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

        /// <summary>
        /// Gets the json action without the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static JsonAction GetJsonAction(string name)
        {
            if(SequenceCollection.ContainsKey(name))
            {
                return SequenceCollection[name];
            }
            else
            {
                Console.WriteLine($"WARNING: table {name} not found.");
                return null;
            }
        }
    }
}
