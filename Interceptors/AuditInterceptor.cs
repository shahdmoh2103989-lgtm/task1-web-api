using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace task1_web_api.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILogger<AuditInterceptor> _logger;

        public AuditInterceptor(ILogger<AuditInterceptor> logger)
        {
            _logger = logger;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "EF Core is saving changes to the database.");

            return base.SavingChangesAsync(
                eventData, result, cancellationToken);
        }
    }
}