using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks.Base;

/// <summary>
/// Блок спортсменов N выступающих в одно и то же время
/// </summary>
/// <typeparam  name="TAthleteBlock">Тип блока FinalAthleteBlock или ConfirmationAthleteBlock</typeparam >
/// <typeparam name="TSubGroup">FinalSubGroup или PreSubGroup</typeparam>
public abstract class BlockSubGroup<TAthleteBlock, TSubGroup> : Entity where TAthleteBlock : IAthleteSubGroup<TSubGroup>
{
    public Guid GroupBlockId { get; set; }
    public GroupBlock GroupBlock { get; set; } = null!;

    public DateTime ExitTime { get; set; }

    public abstract List<TAthleteBlock> AthleteBlocks { get; set; }
}