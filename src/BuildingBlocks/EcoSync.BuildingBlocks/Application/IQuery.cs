using MediatR;

namespace EcoSync.BuildingBlocks.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
