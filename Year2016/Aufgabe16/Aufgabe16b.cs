using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe16b : IAufgabe
{
    private List<bool> _disk;

    public Aufgabe16b()
    {
        var input = Utilities.ReadInput(2016, 16);
        _disk = new(input[0].Length);
        foreach (var character in input[0])
        {
            _disk.Add(character == '1');
        }
    }

    public string Calc()
    {
        const int Length = 35651584;

        while (_disk.Count < Length)
        {
            List<bool> nextDisk = new(_disk.Count * 2 + 1);

            nextDisk.AddRange(_disk);
            nextDisk.Add(false);
            _disk.Reverse();
            nextDisk.AddRange(_disk.Select(x => !x));
            _disk = nextDisk;
        }

        int nextLength = Length;
        do
        {
            GetChecksum(nextLength);
            nextLength /= 2;
        } while (_disk.Count % 2 == 0);

        return string.Join("", _disk.Select(x => x ? 1 : 0));
    }

    private void GetChecksum(int length)
    {
        List<bool> nextDisk = new(length / 2);

        for (int i = 0; i < length; i += 2)
        {
            nextDisk.Add(_disk[i] == _disk[i + 1]);
        }

        _disk = nextDisk;
    }
}
