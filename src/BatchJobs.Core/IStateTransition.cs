namespace BatchJobs.Core
{
    public interface IStateTransition<T>
    {
        bool IsValid(T batch);
        void Execute(T batch);
    }
}