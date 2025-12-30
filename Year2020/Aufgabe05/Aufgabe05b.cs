using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe05b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2020, 5);
    }

    public string Calc()
    {
        var list = _input.Select(GetSeatId).Order().ToList();
        var offset = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            if (i + offset != list[i])
            {
                return (i + offset).ToString();
            }
        }

        throw new NotImplementedException();
    }

    private int GetSeatId(string seat)
    {
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

        return seatId;
    }
}
