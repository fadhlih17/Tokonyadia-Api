using Microsoft.EntityFrameworkCore;

namespace Tokonyadia_Api.Repositories;

public class DbPersistance : Ipersistance
{
    private AppDbContext _context;

    public DbPersistance(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        var result = await strategy.ExecuteAsync(async () =>
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await func();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

        return result;
    }
}