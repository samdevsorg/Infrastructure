namespace SamDevs.InfrastructureCore.Validations
{
    public class XorSpecification:CompositeSpecification
    {
        private readonly ISpecification _leftSpecification; 
        private readonly ISpecification _rightSpecification;

        public XorSpecification(ISpecification leftSpecification, ISpecification rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override bool IsSatisfied()
        {
            return _leftSpecification.IsSatisfied() && !_rightSpecification.IsSatisfied()
                || !_leftSpecification.IsSatisfied() && _rightSpecification.IsSatisfied();
        }
    }
}
