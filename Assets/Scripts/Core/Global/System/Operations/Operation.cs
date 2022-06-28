namespace Christ.Core
{
    public abstract class Operation
    {
        public float OpValue
        {
            get => _value;
            set => _value = value;
        }

        // Зарезервированное значение.
        public float Reserved
        {
            get => _reserved;
            set => _reserved = value;
        }

        protected float _value;
        protected float _reserved;

        public Operation(float value)
        {
            _value = value;
        }

        public virtual float Do(float secondOperator)
        {
            return secondOperator;
        }

        public virtual float Reversed(float secondOperator)
        {
            return _value;
        }
    }
}

