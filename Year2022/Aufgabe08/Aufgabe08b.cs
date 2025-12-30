using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    internal class Aufgabe08b : IAufgabe
    {
        private readonly GridInt _grid;

        public Aufgabe08b()
        {
            _grid = GridInt.CreateIntGrid(2022, 8);
        }

        public string Calc()
        {
            int maxResult = 0;

            for (int y = 1; y < _grid.SizeY - 1; y++)
            {
                for (int x = 1; x < _grid.SizeX - 1; x++)
                {
                    int currentResult = 1;

                    currentResult *= CheckLeft(y, x);
                    currentResult *= CheckRight(y, x);
                    currentResult *= CheckTop(y, x);
                    currentResult *= CheckBottom(y, x);

                    maxResult = int.Max(currentResult, maxResult);
                }
            }

            return maxResult.ToString();
        }

        private int CheckLeft(int x, int y)
        {
            int count = 0;
            int startValue = _grid.GetValue(x, y);

            for (x--; x >= 0; x--)
            {
                count++;

                if (_grid.GetValue(x, y) >= startValue)
                {
                    return count;
                }
            }

            return count;
        }

        private int CheckRight(int x, int y)
        {
            int count = 0;
            int startValue = _grid.GetValue(x, y);

            for (x++; x < _grid.SizeX; x++)
            {
                count++;

                if (_grid.GetValue(x, y) >= startValue)
                {
                    return count;
                }
            }

            return count;
        }

        private int CheckTop(int x, int y)
        {
            int count = 0;
            int startValue = _grid.GetValue(x, y);

            for (y--; y >= 0; y--)
            {
                count++;

                if (_grid.GetValue(x, y) >= startValue)
                {
                    return count;
                }
            }

            return count;
        }

        private int CheckBottom(int x, int y)
        {
            int count = 0;
            int startValue = _grid.GetValue(x, y);

            for (y++; y < _grid.SizeY; y++)
            {
                count++;

                if (_grid.GetValue(x, y) >= startValue)
                {
                    return count;
                }
            }

            return count;
        }
    }
}
