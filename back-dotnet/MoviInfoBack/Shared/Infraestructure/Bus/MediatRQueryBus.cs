using MediatR;
using Shared.Domain.Bus.Query;

namespace Shared.Infraestructure.Bus
{
    public class MediatRQueryBus (Mediator mediator) : QueryBus
    {
      private readonly Mediator _mediator = mediator;

      public Task<TResponse> Ask<TResponse>(Query request)
      {
        return _mediator.Send<TResponse>((IRequest<TResponse>) request);
      }
    }
}