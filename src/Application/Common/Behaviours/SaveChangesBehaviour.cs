using VetCheckup.Application.Services.Persistence;

namespace VetCheckup.Application.Common.Behaviours;

public class SaveChangesBehaviour<TRequest, TResponse>(IApplicationDbContext context)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (typeof(TResponse) == typeof(Unit))
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        return response;
    }
}
