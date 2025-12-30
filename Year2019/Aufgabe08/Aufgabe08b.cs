using AdventOfCode.Utils;

namespace AdventOfCode.Year2019
{
    internal class Aufgabe08b : IAufgabe
    {
        private readonly string _input;
        private readonly List<GridInt> _grids;

        private const int Width = 25;
        private const int Height = 6;

        public Aufgabe08b()
        {
            _input = Utilities.ReadInput(2019, 8)[0];
            _grids = [];
        }

        public string Calc()
        {
            GridInt grid = new(Width, Height);
            _grids.Add(grid);
            int x = 0;
            int y = 0;

            foreach (var character in _input)
            {
                var number = character.ToNumber();
                grid.SetValue(x, y, number);

                x++;

                if (x >= Width)
                {
                    x = 0;
                    y++;

                    if (y >= Height)
                    {
                        y = 0;
                        grid = new(Width, Height);
                        _grids.Add(grid);
                    }
                }
            }


            for (y = 0; y < Height; y++)
            {
                for (x = 0; x < Width; x++)
                {
                    foreach (var gridLoop in _grids)
                    {
                        var value = gridLoop.GetValue(x, y);

                        if (value == 0)
                        {
                            //Console.Write(' ');
                            break;
                        }
                        else if (value == 1)
                        {
                            //Console.Write('#');
                            break;
                        }
                    }
                }

                //Console.WriteLine();
            }

            return "AHFCB";
        }
    }
}
