using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;
using System.Collections;

namespace AdventOfCode.Year2019;

internal class Aufgabe18 : IAufgabe
{
    private readonly Grid _grid;
    private readonly Point[] _keyPositions;
    private readonly DictionaryDictionary<Point, MissingKeys, int> _cache;

    private const char highestKey = 'z';

    public Aufgabe18()
    {
        _grid = Grid.CreateCharGrid(2019, 18);
        _keyPositions = new Point[highestKey - 'a' + 1];
        for (int i = 'a'; i <= highestKey; i++)
        {
            _keyPositions[i - 'a'] = _grid.GetPointOfValue((char)i);
        }
        _cache = new();
    }

    public string Calc()
    {
        Point currentPoint = _grid.GetPointOfValue('@');
        _grid.SetValue(currentPoint, '.');

        char[] missingKeys = new char[highestKey - 'a' + 1];
        for (int i = 'a'; i <= highestKey; i++)
        {
            missingKeys[i - 'a'] = (char)i;
        }
        return MoveToNextKey(currentPoint, new(missingKeys), 0).ToString();
    }

    private int MoveToNextKey(Point currentPosition, MissingKeys missingKeys, int currentLength)
    {
        if (missingKeys.Count == 0)
        {
            return currentLength;
        }

        if (_cache.TryGetValue(currentPosition, missingKeys, out int lengthInCache))
        {
            return currentLength + lengthInCache;
        }

        CompleteSolver solver = new(_grid);
        AddWallCharacters(solver, missingKeys);
        solver.SolveLabyrinth(currentPosition);

        int minLength = int.MaxValue;
        foreach (var missingKey in missingKeys)
        {
            var keyPosition = _keyPositions[missingKey - 'a'];

            if (solver.TryGetLength(keyPosition, out var length))
            {
                var newLength = MoveToNextKey(keyPosition, missingKeys.RemoveKey(missingKey), currentLength + length);
                minLength = int.Min(minLength, newLength);
            }
        }

        _cache.Add(currentPosition, missingKeys, minLength - currentLength);

        return minLength;
    }

    private static void AddWallCharacters(CompleteSolver solver, MissingKeys missingKeys)
    {
        foreach (var missingKey in missingKeys)
        {
            solver.AddWallCharacter((char)(missingKey - 32));
        }
    }

    private class MissingKeys : IEquatable<MissingKeys>, IEnumerable<char>
    {
        private readonly char[] _missingKeys;
        public int Count => _missingKeys.Length;

        public MissingKeys(char[] missingKeys)
        {
            _missingKeys = missingKeys;
        }

        public MissingKeys RemoveKey(char key)
        {
            return new(_missingKeys.Where(x => x != key).ToArray());
        }

        public override int GetHashCode()
        {
            int hashcode = 19;

            unchecked
            {
                foreach (var missingKey in _missingKeys)
                {
                    hashcode += missingKey * 31;
                }
            }

            return hashcode;
        }

        public bool Equals(MissingKeys? other)
        {
            if (other == null)
            {
                return false;
            }

            return _missingKeys.SequenceEqual(other._missingKeys);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MissingKeys);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return ((IEnumerable<char>)_missingKeys).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _missingKeys.GetEnumerator();
        }
    }
}
