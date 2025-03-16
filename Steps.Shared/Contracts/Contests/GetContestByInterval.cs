namespace Steps.Shared.Contracts.Contests;

public class GetContestByInterval
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    
    public string GetQuery()
    {
        const string startName = nameof(Start);
        const string endName = nameof(End);
        return $"?{startName}={ToQuery(Start)}&{endName}={ToQuery(End)}";
    }

    private string ToQuery(DateTime datetime)
    {
        return datetime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
}