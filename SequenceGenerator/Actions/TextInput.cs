using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class TextInput : JsonAction
    {
        public string actionType = "TextInput";
        
        public string Prompt;
        public string Field;

        public override string ActionType()
        {
            return actionType;
        }

        public override void ApplyResult(ref Creation creation)
        {
            Console.WriteLine(Prompt);
            string result = Console.ReadLine();
            creation.SetValue(Field, result);
        }

        public override object CreateDummyAction()
        {
            TextInput ti = new TextInput
            {
                Prompt = "Enter a name",
                Field = "Name"
            };
            return ti;
        }

        public override bool Validate() //This is actually really simple.
        {
            return Prompt != null && Field != null;
        }
    }
}
