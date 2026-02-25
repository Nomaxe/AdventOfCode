using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe24 : IAufgabe
{
    private readonly string[] _input;
    private readonly HashSet<PointHexGrid> _blackPoints;

    public Aufgabe24()
    {
        _input = Utilities.ReadInput(2020, 24);
        _blackPoints = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            PointHexGrid point = new();

            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case 'e':
                        point = point.MoveRight();
                        break;
                    case 'w':
                        point = point.MoveLeft();
                        break;
                    case 's':
                        if (line[i + 1] == 'e')
                        {
                            point = point.MoveDownRight();
                        }
                        else
                        {
                            point = point.MoveDownLeft();
                        }
                        i++;
                        break;
                    case 'n':
                        if (line[i + 1] == 'e')
                        {
                            point = point.MoveUpRight();
                        }
                        else
                        {
                            point = point.MoveUpLeft();
                        }
                        i++;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            if (!_blackPoints.Remove(point))
            {
                _blackPoints.Add(point);
            }
        }

        return _blackPoints.Count.ToString();
    }
}
