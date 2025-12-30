using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;
    private readonly Point[] _startPoints;

    private const int GridSize = 400;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2018, 6);
        _startPoints = new Point[_input.Length];
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            var numbers = _input[i].GetUnsignedNumbers();
            _startPoints[i] = new(numbers[0], numbers[1]);
        }

        int validPoints = 0;
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                int length = 0;
                Point point = new(x, y);

                foreach (var startPoint in _startPoints)
                {
                    length += point.GetManhattenDistance(startPoint);
                }

                if (length < 10000)
                {
                    validPoints++;
                }
            }
        }

        return validPoints.ToString();
    }
}
