public class Day11() : Day(11)
{
    public override long RunPart1()
    {

        return Run(25);
    }

    private long Run(int targetLevel)
    {
        long sum = 0;

        var cache = new Dictionary<(int level, string stone), long>();

        foreach (var item in Input.First().Split(' '))
        {
            sum += Count(0, item, targetLevel, cache);
        }

        return sum;
    }

    private long Count(int level, string stone, int targetLevel, Dictionary<(int level, string stone), long> cache)
    {
        var stoneNbr = long.Parse(stone);
        stone = stoneNbr.ToString(); 
        if (level == targetLevel)
        {
            return 1;
        }

        if (stone == "0")
        {
            return Count(level + 1, "1", targetLevel, cache);
        }

        var stoneLength = stone.Length;
        if (stoneLength % 2 == 0)
        {
            var leftStone = stone[..(stoneLength / 2)];
            var rightStone = stone[(stoneLength / 2)..];
            long sum = 0;
            if (cache.TryGetValue((level, leftStone), out var leftValue))
            {
                sum += leftValue;
            }
            else
            {
                var computedLeftValue = Count(level + 1, leftStone, targetLevel, cache);
                cache.Add((level, leftStone), computedLeftValue);
                sum += computedLeftValue;
            }
            if (cache.TryGetValue((level, rightStone), out var rightValue))
            {
                sum += rightValue;
            }
            else
            {
                var computedRightValue = Count(level + 1, rightStone, targetLevel, cache);
                cache.Add((level, rightStone), computedRightValue);
                sum += computedRightValue;
            }

            return sum;
        }
        else
        {
            var next = long.Parse(stone) * 2024;

            return Count(level + 1, next.ToString(), targetLevel, cache);
        }
    }

    public override long RunPart2()
    {
        return Run(75);
    }
}