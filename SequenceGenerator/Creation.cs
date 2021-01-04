using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    public class Creation
    {

        public Dictionary<string, int> IntValues;
        
        public Dictionary<string, string> StrValues;

        public List<string> TableRolls;

        public Creation()
        {
            IntValues = new Dictionary<string, int>();
            StrValues = new Dictionary<string, string>();
            TableRolls = new List<string>();
        }


        public bool SetValue(string target, int value)
        {
            if (IntValues.ContainsKey(target))
            {
                if(value == 0)
                {
                    IntValues.Remove(target);
                }

                IntValues[target] = value;
                return true;
            }
            IntValues.Add(target, value);
            return false;
        }

        public bool SetValue(string target, string value)
        {
            if (StrValues.ContainsKey(target))
            {
                StrValues[target] = value;
                return true;
            }
            StrValues.Add(target, value);
            return false;
        }

        public bool HasValue(string target)
        {
            return IntValues.ContainsKey(target) || StrValues.ContainsKey(target);
        }
        public void GetValue(string target, out int Value)
        {
            if (IntValues.ContainsKey(target))
            {
                Value = IntValues[target];
                return;
            }
            Value = 0;
        }

        public void GetValue(string target, out string value)
        {
            if (StrValues.ContainsKey(target))
            {
                value = StrValues[target];
                return;
            }
            value = "";
        }



        public void AddTableResult(string result)
        {
            Console.WriteLine(result);
            TableRolls.Add(result);
        }

    }
}
