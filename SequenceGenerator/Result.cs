namespace SequenceGenerator
{
    struct  Result
    {
        public int Weight;
        public string Description;
        public string TargetValue;
        public Operation targetOperation;
        public string TargetTable;
        public int Value;
        public string Condition; // "having anything start with a :: forces it to read settings instead of creation.
        public int ConditionTarget;
        public int NewResult;
    }

    enum Operation
    {
        ADD, SET, SUB, DIV, MULTI, REMOVE
    }
}
