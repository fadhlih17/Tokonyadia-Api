namespace Tokonyadia_Api.Repositories;

public interface Ipersistance
{
    Task SaveChangesAsync();
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
}