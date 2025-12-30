using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe10 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe10()
    {
        _input = Utilities.ReadInput(2025, 10);
    }

    public string Calc()
    {
        var resultCount = 0;

        foreach (var line in _input)
        {
            var goalEndIndex = line.IndexOf(']');
            var goal = line[1..goalEndIndex];
            var buttons = GetButtons(line[(goalEndIndex + 3)..]);

            var count = 1;
            while (true)
            {
                var result = TestButton(goal, CreateStartState(goal.Length), buttons, count);

                if (result)
                {
                    resultCount += count;
                    break;
                }

                count++;
            }
        }

        return resultCount.ToString();
    }

    private static bool TestButton(string goal, char[] currentState, List<int[]> buttons, int count)
    {
        if (count == 0)
        {
            return goal == new string(currentState);
        }

        foreach (var button in buttons)
        {
            var result = TestButton(goal, PressButtons(currentState, button), buttons, count - 1);

            if (result)
            {
                return true;
            }
        }

        return false;
    }

    private static char[] PressButtons(char[] currentState, int[] buttons)
    {
        char[] nextState = new char[currentState.Length];
        Array.Copy(currentState, nextState, currentState.Length);

        foreach (var button in buttons)
        {
            switch (nextState[button])
            {
                case '#':
                    nextState[button] = '.';
                    break;
                case '.':
                    nextState[button] = '#';
                    break;
            }
        }

        return nextState;
    }

    private static char[] CreateStartState(int length)
    {
        char[] state = new char[length];
        for (int i = 0; i < length; i++)
        {
            state[i] = '.';
        }

        return state;
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
