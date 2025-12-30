using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe19b : IAufgabe
{
    private readonly IntCode _intcode;

    public Aufgabe19b()
    {
        _intcode = new(2019, 19);
    }

    public string Calc()
    {
        int currentY = 500;
        int smallestX = 100;

        while (true)
        {
            smallestX = GetNewSmallestX(smallestX, currentY);
            if (Calc(smallestX + 99, currentY) && Calc(smallestX, currentY - 99) && Calc(smallestX + 99, currentY - 99))
            {
                return (smallestX * 10000 + currentY - 99).ToString();
            }

            currentY++;
        }
    }

    private int GetNewSmallestX(int smallestX, int currentY)
    {
        while (!Calc(smallestX, currentY))
        {
            smallestX++;
        }

        return smallestX;
    }

    private bool Calc(int x, int y)
    {
        _intcode.AddInput(x);
        _intcode.AddInput(y);
        _intcode.Calc();
        var output = _intcode.Out[0];
        _intcode.Reset();

        return output == 1;
    }
}
