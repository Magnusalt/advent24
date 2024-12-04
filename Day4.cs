
using System.ComponentModel;

public class Day4() : Day(4)
{
    const string XMAS = "XMAS";
    public override long RunPart1()
    {
        var matrix = CharMatrix();
        var count = 0;

        for (int x = 0; x < matrix.XMax; x++)
        {
            for (int y = 0; y < matrix.YMax; y++)
            {
                var current = matrix.M[x][y];
                if (current == 'X')
                {
                    bool[] xmases = [
                        CheckNorth(matrix, x, y),
                        CheckNorthEast(matrix, x, y),
                        CheckEast(matrix, x, y),
                        CheckSouthEast(matrix, x, y),
                        CheckSouth(matrix, x, y),
                        CheckSouthWest(matrix, x, y),
                        CheckWest(matrix, x, y),
                        CheckNorthWest(matrix, x, y)
                    ];
                    count += xmases.Count(b => b);
                }
            }
        }

        return count;
    }

    private bool CheckNorthWest(CharMatrix matrix, int x, int y)
    {
        if (y < 3 || x < 3)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x - i][y - i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckWest(CharMatrix matrix, int x, int y)
    {
        if (x < 3)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x - i][y] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckSouthWest(CharMatrix matrix, int x, int y)
    {
        if (y > matrix.YMax - 4 || x < 3)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x - i][y + i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckSouth(CharMatrix matrix, int x, int y)
    {
        if (y > matrix.YMax - 4)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x][y + i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckSouthEast(CharMatrix matrix, int x, int y)
    {
        if (x > matrix.XMax - 4 || y > matrix.YMax - 4)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x + i][y + i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckEast(CharMatrix matrix, int x, int y)
    {
        if (x > matrix.XMax - 4)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x + i][y] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckNorthEast(CharMatrix matrix, int x, int y)
    {
        if (y < 3 || x > matrix.XMax - 4)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x + i][y - i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    private bool CheckNorth(CharMatrix matrix, int x, int y)
    {
        if (y < 3)
        {
            return false;
        }
        var i = 1;

        while (i < 4 && matrix.M[x][y - i] == XMAS[i])
        {
            i++;
        }
        return i == XMAS.Length;
    }

    public override long RunPart2()
    {
        var matrix = CharMatrix();
        var count = 0;

        for (int x = 1; x < matrix.XMax - 1; x++)
        {
            for (int y = 1; y < matrix.YMax - 1; y++)
            {
                var current = matrix.M[x][y];
                if (current == 'A')
                {
                    var m = matrix.M;
                    var isX_MAS = (m[x - 1][y - 1], m[x + 1][y - 1],
                                    m[x - 1][y + 1], m[x + 1][y + 1]) switch
                    {
                        ('M', 'M',
                        'S', 'S') => true,
                        ('S', 'S',
                        'M', 'M') => true,
                        ('M', 'S',
                        'M', 'S') => true,
                        ('S', 'M',
                        'S', 'M') => true,
                        _ => false
                    };
                    count += isX_MAS ? 1 : 0;
                }
            }
        }

        return count;
    }
}