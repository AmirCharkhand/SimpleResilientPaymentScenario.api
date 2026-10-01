using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleResilientPaymentScenario.api.Domain.Exceptions;

namespace SimpleResilientPaymentScenario.api.ExceptionHandling;

public sealed class PaymentExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problem = exception switch
        {
            PaymentConflictException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Payment conflict",
                Detail = exception.Message
            },
            PaymentInProgressException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Payment in progress",
                Detail = exception.Message
            },
            _ => null
        };

        if (problem is null)
            return false;

        httpContext.Response.StatusCode = problem.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}