using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class LabelTransfer : JsonAction
    {
        public LabelTransfer()
        {
            ActionType = "LblTransfer";
        }
            

        public override object CreateDummyAction()
        {
            LabelTransfer lt = new LabelTransfer();
            lt.NextAction = "DummyTextInput";
            return lt;
        }
    }
}
