using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Athletes.Queries;

public record GetRemovedAthletes() : IRequest<Result<List<Guid>>>;

public class GetRemovedAthletesHandler : IRequestHandler<GetRemovedAthletes, Result<List<Guid>>>
{
    private readonly IRedisService _redisService;

    public GetRemovedAthletesHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Result<List<Guid>>> Handle(GetRemovedAthletes request, CancellationToken cancellationToken)
    {
        var removedAthletes = await  _redisService.GetRemovedAthletes();
        
        return Result<List<Guid>>.Ok(removedAthletes);
    }
}