using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe19b : IAufgabe
{
    private readonly Dictionary<string, Workflow[]> _workflows = [];
    private ulong _result = 0;

    public Aufgabe19b()
    {
        var input = Utilities.ReadInput(2023, 19);
        foreach (var line in input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)))
        {
            var index = line.IndexOf('{');
            _workflows.Add(line[..index], line[(index + 1)..^1].Split(',').Select(x => new Workflow(x)).ToArray());
        }
    }

    public string Calc()
    {
        Calc("in", 1, 4000, 1, 4000, 1, 4000, 1, 4000);
        return _result.ToString();
    }

    private void Calc(string workflowId, int xMin, int xMax, int mMin, int mMax, int aMin, int aMax, int sMin, int sMax)
    {
        if (workflowId == "R")
        {
            return;
        }
        else if (workflowId == "A")
        {
            _result += (ulong)(xMax - xMin + 1) * (ulong)(mMax - mMin + 1) * (ulong)(aMax - aMin + 1) * (ulong)(sMax - sMin + 1);
            return;
        }

        foreach (var workflow in _workflows[workflowId])
        {
            if (workflow.Value == '\0')
            {
                Calc(workflow.Then, xMin, xMax, mMin, mMax, aMin, aMax, sMin, sMax);
                continue;
            }

            if (workflow.Value == 'x')
            {
                if (workflow.Sign == Sign.BiggerThen)
                {
                    if (xMin <= workflow.Number)
                    {
                        Calc(workflow.Then, workflow.Number + 1, xMax, mMin, mMax, aMin, aMax, sMin, sMax);
                    }
                    xMax = workflow.Number;
                }
                else if (workflow.Sign == Sign.LesserThen)
                {
                    if (xMax >= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, workflow.Number - 1, mMin, mMax, aMin, aMax, sMin, sMax);
                    }
                    xMin = workflow.Number;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (workflow.Value == 'm')
            {
                if (workflow.Sign == Sign.BiggerThen)
                {
                    if (mMin <= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, workflow.Number + 1, mMax, aMin, aMax, sMin, sMax);
                    }
                    mMax = workflow.Number;
                }
                else if (workflow.Sign == Sign.LesserThen)
                {
                    if (mMax >= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, mMin, workflow.Number - 1, aMin, aMax, sMin, sMax);
                    }
                    mMin = workflow.Number;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (workflow.Value == 'a')
            {
                if (workflow.Sign == Sign.BiggerThen)
                {
                    if (aMin <= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, mMin, mMax, workflow.Number + 1, aMax, sMin, sMax);
                    }
                    aMax = workflow.Number;
                }
                else if (workflow.Sign == Sign.LesserThen)
                {
                    if (aMax >= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, mMin, mMax, aMin, workflow.Number - 1, sMin, sMax);
                    }
                    aMin = workflow.Number;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (workflow.Value == 's')
            {
                if (workflow.Sign == Sign.BiggerThen)
                {
                    if (sMin <= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, mMin, mMax, aMin, aMax, workflow.Number + 1, sMax);
                    }
                    sMax = workflow.Number;
                }
                else if (workflow.Sign == Sign.LesserThen)
                {
                    if (sMax >= workflow.Number)
                    {
                        Calc(workflow.Then, xMin, xMax, mMin, mMax, aMin, aMax, sMin, workflow.Number - 1);
                    }
                    sMin = workflow.Number;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    private readonly struct Workflow
    {
        public char Value { get; private init; }
        public Sign Sign { get; private init; }
        public int Number { get; private init; }
        public string Then { get; private init; }

        public Workflow(string line)
        {
            var index = line.IndexOf(':');

            if (index > 0)
            {
                Value = line[0];
                Sign = line[1] == '>' ? Sign.BiggerThen : Sign.LesserThen;
                Number = int.Parse(line[2..index]);
                Then = line[(index + 1)..];
            }
            else
            {
                Then = line;
            }
        }
    }

    private enum Sign
    {
        BiggerThen,
        LesserThen
    }
}
