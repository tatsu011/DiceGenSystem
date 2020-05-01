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
            //a few options for args.

            //-G "Name": generates a new table with the given name. additional parameters- -d num

            //-C "Name": generates a new character with the current table set.

            //-M count: generate a number of characters, each interwoven into other characters. -- will be done later.

            //Lets roll some dice to start.
            Roll roll = new Roll();

            roll.Die = Dice.d6;
            Console.WriteLine($"Now rolling {roll}");
            Console.WriteLine(roll.PerformRoll());
            Console.WriteLine($"Minimum roll:{roll.GetMinValue()} Maximum roll:{roll.GetMaxValue()} Range: {roll.GetRangeDifference()}");

            roll.Die = Dice.d10;
            roll.Count = 2;
            Console.WriteLine($"Now rolling {roll}");
            Console.WriteLine(roll.PerformRoll());
            Console.WriteLine($"Minimum roll:{roll.GetMinValue()} Maximum roll:{roll.GetMaxValue()} Range: {roll.GetRangeDifference()}");


            Console.ReadLine();
        }
    }
}
