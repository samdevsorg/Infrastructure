namespace SamDevs.InfrastructureCore.Validations
{
    public abstract class CompositeSpecification : ISpecification
    {
        public abstract bool IsSatisfied();
        public string Message { get; set; }
        //public T Entity { get; set; }

        public ISpecification And(ISpecification dest)
        { return new AndSpecification(this, dest); }

        public ISpecification Or(ISpecification dest)
        { return new OrSpecification(this, dest); }

        public ISpecification Xor(ISpecification dest)
        { return new XorSpecification(this, dest); }

        public ISpecification Not()
        { return new NotSpecification(this); }
    }
}
