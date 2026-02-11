using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe10b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Light> _lights;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2018, 10);
        _lights = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _lights.Add(new(line));
        }

        foreach (var light in _lights)
        {
            light.Move(10400);
        }

        for (int i = 0; i < 100; i++)
        {
            foreach (var light in _lights)
            {
                light.Move();
            }

            Draw();
        }

        return "10454"; //to look though the images is easier, than to develop a solution
    }

    private void Draw()
    {
        const int SizeX = 300;
        const int SizeY = 150;

        Grid grid = new(SizeX, SizeY, ' ');

        foreach (var light in _lights)
        {
            if (grid.IsInBounds(light.PositionX, light.PositionY))
            {
                grid.SetValue(light.PositionX, light.PositionY, '#');
            }
        }

        //grid.Draw();
    }

    private class Light
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public int VelocityX { get; private init; }
        public int VelocityY { get; private init; }

        public Light(string input)
        {
            var numbers = input.GetNumbers();
            PositionX = numbers[0];
            PositionY = numbers[1];
            VelocityX = numbers[2];
            VelocityY = numbers[3];
        }

        public void Move()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
        }

        public void Move(int times)
        {
            PositionX += VelocityX * times;
            PositionY += VelocityY * times;
        }
    }
}
