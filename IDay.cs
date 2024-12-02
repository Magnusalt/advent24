
public abstract class Day : IDay
{
    private readonly int _dayNbr;

    protected string[] Input { get; }
    public Day(int dayNbr)
    {
        Input = File.ReadAllLines($"input/day{dayNbr}.txt");
        _dayNbr = dayNbr;
    }

    public abstract long RunPart1();

    public abstract long RunPart2();

    public string GetResult()
    {
        return  $"""
                === Day {_dayNbr} ===
                Part 1: {RunPart1()}
                Part 2: {RunPart2()}
                """;
    }
}

public interface IDay
{
    long RunPart1();
    long RunPart2();
    string GetResult();
}