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

            //-D Just roll some dice.  Arguments: idx+b

            //Lets roll some dice to start.
            int Result;

            Roll roll = new Roll();

            while (true)
            {


                roll.Die = Dice.d6;
                Console.WriteLine($"Now rolling {roll}");
                Result = roll.PerformRoll();
                Console.WriteLine(Result);
                Console.WriteLine($"Minimum roll:{roll.GetMinValue()} Maximum roll:{roll.GetMaxValue()} Range: {roll.GetRangeDifference()}");

                roll.Die = Dice.d10;
                roll.Count = 2;
                Console.WriteLine($"Now rolling {roll}");
                Console.WriteLine(roll.PerformRoll());
                Console.WriteLine($"Minimum roll:{roll.GetMinValue()} Maximum roll:{roll.GetMaxValue()} Range: {roll.GetRangeDifference()}");

                roll.Die = Dice.d20;
                roll.Count = null;
                roll.bonus = Result;
                Console.WriteLine($"Now rolling {roll} which has a bonus of {Result}");
                Console.WriteLine(roll.PerformRoll());
                Console.WriteLine($"Minimum roll:{roll.GetMinValue()} Maximum roll:{roll.GetMaxValue()} Range: {roll.GetRangeDifference()}");



                Console.ReadLine();
                roll = new Roll(); //reset the dice.
                Console.Clear();
            }
        }
    }
}
