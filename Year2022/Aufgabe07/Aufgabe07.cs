using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe07 : IAufgabe
{
    private readonly HashSet<string> _directories = [];
    private readonly Dictionary<string, int> _files = [];
    private readonly List<string> _currentDirectory = [];

    public Aufgabe07()
    {
        var input = Utilities.ReadInput(2022, 7);
        foreach (var line in input)
        {
            switch (line[0])
            {
                case '$':
                    if (line[2..4] == "cd")
                    {
                        var directory = line[5..];
                        if (directory != "..")
                        {
                            _currentDirectory.Add(directory);
                            _directories.Add(string.Join('/', _currentDirectory));
                        }
                        else
                        {
                            _currentDirectory.RemoveAt(_currentDirectory.Count - 1);
                        }
                    }
                    break;
                case 'd':
                    break;
                default:
                    var split = line.Split(' ');
                    _files.Add(string.Join('/', _currentDirectory.Append(split[1])), int.Parse(split[0]));
                    break;
            }
        }
    }

    public string Calc()
    {
        int result = 0;

        foreach (var directory in _directories)
        {
            var size = _files.Where(x => x.Key.StartsWith(directory)).Sum(x => x.Value);
            if (size < 100000)
            {
                result += size;
            }
        }

        return result.ToString();
    }
}
