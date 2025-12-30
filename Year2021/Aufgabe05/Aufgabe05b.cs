using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe05b : IAufgabe
{
    private readonly string[] _input;
    private readonly GridInt _grid;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2021, 5);
        _grid = new(1000);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (numbers[0] == numbers[2])
            {
                //Horizontal
                var from = int.Min(numbers[1], numbers[3]);
                var to = int.Max(numbers[1], numbers[3]);
                for (int y = from; y <= to; y++)
                {
                    _grid.SetValue(numbers[0], y, _grid.GetValue(numbers[0], y) + 1);
                }
                continue;
            }
            else if (numbers[1] == numbers[3])
            {
                //Vertical
                var from = int.Min(numbers[0], numbers[2]);
                var to = int.Max(numbers[0], numbers[2]);
                for (int x = from; x <= to; x++)
                {
                    _grid.SetValue(x, numbers[1], _grid.GetValue(x, numbers[1]) + 1);
                }
                continue;
            }

            //Diagonal
            if (numbers[1] > numbers[3])
            {
                (numbers[0], numbers[1], numbers[2], numbers[3]) = (numbers[2], numbers[3], numbers[0], numbers[1]);
            }

            if (numbers[0] < numbers[2])
            {
                //Diagonal rechts
                for (int i = 0; i < numbers[3] - numbers[1] + 1; i++)
                {
                    var xPosition = numbers[0] + i;
                    var yPosition = numbers[1] + i;

                    _grid.SetValue(xPosition, yPosition, _grid.GetValue(xPosition, yPosition) + 1);
                }
            }
            else
            {
                //Diagonal links
                for (int i = 0; i < numbers[3] - numbers[1] + 1; i++)
                {
                    var xPosition = numbers[0] - i;
                    var yPosition = numbers[1] + i;

                    _grid.SetValue(xPosition, yPosition, _grid.GetValue(xPosition, yPosition) + 1);
                }
            }
        }

        return _grid.GetCountOf(x => x > 1).ToString();
    }
}
