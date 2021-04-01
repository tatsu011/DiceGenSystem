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
    }

    enum Operation
    {
        ADD, SET, SUB, DIV, MULTI, REMOVE
    }
}
