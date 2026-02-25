using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe24b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe24b()
    {
        _input = Utilities.ReadInput(2020, 24);
    }

    public string Calc()
    {
        var blackPoints = FillDay0();

        for (int i = 1; i <= 100; i++)
        {
            DictionaryCounter<PointHexGrid> counterBlack = new();
            DictionaryCounter<PointHexGrid> counterWhite = new();

            foreach (var point in blackPoints)
            {
                counterBlack.AddKey(point);
                foreach(var neighbour in point.GetNeighbours())
                {
                    if (blackPoints.Contains(neighbour))
                    {
                        counterBlack.Add(neighbour);
                    }
                    else
                    {
                        counterWhite.Add(neighbour);
                    }
                }
            }

            HashSet<PointHexGrid> nextBlackPoints = new(blackPoints.Count);
            foreach (var point in counterBlack.Where(x => x.Value == 1 || x.Value == 2))
            {
                nextBlackPoints.Add(point.Key);
            }
            foreach (var point in counterWhite.Where(x => x.Value == 2))
            {
                nextBlackPoints.Add(point.Key);
            }

            blackPoints = nextBlackPoints;
        }

        return blackPoints.Count.ToString();
    }

    private HashSet<PointHexGrid> FillDay0()
    {
        HashSet<PointHexGrid> blackPoints = new(_input.Length);

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

            if (!blackPoints.Remove(point))
            {
                blackPoints.Add(point);
            }
        }

        return blackPoints;
    }
}
