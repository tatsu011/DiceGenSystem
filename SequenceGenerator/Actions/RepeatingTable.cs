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
        public override string ActionType()
        {
            return actionType;
        }

        public override void ApplyResult(ref Creation creation)
        {
            // load the target table.
        }

        public override object CreateDummyAction()
        {
            RepeatingTable rt = new RepeatingTable();



            return rt;
        }

        public override bool Validate()
        {
            return true; //for now.
        }
    }
}
