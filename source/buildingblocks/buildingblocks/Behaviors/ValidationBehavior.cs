

using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace buildingblocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
       var validationResults = await Task.WhenAll(validators.Select(x => 
        x.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .Where(x => x.Errors.Any())
            .SelectMany(e => e.Errors)
            .ToList();

        if(errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}

