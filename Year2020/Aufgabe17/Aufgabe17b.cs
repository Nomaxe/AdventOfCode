using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe17b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe17b()
    {
        _input = Utilities.ReadInput(2020, 17);
    }

    public string Calc()
    {
        HashSet<Point4D> currentActiveCubes = new();

        for (int y = 0; y < _input.Length; y++)
        {
            for (int x = 0; x < _input[y].Length; x++)
            {
                if (_input[y][x] != '#')
                {
                    continue;
                }

                currentActiveCubes.Add(new(x, y, 0, 0));
            }
        }

        for (int i = 0; i < 6; i++)
        {
            DictionaryCounter<Point4D> counter = new(currentActiveCubes.Count * 80);

            foreach (var currentActiveCube in currentActiveCubes)
            {
                foreach (var neighbour in currentActiveCube.GetNeighbours())
                {
                    counter.Add(neighbour);
                }
                counter.AddKey(currentActiveCube);
            }

            HashSet<Point4D> nextActiveCubes = new(currentActiveCubes.Count);
            foreach (var item in counter)
            {
                var wasActive = currentActiveCubes.Contains(item.Key);

                if (wasActive)
                {
                    if (item.Value == 2 || item.Value == 3)
                    {
                        nextActiveCubes.Add(item.Key);
                    }
                }
                else
                {
                    if (item.Value == 3)
                    {
                        nextActiveCubes.Add(item.Key);
                    }
                }
            }

            currentActiveCubes = nextActiveCubes;
        }

        return currentActiveCubes.Count.ToString();
    }
}
