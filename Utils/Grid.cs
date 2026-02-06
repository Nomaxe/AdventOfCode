using System.Collections;
using System.Data;

namespace AdventOfCode.Utils;

internal partial class Grid<T> : IEnumerable<T>
                               where T : notnull
{
    private readonly T[,] _map;

    public int SizeX => _map.GetLength(1);
    public int SizeY => _map.GetLength(0);
    public int GridSize => SizeX * SizeY;

    public Grid(int mapsizeX, int mapsizeY)
    {
        _map = new T[mapsizeY, mapsizeX];
    }

    public Grid(int mapsize) : this(mapsize, mapsize)
    {

    }

    public Grid(int mapsizeX, int mapsizeY, T value) : this(mapsizeX, mapsizeY)
    {
        for (int y = 0; y < mapsizeY; y++)
        {
            for (int x = 0; x < mapsizeX; x++)
            {
                SetValue(x, y, value);
            }
        }
    }

    public Point GetPointOfValue(T value)
    {
        return GetPointOfValueOrNull(value) ?? throw new KeyNotFoundException($"Value {value} does not exists");
    }

    public Point? GetPointOfValueOrNull(T value)
    {
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                if (GetValue(x, y).Equals(value))
                {
                    return new(x, y);
                }
            }
        }

        return null;
    }

    public IEnumerable<Point> GetPointsOfValue(T value)
    {
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                if (GetValue(x, y).Equals(value))
                {
                    yield return new(x, y);
                }
            }
        }
    }

    public T GetValue(int x, int y)
    {
        return _map[y, x];
    }

    public T GetValue(Point point)
    {
        return GetValue(point.X, point.Y);
    }

    public T? GetValueOrDefault(int x, int y, T? defaultValue = default)
    {
        if (IsInBounds(x, y))
        {
            return GetValue(x, y);
        }

        return defaultValue;
    }

    public void SetValue(int x, int y, T value)
    {
        _map[y, x] = value;
    }

    public void SetValue(Point point, T value)
    {
        SetValue(point.X, point.Y, value);
    }

    public bool HasValue(T value)
    {
        return this.Contains(value);
    }

    public void Replace(T oldValue, T newValue)
    {
        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                if (GetValue(x, y).Equals(oldValue))
                {
                    SetValue(x, y, newValue);
                }
            }
        }
    }

    public bool IsInBounds(int x, int y)
    {
        if (x < 0 || x >= _map.GetLength(1))
        {
            return false;
        }

        if (y < 0 || y >= _map.GetLength(0))
        {
            return false;
        }

        return true;
    }

    public bool IsInBounds(Point point)
    {
        return IsInBounds(point.X, point.Y);
    }

    public Point[] GetInBoundNeighbours(Point point)
    {
        return point.GetNeighbours().Where(IsInBounds).ToArray();
    }

    public Point[] GetInBoundNeighbours(int x, int y)
    {
        return GetInBoundNeighbours(new(x, y));
    }

    public Point[] GetInBoundFullNeighbours(Point point)
    {
        return point.GetFullNeighbours().Where(IsInBounds).ToArray();
    }

    public Point[] GetInBoundFullNeighbours(int x, int y)
    {
        return GetInBoundFullNeighbours(new(x, y));
    }

    public int GetCountOfValue(T value)
    {
        int count = 0;

        foreach (var cell in _map)
        {
            if (cell.Equals(value))
            {
                count++;
            }
        }

        return count;
    }

    public int GetCountOf(Predicate<T> predicate)
    {
        int count = 0;

        foreach (var cell in _map)
        {
            if (predicate(cell))
            {
                count++;
            }
        }

        return count;
    }

    public bool ContainsValue(T value)
    {
        return this.Contains(value);
    }

    public void CopyValuesOfGrid(Grid<T> otherGrid, int xStart, int yStart)
    {
        CopyValuesOfGrid(otherGrid, xStart, yStart, 0, 0, otherGrid.SizeX, otherGrid.SizeY);
    }

    public void CopyValuesOfGrid(Grid<T> otherGrid, int xStart, int yStart, int startSubGrid, int length)
    {
        CopyValuesOfGrid(otherGrid, xStart, yStart, startSubGrid, startSubGrid, length, length);
    }

    public void CopyValuesOfGrid(Grid<T> otherGrid, int xStart, int yStart, int xStartSubGrid, int yStartSubGrid, int xLength, int yLength)
    {
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                var value = otherGrid.GetValue(xStartSubGrid + x, yStartSubGrid + y);
                SetValue(xStart + x, yStart + y, value);
            }
        }
    }

    public Grid<T> RotateLeft()
    {
        Grid<T> newGrid = new(SizeX, SizeY);

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var value = GetValue(x, y);
                newGrid.SetValue(y, SizeX - x - 1, value);
            }
        }

        return newGrid;
    }

    public Grid<T> RotateRight()
    {
        Grid<T> newGrid = new(SizeX, SizeY);

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var value = GetValue(x, y);
                newGrid.SetValue(SizeY - y - 1, x, value);
            }
        }

        return newGrid;
    }

    public Grid<T> Rotate180()
    {
        Grid<T> newGrid = new(SizeX, SizeY);

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var value = GetValue(x, y);
                newGrid.SetValue(SizeX - x - 1, SizeY - y - 1, value);
            }
        }

        return newGrid;
    }

    public Grid<T> FlipHorizontal()
    {
        Grid<T> newGrid = new(SizeX, SizeY);

        for (int y = 0; y < SizeY / 2; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var value = GetValue(x, y);
                newGrid.SetValue(x, SizeY - y - 1, value);

                value = GetValue(x, SizeY - y - 1);
                newGrid.SetValue(x, y, value);
            }
        }

        return newGrid;
    }

    public Grid<T> FlipVertical()
    {
        Grid<T> newGrid = new(SizeX, SizeY);

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX / 2; x++)
            {
                var value = GetValue(x, y);
                newGrid.SetValue(SizeX - x - 1, y, value);

                value = GetValue(SizeX - x - 1, y);
                newGrid.SetValue(x, y, value);
            }
        }

        return newGrid;
    }

    public void Draw()
    {
        Draw(0, SizeX, 0, SizeY);
    }

    public void Draw(int xFrom, int xTo, int yFrom, int yTo)
    {
        for (int y = yFrom; y < yTo; y++)
        {
            for (int x = xFrom; x < xTo; x++)
            {
                if (typeof(T) == typeof(char))
                {
                    if (GetValue(x, y).Equals('\0'))
                    {
                        Console.Write(' ');
                        continue;
                    }
                }
                else if (typeof(T) == typeof(bool))
                {
                    if (GetValue(x, y).Equals(true))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                    continue;
                }

                Console.Write(GetValue(x, y));
            }

            Console.WriteLine();
        }
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = SizeX * SizeY;

            foreach (var item in _map)
            {
                hashCode = hashCode * 17 + item.GetHashCode();
            }

            return hashCode;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Grid other)
        {
            return false;
        }

        if (SizeX != other.SizeX)
        {
            return false;
        }

        if (SizeY != other.SizeY)
        {
            return false;
        }

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                if (!GetValue(x, y).Equals(other.GetValue(x, y)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Grid<T> Clone()
    {
        Grid<T> grid = new(SizeX, SizeY);
        Array.Copy(_map, grid._map, _map.Length);
        return grid;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var cell in _map)
        {
            yield return cell;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static bool operator ==(Grid<T> left, Grid<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Grid<T> left, Grid<T> right)
    {
        return !(left == right);
    }
}