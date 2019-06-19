namespace SamDevs.Infrastructure.Validations
{
    public class NotSpecification:CompositeSpecification
    {
        private readonly ISpecification _innerSpecification; 

        public NotSpecification(ISpecification innerSpecification)
        {
            _innerSpecification = innerSpecification;
        }

        public override bool IsSatisfied()
        {
            return !_innerSpecification.IsSatisfied();
        }
    }
}
