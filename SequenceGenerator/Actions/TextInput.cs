﻿using System;

namespace SequenceGenerator.Actions
{
    class TextInput : JsonAction
    {
        
        public string Prompt;
        public string Field;

        public TextInput()
        {
            ActionType = "TextInput";
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
                Field = "Name",
                NextAction = "DummyRepeatingTable"
            };
            return ti;
        }

        public override bool Validate() //This is actually really simple.
        {
            return Prompt != null && Field != null;
        }
    }
}
