using System.Numerics;

public class Day6() : Day(6)
{
    public override long RunPart1()
    {

        var dir = new Vector2(0, -1);

        var map = CharMatrix();

        var xCurrent = 0;
        var yCurrent = 0;

        var visited = new HashSet<(int x, int y)>();

        for (int x = 0; x < map.XMax; x++)
        {
            for (int y = 0; y < map.YMax; y++)
            {
                if (map.M[x][y] == '^')
                {
                    xCurrent = x;
                    yCurrent = y;
                }
            }
        }

        while (xCurrent >= 0 && xCurrent < map.XMax && yCurrent >= 0 && yCurrent < map.YMax)
        {
            var path = map.M[xCurrent][yCurrent];
            if (path is '.' or '^')
            {
                visited.Add((xCurrent, yCurrent));

                xCurrent += (int)dir.X;
                yCurrent += (int)dir.Y;
            }
            else
            {
                xCurrent -= (int)dir.X;
                yCurrent -= (int)dir.Y;
                dir = RotateIntegral(dir);
            }
        }


        return visited.Count;
    }

    private Vector2 RotateIntegral(Vector2 dir, float radians = (float)Math.PI / 2)
    {
        var rotation = Matrix4x4.CreateRotationZ(radians);


        var newDir = Vector2.TransformNormal(dir, rotation);

        newDir.X = (float)Math.Round(newDir.X);
        newDir.Y = (float)Math.Round(newDir.Y);
        return newDir;
    }

    public override long RunPart2()
    {
        var dir = new Vector2(0, -1);

        var map = CharMatrix();

        var xCurrent = 0;
        var yCurrent = 0;

        var cache = new HashSet<(int, int, Vector2)>();

        for (int x = 0; x < map.XMax; x++)
        {
            for (int y = 0; y < map.YMax; y++)
            {
                if (map.M[x][y] == '^')
                {
                    xCurrent = x;
                    yCurrent = y;
                }
            }
        }

        var nbrOfPossibleCirculars = 0;

        while (xCurrent >= 0 && xCurrent < map.XMax && yCurrent >= 0 && yCurrent < map.YMax)
        {
            var path = map.M[xCurrent][yCurrent];
            if (path is '.' or '^')
            {


                var isCircular = TurnHere(xCurrent, yCurrent, map, dir, cache);
                if (isCircular)
                {
                    nbrOfPossibleCirculars++;
                }


                xCurrent += (int)dir.X;
                yCurrent += (int)dir.Y;
            }
            else
            {
                xCurrent -= (int)dir.X;
                yCurrent -= (int)dir.Y;
                dir = RotateIntegral(dir);
            }
        }

        return nbrOfPossibleCirculars;
    }

    private bool TurnHere(int xCurrent, int yCurrent, CharMatrix map, Vector2 dir, HashSet<(int, int, Vector2)> global)
    {
        dir = RotateIntegral(dir);
        var cache = new HashSet<(int, int, Vector2)>();

        var turns = 0;
        while (xCurrent >= 0 && xCurrent < map.XMax && yCurrent >= 0 && yCurrent < map.YMax)
        {
            if (cache.Contains((xCurrent, yCurrent, dir)) || global.Contains((xCurrent, yCurrent, dir)))
            {
                foreach (var item in cache)
                {
                    global.Add(item);
                }
                return true;
            }
            cache.Add((xCurrent, yCurrent, dir));
            var path = map.M[xCurrent][yCurrent];
            if (path is '.' or '^')
            {
                xCurrent += (int)dir.X;
                yCurrent += (int)dir.Y;
            }
            else
            {
                xCurrent -= (int)dir.X;
                yCurrent -= (int)dir.Y;
                dir = RotateIntegral(dir);
                turns++;
            }
            if(turns > 100)
            {
                return false;
            }
        }
        return false;
    }
}