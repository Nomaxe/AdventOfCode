namespace AdventOfCode.Utils;

internal partial class Grid<T>
{
    public static Grid CreateCharGrid(int year, int day)
    {
        return CreateCharGrid(Utilities.ReadInput(year, day));
    }

    public static Grid CreateCharGrid(string[] input)
    {
        Grid<char> grid = new(input[0].Length, input.Length);
        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                grid.SetValue(x, y, input[y][x]);
            }
        }

        return grid;
    }

    public static GridBool CreateBoolGrid(int year, int day, char trueValue)
    {
        var input = Utilities.ReadInput(year, day);
        GridBool grid = new(input[0].Length, input.Length);
        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                grid.SetValue(x, y, input[y][x] == trueValue);
            }
        }

        return grid;
    }

    public static GridInt CreateIntGrid(int year, int day)
    {
        var input = Utilities.ReadInput(year, day);
        GridInt grid = new(input[0].Length, input.Length);
        for (int y = 0; y < grid.SizeY; y++)
        {
            for (int x = 0; x < grid.SizeX; x++)
            {
                grid.SetValue(x, y, input[y][x].ToNumber());
            }
        }

        return grid;
    }
}
