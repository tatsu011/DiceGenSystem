using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class RepeatingTable : JsonAction
    {
        public string actionType = "RepeatingTable";

        public string TargetTable;

        public Roll roll;

        int Count;
        public override string ActionType()
        {
            return actionType;
        }

        public override void ApplyResult(ref Creation creation)
        {
            // load the target table.


            for(int i = Count; i > 0; i--)
            {

            }

        }

        public override object CreateDummyAction()
        {
            RepeatingTable rt = new RepeatingTable();
            rt.roll = new Roll();
            roll.Die = Dice.d3;


            return rt;
        }

        public override bool Validate()
        {
            return true; //for now.
        }
    }
}
