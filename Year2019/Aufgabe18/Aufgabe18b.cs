using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;
using System.Collections;

namespace AdventOfCode.Year2019;

internal class Aufgabe18b : IAufgabe
{
    private readonly Grid _grid;
    private readonly Point[] _keyPositions;
    private readonly DictionaryDictionary<PointList, MissingKeys, int> _cache;

    private const char highestKey = 'z';

    public Aufgabe18b()
    {
        _grid = Grid.CreateCharGrid(2019, 18);
        _keyPositions = new Point[highestKey - 'a' + 1];
        _cache = new();
    }

    public string Calc()
    {
        for (int i = 'a'; i <= highestKey; i++)
        {
            _keyPositions[i - 'a'] = _grid.GetPointOfValue((char)i);
        }

        Point currentPoint = _grid.GetPointOfValue('@');
        _grid.SetValue(currentPoint, '#');
        _grid.SetValue(currentPoint.X - 1, currentPoint.Y, '#');
        _grid.SetValue(currentPoint.X + 1, currentPoint.Y, '#');
        _grid.SetValue(currentPoint.X, currentPoint.Y - 1, '#');
        _grid.SetValue(currentPoint.X, currentPoint.Y + 1, '#');

        List<char> missingKeysTopLeft = new();
        List<char> missingKeysTopRight = new();
        List<char> missingKeysBottomRight = new();
        List<char> missingKeysBottomLeft = new();
        for (int i = 'a'; i <= highestKey; i++)
        {
            var key = (char)i;
            var point = _grid.GetPointOfValue(key);
            var topHalf = point.Y < _grid.SizeY / 2;
            var leftHalf = point.X < _grid.SizeX / 2;

            if (topHalf)
            {
                if (leftHalf)
                {
                    missingKeysTopLeft.Add(key);
                }
                else
                {
                    missingKeysTopRight.Add(key);
                }
            }
            else
            {
                if (leftHalf)
                {
                    missingKeysBottomLeft.Add(key);
                }
                else
                {
                    missingKeysBottomRight.Add(key);
                }
            }
        }

        return MoveToNextKey(new(currentPoint.X - 1, currentPoint.Y - 1), new(currentPoint.X + 1, currentPoint.Y - 1),
                             new(currentPoint.X + 1, currentPoint.Y + 1), new(currentPoint.X - 1, currentPoint.Y + 1),
                             new(missingKeysTopLeft), new(missingKeysTopRight), new(missingKeysBottomRight), new(missingKeysBottomLeft),
                             0).ToString();
    }

