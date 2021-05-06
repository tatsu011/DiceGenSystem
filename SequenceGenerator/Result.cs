namespace SequenceGenerator
{
    struct  Result
    {
        public int Weight;
        public string Description;
        public ValueOperation[] Items;
        public string TargetTable;
        public string Condition; // having anything start with a :: forces it to read settings instead of creation.
        public int ConditionTarget;
        public int NewResult;
    }

    struct ValueOperation
    {
        public string Target;
        public Operation Action;
        public int Value;
    }

    enum Operation
    {
        ADD, SET, SUB, DIV, MULTI, REMOVE
    }
}
