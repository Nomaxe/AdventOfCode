using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2018;

internal class Aufgabe20 : IAufgabe
{
    private readonly string _input;
    private readonly Grid _grid;

    public Aufgabe20()
    {
        _input = Utilities.ReadInputAsString(2018, 20)[1..^1];
        _grid = new(1000);
    }

    public string Calc()
    {
        Point point = new(500, 500);
        _grid.SetValue(point, 'X');
        SetOpen(point.X + 1, point.Y);
        SetOpen(point.X - 1, point.Y);
        SetOpen(point.X, point.Y + 1);
        SetOpen(point.X, point.Y - 1);
        Move(point, _input);
        _grid.Replace('?', '#');

        CompleteSolver solver = new(_grid);
        solver.SolveLabyrinth(point);

        return (solver.GetMaxLength() / 2).ToString();
    }

    private Point Move(Point point, string input)
    {
        Point startPosition = point;

        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case 'N':
                    SetDoor(point.X, point.Y - 1);
                    SetWall(point.X - 1, point.Y - 1);
                    SetWall(point.X + 1, point.Y - 1);
                    point = new(point.X, point.Y - 2);
                    SetSpace(point);
                    SetOpen(point.X - 1, point.Y);
                    SetOpen(point.X + 1, point.Y);
                    SetOpen(point.X, point.Y - 1);
                    break;
                case 'E':
                    SetDoor(point.X + 1, point.Y);
                    SetWall(point.X + 1, point.Y - 1);
                    SetWall(point.X + 1, point.Y + 1);
                    point = new(point.X + 2, point.Y);
                    SetSpace(point);
                    SetOpen(point.X + 1, point.Y);
                    SetOpen(point.X, point.Y - 1);
                    SetOpen(point.X, point.Y + 1);
                    break;
                case 'S':
                    SetDoor(point.X, point.Y + 1);
                    SetWall(point.X - 1, point.Y + 1);
                    SetWall(point.X + 1, point.Y + 1);
                    point = new(point.X, point.Y + 2);
                    SetSpace(point);
                    SetOpen(point.X - 1, point.Y);
                    SetOpen(point.X + 1, point.Y);
                    SetOpen(point.X, point.Y + 1);
                    break;
                case 'W':
                    SetDoor(point.X - 1, point.Y);
                    SetWall(point.X - 1, point.Y - 1);
                    SetWall(point.X - 1, point.Y + 1);
                    point = new(point.X - 2, point.Y);
                    SetSpace(point);
                    SetOpen(point.X - 1, point.Y);
                    SetOpen(point.X, point.Y - 1);
                    SetOpen(point.X, point.Y + 1);
                    break;
                case '(':
                    var subInput = GetInput(input, i);
                    point = Move(point, subInput);
                    i += subInput.Length + 1;
                    break;
                case '|':
                    point = startPosition;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return point;
    }

    private void SetWall(int x, int y)
    {
        _grid.SetValue(x, y, '#');
    }

    private void SetDoor(int x, int y)
    {
        _grid.SetValue(x, y, '|');
    }

    private void SetSpace(Point point)
    {
        _grid.SetValue(point, '.');
    }

    private void SetOpen(int x, int y)
    {
        if (_grid.GetValue(x, y) == '\0')
        {
            _grid.SetValue(x, y, '?');
        }
    }

    private static string GetInput(string input, int position)
    {
        int depth = 0;
        int endPosition = position;

        do
        {
            switch (input[endPosition])
            {
                case '(':
                    depth++;
                    break;
                case ')':
                    depth--;
                    break;
            }

            endPosition++;
        } while (depth > 0);

        return input[(position + 1)..(endPosition - 1)];
    }
}
