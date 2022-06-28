namespace Christ.Core
{
    public class OperationSum : Operation 
    {
        public OperationSum(float value) : base(value)
        { }

        public override float Do(float secondOperator)
        {
            return _value + secondOperator;
        }

        public override float Reversed(float secondOperator)
        {
            return _value - secondOperator;
        }
    }
}

