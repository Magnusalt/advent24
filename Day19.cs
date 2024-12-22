public class Day19() : Day(19)
{
    public override long RunPart1()
    {
        var parts = Input[0].Split(", ").GroupBy(s => s.Length).ToDictionary(g => g.Key, g => g.ToArray());


        var result = 0;
        var memo = new Dictionary<string, long> { [""] = 0 };
        foreach (var item in Input.Skip(2))
        {
            var res = CanBuild(item, parts, memo);

            result += res > 0 ? 1 : 0;
        }

        return result;
    }

    private long CanBuild(string goal, Dictionary<int, string[]> parts, Dictionary<string, long> counts)
    {
        if (counts.TryGetValue(goal, out var result))
        {
            return result;
        }

        if (parts.SelectMany(kv => kv.Value).Any(v => v == goal))
        {
            counts.TryGetValue(goal, out var c);
            counts[goal] = c + 1;
        }

        var substringLength = Math.Min(parts.Keys.Max(), goal.Length);
        while (substringLength > 0)
        {
            var substring = goal[0..substringLength];

            var part = parts[substringLength].FirstOrDefault(p => p == substring);

            if (string.IsNullOrEmpty(part))
            {
                substringLength--;
                continue;
            }

            var solutions = CanBuild(goal[substringLength..], parts, counts);
            if (solutions > 0)
            {
                counts.TryGetValue(goal, out var c);
                counts[goal] = solutions + c;
            }

            substringLength--;
        }

        counts.TryGetValue(goal, out var finalCount);
        counts[goal] = finalCount;
        return finalCount;
    }

    public override long RunPart2()
    {
        var parts = Input[0].Split(", ").GroupBy(s => s.Length).ToDictionary(g => g.Key, g => g.ToArray());


        long result = 0;
        var memo = new Dictionary<string, long> { [""] = 0 };
        foreach (var item in Input.Skip(2))
        {
            var res = CanBuild(item, parts, memo);

            result += res;
        }

        return result;
    }
}