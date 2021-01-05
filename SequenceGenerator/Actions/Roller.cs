using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class Roller : JsonAction
    {
        public string TargetValue;
        public Roll roll;


        public override object CreateDummyAction()
        {
            Roller rt = new Roller()
            {
                roll = new Roll
                {
                    Die = Dice.d6,
                    Count = 2,
                    bonus = 16,
                    
                },
                TargetValue = "Age"
            };
            return rt;
        }

    }
}
