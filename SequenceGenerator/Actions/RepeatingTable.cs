using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class RepeatingTable : JsonAction
    {

        public string TargetTable;

        public Roll roll;

        int Count;

        public string NextTable;

        public string Explanation;

        public RepeatingTable()
        {
            ActionType = "RepeatingTable";
        }

        public override void ApplyResult(ref Creation creation)
        {
            // load the target table.
            
            JsonAction targetTable = JsonController.GetJsonAction(TargetTable);
            Count = roll.PerformRoll();
            creation.AddTableResult(string.Format(Explanation, Count));
            for(int i = Count; i > 0; i--)
            {
                targetTable.ApplyResult(ref creation);
                if (creation.HasValue($"{TargetTable}_Adj"))
                {
                    int adjustment;
                    creation.GetValue($"{TargetTable}_Adj", out adjustment);
                    if ( adjustment != 0)
                    {
                        Count += adjustment;
                        creation.SetValue($"{TargetTable}_Adj", 0); //remove the adjustment from the creation.
                    }
                }
                if(creation.HasValue($"{TargetTable}_Stop"))
                {
                    //this signal says that 'this creation step is over.  
                    creation.SetValue($"{TargetTable}_Stop", 0); //remove the value from the creation.
                    break;
                }
            }

        }

        public override object CreateDummyAction()
        {
            RepeatingTable rt = new RepeatingTable()
            {
                roll = new Roll
                {
                    Die = Dice.d3
                },
                TargetTable = "DummyRollTable",
                Explanation = "A thing happened {0} times",
                NextTable = "DummyRollTable"
            };

            return rt;
        }

        public override bool Validate()
        {
            return true; //for now.
        }
    }
}
