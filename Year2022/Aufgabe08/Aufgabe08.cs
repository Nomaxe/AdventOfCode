using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    internal class Aufgabe08 : IAufgabe
    {
        private readonly GridInt _grid;

        public Aufgabe08()
        {
            _grid = GridInt.CreateIntGrid(2022, 8);
        }

        public string Calc()
        {
            int result = 0;

            result += 2 * _grid.SizeX;
            result += 2 * (_grid.SizeY - 2);

            for (int y = 1; y < _grid.SizeY - 1; y++)
            {
                for (int x = 1; x < _grid.SizeX - 1; x++)
                {
                    if (CheckLeft(x, y))
                    {
                        result++;
                        continue;
                    }

                    if (CheckRight(x, y))
                    {
                        result++;
                        continue;
                    }

                    if (CheckTop(x, y))
                    {
                        result++;
                        continue;
                    }

                    if (CheckBottom(x, y))
                    {
                        result++;
                        continue;
                    }
                }
            }

            return result.ToString();
        }

        private bool CheckLeft(int x, int y)
        {
            int startValue = _grid.GetValue(x, y);

            for (x--; x >= 0; x--)
            {
                if (_grid.GetValue(x, y) >= startValue)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckRight(int x, int y)
        {
            int startValue = _grid.GetValue(x, y);

            for (x++; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) >= startValue)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckTop(int x, int y)
        {
            int startValue = _grid.GetValue(x, y);

            for (y--; y >= 0; y--)
            {
                if (_grid.GetValue(x, y) >= startValue)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckBottom(int x, int y)
        {
            int startValue = _grid.GetValue(x, y);

            for (y++; y < _grid.SizeY; y++)
            {
                if (_grid.GetValue(x, y) >= startValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
