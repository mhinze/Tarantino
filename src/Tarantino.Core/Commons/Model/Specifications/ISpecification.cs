namespace Tarantino.Core.Commons.Model.Specifications
{
	public interface ISpecification<T>
	{
		bool IsSatisfiedBy(T objectToTestSatisfaction);
		bool IsNotSatisfiedBy(T objectToTestSatisfaction);
		ISpecification<T> And(ISpecification<T> specification);
		ISpecification<T> Or(ISpecification<T> specification);
		ISpecification<T> AndNot(ISpecification<T> specification);
		ISpecification<T> OrNot(ISpecification<T> specification);
	}
}