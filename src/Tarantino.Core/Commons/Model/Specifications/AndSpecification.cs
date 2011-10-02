namespace Tarantino.Core.Commons.Model.Specifications
{
	public class AndSpecification<T> : BaseSpecification<T>
	{
		private readonly ISpecification<T> _leftSideSpec;
		private readonly ISpecification<T> _rightSideSpec;

		public AndSpecification(ISpecification<T> leftSideSpec, ISpecification<T> rightSideSpec)
		{
			_leftSideSpec = leftSideSpec;
			_rightSideSpec = rightSideSpec;
		}

		public override bool IsSatisfiedBy(T objectToTestSatisfaction)
		{
			return _leftSideSpec.IsSatisfiedBy(objectToTestSatisfaction) && _rightSideSpec.IsSatisfiedBy(objectToTestSatisfaction);
		}
	}
}