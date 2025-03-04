using Steps.Domain.Base;

namespace Steps.Domain.Entities;

// групповой блок с участниками 
public class GroupBlock : Entity
{
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<PreBlock> PreBlocks { get; set; } = new();
    public List<FinalBlock> FinalBlocks { get; set; } = new();
}

// блок спортсменов предварительный
public class PreBlock : BlockBase<ConfirmationAthleteBlock, PreBlock>
{
    public override List<ConfirmationAthleteBlock> AthleteBlocks { get; set; } = new();
}

// блок спортсменов финальный
public class FinalBlock : BlockBase<FinalAthleteBlock, FinalBlock>
{
    public override List<FinalAthleteBlock> AthleteBlocks { get; set; } = new();
}

// связь для финального списка без подтверждения
public class FinalAthleteBlock : Entity, IAthleteBlock<FinalBlock>
{
    public int SequenceNumber { get; set; }
    public Guid BlockId { get; set; }
    public FinalBlock Block { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
}

// подтверждение атлета
public class ConfirmationAthleteBlock : Entity, IAthleteBlock<PreBlock>
{
    public int SequenceNumber { get; set; }
    public Guid BlockId { get; set; }
    public PreBlock Block { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;

    public bool IsConfirmed { get; set; }
}

// блок (блок спорсменов N выступающих в одно и то же время)
public abstract class BlockBase<T, TBlock> : Entity where T : IAthleteBlock<TBlock>
{
    public Guid GroupBlockId { get; set; }
    public GroupBlock GroupBlock { get; set; } = null!;

    public DateTime ExitTime { get; set; }

    public abstract List<T> AthleteBlocks { get; set; }
}

public interface IAthleteBlock<TBlock> : IHaveId
{
    int SequenceNumber { get; set; }
    Guid BlockId { get; set; }
    TBlock Block { get; set; }

    Guid AthleteId { get; set; }
    Athlete Athlete { get; set; }
}