    private int MoveToNextKey(Point currentPositionTopLeft, Point currentPositionTopRight, Point currentPositionBottomRigth, Point currentPositionBottomLeft,
                              MissingKeys missingKeysTopLeft, MissingKeys missingKeysTopRight, MissingKeys missingKeysBottomRigth, MissingKeys missingKeysBottomLeft,
                              int currentLength)
    {
        if (missingKeysTopLeft.Count == 0 && missingKeysTopRight.Count == 0 && missingKeysBottomRigth.Count == 0 && missingKeysBottomLeft.Count == 0)
        {
            return currentLength;
        }

        PointList pointList = new(currentPositionTopLeft, currentPositionTopRight, currentPositionBottomRigth, currentPositionBottomLeft);
        MissingKeys missingKeyList = new(missingKeysTopLeft, missingKeysTopRight, missingKeysBottomRigth, missingKeysBottomLeft);

        if (_cache.TryGetValue(pointList, missingKeyList, out int lengthInCache))
        {
            return currentLength + lengthInCache;
        }

        int minLength = int.MaxValue;
        CompleteSolver solver = new(_grid);
        AddWallCharacters(solver, missingKeysTopLeft);
        AddWallCharacters(solver, missingKeysTopRight);
        AddWallCharacters(solver, missingKeysBottomRigth);
        AddWallCharacters(solver, missingKeysBottomLeft);

        if (missingKeysTopLeft.Count > 0)
        {
            solver.SolveLabyrinth(currentPositionTopLeft);

            foreach (var missingKey in missingKeysTopLeft)
            {
                var keyPosition = _keyPositions[missingKey - 'a'];

                if (solver.TryGetLength(keyPosition, out var length))
                {
                    var newLength = MoveToNextKey(keyPosition, currentPositionTopRight, currentPositionBottomRigth, currentPositionBottomLeft,
                                                  missingKeysTopLeft.RemoveKey(missingKey), missingKeysTopRight, missingKeysBottomRigth, missingKeysBottomLeft,
                                                  currentLength + length);
                    minLength = int.Min(minLength, newLength);
                }
            }
        }

        if (missingKeysTopRight.Count > 0)
        {
            solver.SolveLabyrinth(currentPositionTopRight);

            foreach (var missingKey in missingKeysTopRight)
            {
                var keyPosition = _keyPositions[missingKey - 'a'];

                if (solver.TryGetLength(keyPosition, out var length))
                {
                    var newLength = MoveToNextKey(currentPositionTopLeft, keyPosition, currentPositionBottomRigth, currentPositionBottomLeft,
                                                  missingKeysTopLeft, missingKeysTopRight.RemoveKey(missingKey), missingKeysBottomRigth, missingKeysBottomLeft,
                                                  currentLength + length);
                    minLength = int.Min(minLength, newLength);
                }
            }
        }

        if (missingKeysBottomRigth.Count > 0)
        {
            solver.SolveLabyrinth(currentPositionBottomRigth);

            foreach (var missingKey in missingKeysBottomRigth)
            {
                var keyPosition = _keyPositions[missingKey - 'a'];

                if (solver.TryGetLength(keyPosition, out var length))
                {
                    var newLength = MoveToNextKey(currentPositionTopLeft, currentPositionTopRight, keyPosition, currentPositionBottomLeft,
                                                  missingKeysTopLeft, missingKeysTopRight, missingKeysBottomRigth.RemoveKey(missingKey), missingKeysBottomLeft,
                                                  currentLength + length);
                    minLength = int.Min(minLength, newLength);
                }
            }
        }

        if (missingKeysBottomLeft.Count > 0)
        {
            solver.SolveLabyrinth(currentPositionBottomLeft);

            foreach (var missingKey in missingKeysBottomLeft)
            {
                var keyPosition = _keyPositions[missingKey - 'a'];

                if (solver.TryGetLength(keyPosition, out var length))
                {
                    var newLength = MoveToNextKey(currentPositionTopLeft, currentPositionTopRight, currentPositionBottomRigth, keyPosition,
                                                  missingKeysTopLeft, missingKeysTopRight, missingKeysBottomRigth, missingKeysBottomLeft.RemoveKey(missingKey),
                                                  currentLength + length);
                    minLength = int.Min(minLength, newLength);
                }
            }
        }

        _cache.Add(pointList, missingKeyList, minLength - currentLength);
        return minLength;
    }

    private static void AddWallCharacters(CompleteSolver solver, MissingKeys missingKeys)
    {
        foreach (var missingKey in missingKeys)
        {
            solver.AddWallCharacter((char)(missingKey - 32));
        }
    }

    private class PointList : IEquatable<PointList>
    {
        private readonly Point _topLeft;
        private readonly Point _topRight;
        private readonly Point _bottomRight;
        private readonly Point _bottomLeft;

        public PointList(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomRight = bottomRight;
            _bottomLeft = bottomLeft;
        }

        public bool Equals(PointList? other)
        {
            if (other == null)
            {
                return false;
            }

            return _topLeft == other._topLeft &&
                   _topRight == other._topRight &&
                   _bottomRight == other._bottomRight &&
                   _bottomLeft == other._bottomLeft;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PointList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_topLeft, _topRight, _bottomRight, _bottomLeft);
        }
    }

    private class MissingKeys : IEquatable<MissingKeys>, IEnumerable<char>
    {
        private readonly char[] _missingKeys;
        public int Count => _missingKeys.Length;

        public MissingKeys(List<char> missingKeys)
        {
            _missingKeys = missingKeys.ToArray();
        }

        public MissingKeys(MissingKeys topLeft, MissingKeys topRight, MissingKeys bottomRight, MissingKeys bottomLeft)
        {
            _missingKeys = topLeft.Concat(topRight).Concat(bottomRight).Concat(bottomLeft).ToArray();
        }

        public MissingKeys RemoveKey(char key)
        {
            return new(_missingKeys.Where(x => x != key).ToList());
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
