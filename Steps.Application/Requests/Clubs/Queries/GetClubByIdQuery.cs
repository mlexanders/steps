using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Clubs.Commands;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Queries;

public record GetClubByIdQuery(Guid Id) : IRequest<Result<ClubViewModel>>;

public class GetClubByIdQueryHandler : IRequestHandler<GetClubByIdQuery, Result<ClubViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClubByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ClubViewModel>> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Club>();
        var club = await repository.FindAsync(request.Id) ?? throw new AppNotFoundException("клуб не найден");
        
        var mapped = _mapper.Map<ClubViewModel>(club);
        
        return Result<ClubViewModel>.Ok(mapped);
    }
}