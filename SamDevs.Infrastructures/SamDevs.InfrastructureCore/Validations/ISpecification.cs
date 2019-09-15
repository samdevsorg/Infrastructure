namespace SamDevs.InfrastructureCore.Validations
{
    public interface ISpecification
    {
        bool IsSatisfied();
        string Message { get; set; }

        //T Entity { get; set; }

        ISpecification And(ISpecification dest);
        ISpecification Or(ISpecification dest);
        ISpecification Xor(ISpecification dest);
        ISpecification Not();
    }
}
