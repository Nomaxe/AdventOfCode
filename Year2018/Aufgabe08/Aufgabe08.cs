using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe08 : IAufgabe
{
    private readonly List<int> _input;
    private int _result = 0;
    private int _index = 0;

    public Aufgabe08()
    {
        _input = Utilities.ReadInputAsIntList(2018, 8, ' ');
    }

    public string Calc()
    {
        CalcNodes();

        return _result.ToString();
    }

    private void CalcNodes()
    {
        var nodeCount = _input[_index];
        var metadataCount = _input[_index + 1];

        _index += 2;
        for (int i = 0; i < nodeCount; i++)
        {
            CalcNodes();
        }

        for (int i = 0; i < metadataCount; i++)
        {
            _result += _input[_index + i];
        }
        _index += metadataCount;
    }
}
