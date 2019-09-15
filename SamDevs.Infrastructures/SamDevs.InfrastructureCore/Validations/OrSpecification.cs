namespace SamDevs.InfrastructureCore.Validations
{
    public class OrSpecification:CompositeSpecification
    {
        private readonly ISpecification _leftSpecification; 
        private readonly ISpecification _rightSpecification;

        public OrSpecification(ISpecification leftSpecification, ISpecification rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override bool IsSatisfied()
        {
            return _leftSpecification.IsSatisfied() || _rightSpecification.IsSatisfied();
        }
    }
}
