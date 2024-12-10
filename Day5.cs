public class Day5() : Day(5)
{
    public override long RunPart1()
    {
        var orderRules = new Dictionary<int, HashSet<int>> { };
        var manuals = new List<List<int>>();
        var parseFirstPart = true;
        for (int i = 0; i < Input.Length; i++)
        {
            var current = Input[i];
            if (current == "")
            {
                parseFirstPart = false;
                continue;
            }
            if (parseFirstPart)
            {
                var order = current.Split('|');
                var kv = order.Select(int.Parse).ToArray();
                if (!orderRules.TryAdd(kv[0], [kv[1]]))
                {
                    orderRules[kv[0]].Add(kv[1]);
                }
            }
            else
            {
                manuals.Add(current.Split(',').Select(int.Parse).ToList());
            }
        }

        var sum = 0;
        foreach (var manual in manuals)
        {
            var isValid = true;
            for (int i = 1; i < manual.Count; i++)
            {
                var current = manual[i];
                var hasRules = orderRules.TryGetValue(current, out var ruleSet);
                if (!hasRules)
                {
                    continue;
                }
                var beforeCurrent = manual[..i];

                var intersection = ruleSet!.Intersect(beforeCurrent);

                if (intersection.Any())
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                var mid = manual.Count / 2;
                sum += manual[mid];
            }
        }
        return sum;
    }




    public override long RunPart2()
    {
        var orderRules = new Dictionary<int, HashSet<int>> { };
        var manuals = new List<List<int>>();
        var parseFirstPart = true;
        for (int i = 0; i < Input.Length; i++)
        {
            var current = Input[i];
            if (current == "")
            {
                parseFirstPart = false;
                continue;
            }
            if (parseFirstPart)
            {
                var order = current.Split('|');
                var kv = order.Select(int.Parse).ToArray();
                if (!orderRules.TryAdd(kv[0], [kv[1]]))
                {
                    orderRules[kv[0]].Add(kv[1]);
                }
            }
            else
            {
                manuals.Add(current.Split(',').Select(int.Parse).ToList());
            }
        }

        var manualsToSort = new List<List<int>>();
        foreach (var manual in manuals)
        {
            var isValid = true;
            for (int i = 1; i < manual.Count; i++)
            {
                var current = manual[i];
                var hasRules = orderRules.TryGetValue(current, out var ruleSet);
                if (!hasRules)
                {
                    continue;
                }
                var beforeCurrent = manual[..i];

                var intersection = ruleSet!.Intersect(beforeCurrent);

                if (intersection.Any())
                {
                    isValid = false;
                    break;
                }
            }

            if (!isValid)
            {
                manualsToSort.Add(manual);
            }
        }
        var sum = 0;
        foreach (var manual in manualsToSort)
        {
            for (int i = 1; i < manual.Count; i++)
            {
                var current = manual[i];
                var hasRules = orderRules.TryGetValue(current, out var ruleSet);
                if (!hasRules)
                {
                    continue;
                }
                var beforeCurrent = manual[..i];

                var intersection = ruleSet!.Intersect(beforeCurrent).ToList();

                if (intersection.Count > 0)
                {
                    for (int j = 0; j < intersection.Count; j++)
                    {
                        manual.RemoveAt(i - intersection.Count);
                        manual.Insert(i, intersection[j]);
                    }
                    i -= intersection.Count;
                }
            }

            sum += manual[manual.Count / 2];
        }



        return sum;
    }
}