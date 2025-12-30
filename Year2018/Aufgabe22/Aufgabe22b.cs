using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe22b : IAufgabe
{
    private readonly int _depth;
    private readonly Point _target;
    private readonly GridInt _erosionLevel;
    private readonly Grid<Type> _cave;
    private readonly GridBool _didSwitch;
    private readonly HashSet<Position> _didVisit;

    public Aufgabe22b()
    {
        var input = Utilities.ReadInput(2018, 22);
        _depth = input[0].GetNumber(7);
        _target = new(input[1].GetNumbers());

        _erosionLevel = new(_target.X * 5 + 1, _target.Y * 2 + 1);
        _cave = new(_erosionLevel.SizeX, _erosionLevel.SizeY);
        _didSwitch = new(_erosionLevel.SizeX, _erosionLevel.SizeY);
        _didVisit = new();
    }

    public string Calc()
    {
        for (int y = 0; y < _erosionLevel.SizeY; y++)
        {
            for (int x = 0; x < _erosionLevel.SizeX; x++)
            {
                CalcErosionLevel(x, y);
            }
        }

        int minutes = 0;
        List<Position>[] moves = new List<Position>[8];
        for (int i = 0; i < moves.Length; i++)
        {
            moves[i] = new();
        }
        moves[0].Add(new(Tool.Torch, new(0, 0)));

        do
        {
            foreach (var move in moves[0])
            {
                if (move.Tool == Tool.Torch && move.Point == _target)
                {
                    return minutes.ToString();
                }

                foreach (var neighbour in _cave.GetInBoundNeighbours(move.Point))
                {
                    if (IsValidTool(move.Tool, _cave.GetValue(neighbour)))
                    {
                        Position nextPosition = new(move.Tool, neighbour);
                        if (_didVisit.Add(nextPosition))
                        {
                            moves[1].Add(nextPosition);
                        }
                    }
                }

                if (!_didSwitch.GetValue(move.Point))
                {
                    _didSwitch.SetValue(move.Point, true);
                    moves[7].Add(new(GetOtherPossibleTool(move.Tool, _cave.GetValue(move.Point)), move.Point));
                }
            }

            minutes++;
            Array.Copy(moves, 1, moves, 0, 7);
            moves[7] = new();
        } while (true);
    }

    private void CalcErosionLevel(int x, int y)
    {
        int geologicIndex;

        if (x == 0 && y == 0)
        {
            geologicIndex = 0;
        }
        else if (x == _target.X && y == _target.Y)
        {
            geologicIndex = 0;
        }
        else if (y == 0)
        {
            geologicIndex = x * 16807;
        }
        else if (x == 0)
        {
            geologicIndex = y * 48271;
        }
        else
        {
            geologicIndex = _erosionLevel.GetValue(x - 1, y) * _erosionLevel.GetValue(x, y - 1);
        }

        var erosionLevel = (geologicIndex + _depth) % 20183;
        _erosionLevel.SetValue(x, y, erosionLevel);

        _cave.SetValue(x, y, (Type)(erosionLevel % 3));
    }

    private static bool IsValidTool(Tool tool, Type type)
    {
        return tool switch
        {
            Tool.Torch => type == Type.Rocky || type == Type.Narrow,
            Tool.ClimbingGear => type == Type.Rocky || type == Type.Wet,
            Tool.Neither => type == Type.Wet || type == Type.Narrow,
            _ => throw new NotImplementedException(),
        };
    }

    private static Tool GetOtherPossibleTool(Tool tool, Type type)
    {
        return type switch
        {
            Type.Rocky => tool == Tool.ClimbingGear ? Tool.Torch : Tool.ClimbingGear,
            Type.Wet => tool == Tool.ClimbingGear ? Tool.Neither : Tool.ClimbingGear,
            Type.Narrow => tool == Tool.Torch ? Tool.Neither : Tool.Torch,
            _ => throw new NotImplementedException(),
        };
    }

    private enum Type
    {
        Rocky,
        Wet,
        Narrow
    }

    private enum Tool
    {
        Torch,
        ClimbingGear,
        Neither
    }

    private record struct Position(Tool Tool, Point Point)
    {

    }
}
