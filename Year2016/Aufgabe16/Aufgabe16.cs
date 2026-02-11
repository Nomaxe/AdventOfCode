using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe16 : IAufgabe
{
    private readonly string _input;
    private List<bool> _disk;

    public Aufgabe16()
    {
        _input = Utilities.ReadInputAsString(2016, 16);
        _disk = new(_input.Length);
        
    }

    public string Calc()
    {
        const int Length = 272;

        foreach (var character in _input)
        {
            _disk.Add(character == '1');
        }

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
