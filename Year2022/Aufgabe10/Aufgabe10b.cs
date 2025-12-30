using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe10b : IAufgabe
{
    private readonly string[] _input;
    private readonly Grid<char> _grid;
    private int _x = 1;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2022, 10);
        _grid = new(40, 6, '\0');
    }

    public string Calc()
    {
        int cycle = 0;

        foreach (var line in _input)
        {
            switch (line[..4])
            {
                case "noop":
                    Draw(cycle);
                    cycle++;
                    break;
                case "addx":
                    Draw(cycle);
                    Draw(cycle + 1);
                    cycle += 2;
                    _x += int.Parse(line[5..]);

                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        //_grid.Draw();

        return "PZBGZEJB";
    }

    private void Draw(int cycle)
    {
        int xPosition = cycle % 40;
        int yPosition = cycle / 40;

        _grid.SetValue(xPosition, yPosition, int.Abs(xPosition - _x) <= 1 ? '#' : '.');
    }
}
