using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2020, 5);
    }

    public string Calc()
    {
        List<string> min = [_input[0]];
        var currentMinRow = min[0][..7];

        foreach (var line in _input.Skip(1))
        {
            var row = line[..7];
            var compare = row.CompareTo(currentMinRow);

            if (compare < 0)
            {
                min.Clear();
                min.Add(line);
                currentMinRow = row;
            }
            else if (compare == 0)
            {
                min.Add(line);
            }
        }

        var seat = min.Max()!;
        var seatId = 0;

        if (seat[0] == 'B')
        {
            seatId += 64;
        }
        if (seat[1] == 'B')
        {
            seatId += 32;
        }
        if (seat[2] == 'B')
        {
            seatId += 16;
        }
        if (seat[3] == 'B')
        {
            seatId += 8;
        }
        if (seat[4] == 'B')
        {
            seatId += 4;
        }
        if (seat[5] == 'B')
        {
            seatId += 2;
        }
        if (seat[6] == 'B')
        {
            seatId += 1;
        }
        seatId *= 8;

        if (seat[7] == 'R')
        {
            seatId += 4;
        }
        if (seat[8] == 'R')
        {
            seatId += 2;
        }
        if (seat[9] == 'R')
        {
            seatId += 1;
        }

        return seatId.ToString();
    }
}
