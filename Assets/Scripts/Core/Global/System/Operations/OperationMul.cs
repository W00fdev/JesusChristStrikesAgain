namespace Christ.Core
{
    public class OperationMul : Operation
    {
        public OperationMul(float value) : base(value)
        { }

        public override float Do(float secondOperator)
        {
            return _value * secondOperator;
        }

        public override float Reversed(float secondOperator)
        {
            return _value / secondOperator;
        }
    }
}