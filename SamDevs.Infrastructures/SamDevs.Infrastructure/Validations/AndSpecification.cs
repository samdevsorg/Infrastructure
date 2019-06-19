namespace SamDevs.Infrastructure.Validations
{
    public class AndSpecification:CompositeSpecification
    {
        private readonly ISpecification _leftSpecification; 
        private readonly ISpecification _rightSpecification;

        public AndSpecification(ISpecification leftSpecification, ISpecification rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override bool IsSatisfied()
        {
            return _leftSpecification.IsSatisfied() && _rightSpecification.IsSatisfied();
        }
    }
}
