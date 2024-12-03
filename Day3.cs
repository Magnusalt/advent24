using System.Text.RegularExpressions;

namespace advent24
{
    public partial class Day3() : Day(3)
    {
        public override long RunPart1()
        {
            var instructions = new List<string>();

            foreach (var row in Input)
            {
                var matches = FindMul().Matches(row);
                instructions.AddRange(matches.Select(m => m.ToString()));
            }

            return instructions.Select(
                i => i[4..^1]
                .Split(',')
                .Select(int.Parse)
                .Aggregate(1, (acc, val) => acc * val))
            .Sum();

        }

        [GeneratedRegex("mul\\([0-9]+\\,[0-9]+\\)")]
        private static partial Regex FindMul();

        [GeneratedRegex("do\\(\\)")]
        private static partial Regex FindDo();

        [GeneratedRegex("don\\'t\\(\\)")]
        private static partial Regex FindDont();

        public override long RunPart2()
        {
            var enable = true;
            var sum = 0;
            foreach (var row in Input)
            {
                var between = FindMul().EnumerateSplits(row);
                var matches = FindMul().Matches(row);
                var matchIndex = 0;
                foreach (var section in between)
                {
                    if (section.Start.Value == section.End.Value && section.End.Value == row.Length)
                    {
                        continue;
                    }
                    var before = row[section.Start..section.End];
                    var foundDo = FindDo().IsMatch(before);
                    var foundDont = FindDont().IsMatch(before);

                    enable = (enable, foundDo, foundDont) switch
                    {
                        (true, true, false) => true,
                        (true, false, true) => false,
                        (false, true, false) => true,
                        (false, false, true) => false,
                        (true, false, false) => true,
                        (false, false, false) => false,
                        (_, _, _) => throw new InvalidOperationException($"enable: {enable}, foundDo: {foundDo}, foundDont: {foundDont} ")
                    };

                    if (enable)
                    {
                        var matchValue = matches[matchIndex].ToString()[4..^1].Split(',').Select(int.Parse).Aggregate(1, (acc, val) => acc * val);
                        sum += matchValue;
                    }
                    matchIndex++;
                }
            }


            return sum;
        }
    }
}