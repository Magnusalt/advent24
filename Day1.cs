public class Day1() : Day(1)
{
    public override long RunPart1()
    {
        var leftRight = Input.Select(i => i.Split("   ")).Select(lr => (int.Parse(lr[0]), int.Parse(lr[1])));
        var left = leftRight.Select(lr => lr.Item1).Order();
        var right = leftRight.Select(lr => lr.Item2).Order();
        return left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();
    }

    public override long RunPart2()
    {
        var leftRight = Input.Select(i => i.Split("   ")).Select(lr => (int.Parse(lr[0]), int.Parse(lr[1])));
        var left = leftRight.Select(lr => lr.Item1);
        var right = leftRight.Select(lr => lr.Item2).ToList();

        var simScore = 0;
        foreach (var item in left)
        {
            simScore += item * right.Count(i => i == item);
        }

        return simScore;
    }
}
