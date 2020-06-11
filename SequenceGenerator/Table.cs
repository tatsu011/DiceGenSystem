using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    class Table
    {

        public string ModifierCheck = "";

        public Roll dicecheck;

        public Result[] results;

        public string FollowupTable;
        
        public bool Validate()
        {
            int MinLength = dicecheck.GetRangeDifference();

            int length = 0;
            foreach(Result r in results)
            {
                length += r.Weight;
            }

            return MinLength <= length;
        }

        Result ToResult(int id)
        {
            int i = 0;
            int r = 0;
            while(i < id)
            {
                i += results[r].Weight;
                r++;
            }

            return results[r];
        }

        public void RollResult(ref Creation creation)
        {
            if(ModifierCheck != "")
            {
                int bonus;
                creation.GetValue(ModifierCheck, out bonus);
                dicecheck.bonus = bonus;
            }
            Result result = ToResult(dicecheck.PerformRoll());

            creation.AddTableResult(result.Description);

            //apply result to creation (if expected.)
            if(result.TargetValue != string.Empty)
            {
                creation.GetValue(result.TargetValue, out int value);
                switch (result.targetOperation)
                {
                    case Operation.ADD:
                        creation.SetValue(result.TargetValue, result.Value + value);
                        break;
                    case Operation.SUB:
                        creation.SetValue(result.TargetValue, value - result.Value);
                        break;
                    case Operation.MULTI:
                        creation.SetValue(result.TargetValue, value * result.Value);
                        break;
                    case Operation.DIV:
                        creation.SetValue(result.TargetValue, value / result.Value);
                        break;
                    case Operation.SET:
                    default:
                        creation.SetValue(result.TargetValue, result.Value); //default operation is SET.
                        break;
                }
            }

            if(result.TargetTable != string.Empty)
            {

            }
        }

        public Creation RollTables()
        {
            Creation creation = new Creation();



            return creation;
        }

    }

    public enum Dice
    {
        coin = 2, d3,d4,d5,d6,d8 = 8,d10 = 10,d12 = 12,d20 = 20,d100 = 100
    }
}
