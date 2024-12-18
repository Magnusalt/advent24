
public abstract class Day<TP1, TP2> : IDay<TP1, TP2>
{
    private readonly int _dayNbr;

    protected string[] Input { get; }
    public Day(int dayNbr)
    {
        Input = File.ReadAllLines($"input/day{dayNbr}.txt");
        _dayNbr = dayNbr;
    }

    public CharMatrix CharMatrix()
    {
        return new CharMatrix(Input);
    }

    public abstract TP1 RunPart1();

    public abstract TP2 RunPart2();

    public string GetResult()
    {
        return $"""
                === Day {_dayNbr} ===
                Part 1: {RunPart1()}
                Part 2: {RunPart2()}
                """;
    }
}

public abstract class Day : Day<long, long>, IDay
{
    protected Day(int dayNbr) : base(dayNbr)
    {
    }
}

public abstract class Matrix<T> where T : struct
{
    private readonly string[] _input;

    protected Matrix(string[] input)
    {
        XMax = input[0].Length;
        YMax = input.Length;
        _input = input;
    }

    public int XMax { get; set; }

    public int YMax { get; set; }
    public abstract T[][] M { get; }

    protected T[][] Create(Func<char, T> parseFunc)
    {
        var result = new List<T[]>();
        for (int i = 0; i < XMax; i++)
        {
            var yCol = _input.Select(row => parseFunc(row[i])).ToArray();

            result.Add(yCol);
        }
        return [.. result];
    }
}

public class CharMatrix(string[] input) : Matrix<char>(input)
{
    public override char[][] M => Create(c => c);
}

public interface IDay<out TP1, out TP2>
{
    TP1 RunPart1();
    TP2 RunPart2();
    string GetResult();
}

public interface IDay : IDay<long, long>{}