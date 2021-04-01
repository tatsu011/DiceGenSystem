using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace SequenceGenerator
{
    public static class JsonController
    {
        static Dictionary<string, JsonAction> ActionCollection = new Dictionary<string, JsonAction>(); //Registers type.
        static Dictionary<string, JsonAction> SequenceCollection = new Dictionary<string, JsonAction>(); //registers tables.


        public static void RegisterJsonAction(JsonAction action)
        {
            ActionCollection.Add(action.ActionType, action);
        }

        public static void RegisterJsonActionGroup(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {
                foreach(string file in Directory.GetFiles(folderPath))
                {
                    Console.WriteLine(file);

                    string ActionName = file.Substring(file.LastIndexOf('\\') + 1 , file.LastIndexOf('.') - file.LastIndexOf('\\') - 1);

                    if(ActionName.Equals("settings"))
                    {
                        Console.WriteLine($"This is a settings file.  Loading it as such.");
                        SequenceSettings.ActivateSettings(JsonConvert.DeserializeObject<SequenceSettings>(File.ReadAllText(file)));

                        continue;
                    }
                    
                    JsonAction target = JsonConvert.DeserializeObject<JsonAction>(File.ReadAllText(file));

                    Console.WriteLine($"found {ActionName}, a type of {target.ActionType}");

                    SequenceCollection.Add(ActionName, target);

                    Console.WriteLine($"Loaded {ActionName} as {SequenceCollection[ActionName].ActionType}");

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

            SequenceSettings DummySettings = new SequenceSettings();
            DummySettings.GenSettings.Add("Test1", 22);
            DummySettings.GenSettings.Add("Test2", 96);
            File.WriteAllText($"{ AppContext.BaseDirectory}\\dummy\\settings.json", JsonConvert.SerializeObject(DummySettings));

            string fName = "Start.json";
            Actions.LabelTransfer labelTransfer = new Actions.LabelTransfer();
            labelTransfer.NextAction = "DummyLblTransfer";
            string Contents = JsonConvert.SerializeObject(labelTransfer);
            File.WriteAllText($"{ AppContext.BaseDirectory}\\dummy\\{fName}", Contents);
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

        internal static void ValidateTables()
        {
            int failedTables = 0;
            foreach(var action in SequenceCollection)
            {
                if (action.Value.Validate())
                {
                    Console.WriteLine($"{action.Key} has passed inspection.");
                }
                else
                {
                    Console.WriteLine($"WARNING: {action.Key} DID NOT PASS INSPECTION, check {action.Key}.json!");
                    failedTables++;
                }
            }
            if(failedTables != 0)
            {
                Console.WriteLine($"We had {failedTables} failed actions.  Please check log.");
            }
        }

        internal static void RunSystem()
        {
            if(!SequenceCollection.ContainsKey("Start"))
            {
                Console.WriteLine("System contains no Start.json.  No entry point defined (run -Dummy to get a basic system");
            }

            Creation creation = new Creation();
            JsonAction startingAction = SequenceCollection["Start"];
            startingAction.ApplyResult(ref creation);
            JsonAction NextAction = SequenceCollection[startingAction.NextAction];


            while (NextAction.NextAction != null)
            {
                NextAction.ApplyResult(ref creation);
                NextAction = SequenceCollection[NextAction.NextAction];
            }
            NextAction.ApplyResult(ref creation);

            string CreationData = JsonConvert.SerializeObject(creation);
            string filename;

            if(creation.HasValue("Name"))
            {
                creation.GetValue("Name", out filename);
            }
            else
            {
                filename = "RandomCreation";
            }
            Console.WriteLine("Saving creation");
            File.WriteAllText($"{filename}.json", CreationData);

        }
    }
}
