using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.GroupBlocks;

public interface IGroupBlocksService
{
    /// <summary>
    /// Получение команд из заявок, поданных на Contest
    /// </summary>
    /// <returns></returns>
    Task<Result<List<TeamViewModel>>> GetTeamsForCreateGroupBlocks(Guid contestId);
    
    /// <summary>
    /// Создать групповые блоки с учетом порядка команд из заявок 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Result> CreateByTeams(CreateGroupBlockViewModel model);
    
    /// <summary>
    /// Получить групповые блоки по id Contest`a
    /// </summary>
    /// <param name="contestId"></param>
    /// <returns></returns>
    Task<Result<List<GroupBlockViewModel>>> GetByContestId(Guid contestId);
    
    /// <summary>
    /// Получить блок по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<GroupBlockViewModel>> GetById(Guid id);
    
    /// <summary>
    /// Удаление всех блоков по ContestId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result> DeleteByContestId(Guid id);
}


