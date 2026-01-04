using AdventOfCode.Utils;
using Microsoft.Z3;

namespace AdventOfCode.Year2020;

internal class Aufgabe13b : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe13b()
    {
        _input = Utilities.ReadInput(2020, 13);
    }

    public string Calc()
    {
        //Optimierung Chinese Remainder Theorem um das selbst zu berechnen

        var busses = _input[1].Split(',');

        using Context context = new();
        using Optimize optimize = context.MkOptimize();

        var number = context.MkIntConst("p");
        optimize.Add(context.MkGe(number, context.MkInt(0)));

        for (int i = 0; i < busses.Length; i++)
        {
            if (busses[i] == "x")
            {
                continue;
            }

            var mod = context.MkMod(number, context.MkInt(busses[i]));
            var expression = context.MkEq(mod, context.MkInt(GetRemainingValue(int.Parse(busses[i]), i)));
            optimize.Assert(expression);
        }

        optimize.MkMinimize(number);
        optimize.Check();

        return ((IntNum)optimize.Model.Eval(number, true)).UInt64.ToString();
    }

    private static int GetRemainingValue(int id, int index)
    {
        if (index == 0)
        {
            return 0;
        }

        var oldId = id;
        while (id < index)
        {
            id += oldId;
        }

        return id - index;
    }
}
