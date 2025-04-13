using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;

namespace StarcraftOrganizer.Services
{
    public abstract class BaseService
    {
        protected readonly IDbContextFactory<DataContext> ContextFactory;

        protected BaseService(IDbContextFactory<DataContext> factory)
        {
            ContextFactory = factory;
        }

        protected async Task<DataContext> CreateContextAsync() => await ContextFactory.CreateDbContextAsync();
    }
}
