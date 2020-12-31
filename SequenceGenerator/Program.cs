using Newtonsoft.Json;
using OpenMacroBoard.SDK;
using SequenceGenerator.Actions;
using StreamDeckSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonController.RegisterJsonAction(new Table());
            JsonController.RegisterJsonAction(new TextInput());
            JsonController.RegisterJsonAction(new RepeatingTable());
            JsonController.RegisterJsonAction(new LabelTransfer());
            JsonConvert.DefaultSettings = JsonSettings;

            if(args.Count() == 0)
            {
                /*Console.WriteLine(
                    @"No arguments.
Valid arguments: 
-elgato - enables Elgato mode (WIP) 
-roll - Rolls a set of dice with the format idx+y 
notes: this is dice notation and the only required numeric is x. 
-Character - creates a character with various properties. 
notes: This has additional arguments including it's own help section."); */

                string argHelp = "No Arguments. \n";
                argHelp += "Valid Arguments: \n" +
                    "-elgato - Enables Elgato mode.  Requires an Elgato device.\n" +
                    "-roll - Rolls a set of dice with the format idx+y \n" + 
                    "Notes: this is dice notation, and the only required variable is x.\n" +
                    "-character - creates a character with various properties. \n" + 
                    "notes: this has additional arguments.";

                Console.WriteLine(argHelp);

                Console.ReadLine();
                return;
            }

            if(args.Contains("-Create") || args.Contains("-create"))
            {
                Creation creation = new Creation();
                string directory = "";


                if(args.Contains("-sys"))
                {
                    //JsonController.RegisterJsonActionGroup(args.Split(' '))
                    List<string> Largs = new List<string>(args);
                    int sysArg = Largs.IndexOf("-sys") + 1; //get the next index.
                    if (Largs.Count <= sysArg)
                    {
                        Console.WriteLine("Creation system not mentioned (-sys argument is null) terminated.");

                    }
                    else
                    {
                        JsonController.RegisterJsonActionGroup(Largs[sysArg]);
                    }

                }
                else
                {
                    Console.WriteLine("Creations requires the use of a system.  A system is a set of actions that are applied to a creation.");
                    Console.WriteLine("Creations can be characters, worlds, history, or anything you can craft using values");
                    Console.WriteLine("Additional arguments: -sys = The system the creation is built on.");
                }

            }
            
            if(args.Contains("-Dummy"))
            {
                JsonController.CreateDummyFolder();
            }
            
            if (args.Contains("-elgato"))
            {
                KeyBitmap d20 = KeyBitmap.Create.FromFile("Elgato/d20.png");
                KeyBitmap d12 = KeyBitmap.Create.FromFile("Elgato/d12.png");
                KeyBitmap d10 = KeyBitmap.Create.FromFile("Elgato/d10.png");
                KeyBitmap d100 = KeyBitmap.Create.FromFile("Elgato/d100.png");

                using (IStreamDeckBoard deck = StreamDeck.OpenDevice())
                {
                    deck.SetBrightness(100);

                    if (deck.Keys.Count == 15) //normal elgato streamdeck.
                    {
                        deck.SetKeyBitmap(0, d20);
                        deck.SetKeyBitmap(1, d12);
                        deck.SetKeyBitmap(5, d10);
                        deck.SetKeyBitmap(6, d100);
                    }

                    if (deck.Keys.Count == 6) //mini
                    {

                    }

                    if (deck.Keys.Count == 32) //XL
                    {

                    }



                    Console.ReadLine();
                }
            }

            if(args.Contains("-SyncRun"))
            {
                JsonController.CreateDummyFolder();
                JsonController.RegisterJsonActionGroup("Dummy");
            }

            Console.WriteLine("Complete.  Press enter to close.");
            Console.ReadLine();
        }

        static JsonSerializerSettings JsonSettings()
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
                
            };
        }
    }
}
