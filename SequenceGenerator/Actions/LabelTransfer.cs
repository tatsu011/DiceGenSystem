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
