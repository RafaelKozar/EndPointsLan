namespace EndPoints.Core
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}