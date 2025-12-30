using AdventOfCode.Utils;
using Microsoft.Z3;

namespace AdventOfCode.Year2025;

internal class Aufgabe10b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2025, 10);
    }

    public string Calc()
    {
        var resultCount = 0;

        foreach (var line in _input)
        {
            using Context context = new();
            using Optimize optimize = context.MkOptimize();

            var buttonGoalEndIndex = line.IndexOf(']');
            var buttons = GetButtons(line[(buttonGoalEndIndex + 3)..]);
            var joltageStart = line.LastIndexOf('{');
            var goal = line[joltageStart..].GetNumbers();
            List<IntExpr> presses = new(buttons.Count);

            for (int i = 0; i < buttons.Count; i++)
            {
                var press = context.MkIntConst($"p{i}");
                optimize.Add(context.MkGe(press, context.MkInt(0)));
                presses.Add(press);
            }

            for (int i = 0; i < goal.Length; i++)
            {
                List<ArithExpr> terms = new();

                for (int j = 0; j < buttons.Count; j++)
                {
                    if (buttons[j].Contains(i))
                    {
                        terms.Add(presses[j]);
                    }
                }

                var sumExpression = context.MkAdd(terms);
                var tarExpression = context.MkInt(goal[i]);
                optimize.Assert(context.MkEq(sumExpression, tarExpression));
            }

            optimize.MkMinimize(context.MkAdd(presses));

            optimize.Check();
            resultCount += presses.Sum(x => ((IntNum)optimize.Model.Eval(x, true)).Int);
        }

        return resultCount.ToString();
    }

    private static List<int[]> GetButtons(string line)
    {
        var split = line.Split(" (");
        List<int[]> buttons = new(split.Length);
        foreach (var item in split)
        {
            var endIndex = item.LastIndexOf(')');
            var numberString = item[0..endIndex];
            var list = numberString.GetNumbers();
            buttons.Add(list);
        }

        return buttons;
    }
}
