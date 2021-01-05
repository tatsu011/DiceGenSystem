using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator.Actions
{
    class NumericInput : JsonAction
    {
        public string TargetValue;
        public string Prompt;
        public string Explanation;


        public override void ApplyResult(ref Creation creation)
        {
            int value;
            string temp = "";
            while(!int.TryParse(temp, out value))
            {
                Console.WriteLine(Prompt);
                temp = Console.ReadLine();
            }


            creation.SetValue(TargetValue, value);
            if (Explanation != null)
                creation.AddTableResult(Explanation);
        }

        public override object CreateDummyAction()
        {
            NumericInput rt = new NumericInput()
            {
                TargetValue = "Age",
                Prompt = "How old are you?",
                Explanation = "You are {0} years old"
            };
            return rt;
        }

    }
}
