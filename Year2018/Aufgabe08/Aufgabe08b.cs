using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe08b : IAufgabe
{
    private readonly List<int> _input;
    private int _index = 0;

    public Aufgabe08b()
    {
        _input = Utilities.ReadInputAsList<int>(2018, 8, ' ');
    }

    public string Calc()
    {
        return CalcNodes().ToString();
    }

    private int CalcNodes()
    {
        var nodeCount = _input[_index];
        var metadataCount = _input[_index + 1];
        List<int> nodes = new(nodeCount);

        _index += 2;
        for (int i = 0; i < nodeCount; i++)
        {
            nodes.Add(CalcNodes());
        }

        int result = 0;
        for (int i = 0; i < metadataCount; i++)
        {
            var value = _input[_index + i];

            if (nodes.Count > 0)
            {
                var nodeIndex = value - 1;
                if (nodeIndex >= 0 && nodeIndex < nodes.Count)
                {
                    result += nodes[nodeIndex];
                }
            }
            else
            {
                result += value;
            }
        }
        _index += metadataCount;

        return result;
    }
}
