using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class Table : JsonAction
    {
        public string actionType = "RollTable";

        public string ModifierCheck = "";

        public Roll dicecheck;

        public Result[] results;
        
        public override bool Validate()
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

        public override void ApplyResult(ref Creation creation)
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

        public override string ActionType()
        {
            return actionType;
        }

        public override object CreateDummyAction()
        {
            Table table = new Table
            {
                dicecheck = new Roll
                {
                    Die = Dice.d20
                },
                ModifierCheck = "CultureMod",
                results = new Result[5]
            };

            table.results[0] = new Result
            {
                Weight = 1,
                Description = "You had a bad fall.  Add an injury.",
                TargetValue = "injury",
                targetOperation = Operation.ADD,
                TargetTable = "Injury",
                Value = 1
            };

            table.results[1] = new Result
            {
                Weight = 6,
                Description = "You performed poorly.",
                TargetTable = "Unlucky"
            };

            table.results[2] = new Result
            {
                Weight = 6,
                Description = "This was an average performance."
            };

            table.results[3] = new Result()
            {
                Weight = 6,
                Description = "This is an above average performance",
                TargetTable = "Boon"
            };

            table.results[4] = new Result()
            {
                Weight = 1,
                Description = "This is a glorious performance",
                TargetTable = "Boon",
                TargetValue = "Boost",
                targetOperation = Operation.ADD,
                Value = 1
            };

            return table;
        }
    }


}
