namespace Tarantino.Core.Commons.Model.Specifications
{
	public abstract class BaseSpecification<T> : ISpecification<T>
	{
		public abstract bool IsSatisfiedBy(T objectToTestSatisfaction);

		public virtual bool IsNotSatisfiedBy(T objectToTestSatisfaction)
		{
			return !IsSatisfiedBy(objectToTestSatisfaction);
		}

		public ISpecification<T> And(ISpecification<T> specification)
		{
			return new AndSpecification<T>(this, specification);
		}

		public ISpecification<T> Or(ISpecification<T> specification)
		{
			return new OrSpecification<T>(this, specification);
		}

		public ISpecification<T> AndNot(ISpecification<T> specification)
		{
			return new AndSpecification<T>(this, new NotSpecification<T>(specification));
		}

		public ISpecification<T> OrNot(ISpecification<T> specification)
		{
			return new OrSpecification<T>(this, new NotSpecification<T>(specification));
		}

		public static BaseSpecification<T> operator *(BaseSpecification<T> firstSpec, BaseSpecification<T> secondSpec)
		{
			return new AndSpecification<T>(firstSpec, secondSpec);
		}

		public static BaseSpecification<T> operator +(BaseSpecification<T> firstSpec, BaseSpecification<T> secondSpec)
		{
			return new OrSpecification<T>(firstSpec, secondSpec);
		}

		public static BaseSpecification<T> operator !(BaseSpecification<T> otherSpec)
		{
			return new NotSpecification<T>(otherSpec);
		}
	}
}