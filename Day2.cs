public class Day2() : Day(2)
{
    public override long RunPart1()
    {
        var count = 0;

        foreach (var report in Input)
        {
            var reportInts = report.Split(" ").Select(int.Parse).ToList();

            if (ReportIsSafe(reportInts))
            {
                count++;
            }
        }

        return count;
    }

    private bool ReportIsSafe(List<int> report)
    {
        var initalDirection = report[1] - report[0];

        if (initalDirection == 0)
        {
            return false;
        }
        var increasing = initalDirection > 0;

        var level = 0;
        for (int i = 1; i < report.Count; i++)
        {
            var direction = report[i] - report[i - 1];
            var delta = Math.Abs(direction);
            if (!(delta >= 1 && delta <= 3))
            {
                return false;
            }
            var nextLevel = level + direction;
            if (increasing)
            {
                if (nextLevel < level)
                {
                    return false;
                }
            }
            else
            {
                if (nextLevel > level)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override long RunPart2()
    {
        var count = 0;

        foreach (var report in Input)
        {
            var reportInts = report.Split(" ").Select(int.Parse).ToList();

            if (ReportIsSafe(reportInts))
            {
                count++;
                continue;
            }

            for (int indexToSkip = 0; indexToSkip < reportInts.Count; indexToSkip++)
            {
                var first = reportInts[0..indexToSkip];
                var second = reportInts[(indexToSkip + 1)..];
                
                var reduced = first.Concat(second).ToList();

                if (ReportIsSafe(reduced))
                {
                    count++;
                    break;
                }
            }
        }

        return count;
    }
}