using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2018;

internal class Aufgabe15 : IAufgabe
{
    private readonly Grid _grid;
    private readonly List<Unit> _units;

    public Aufgabe15()
    {
        _grid = Grid.CreateCharGrid(2018, 15);
        _units = new();
    }

    public string Calc()
    {
        int rounds = 0;

        AddUnits('E');
        AddUnits('G');

        while (true)
        {
            RemoveDeadUnits();
            _units.Sort();

            foreach (var unit in _units)
            {
                if (unit.IsDead)
                {
                    continue;
                }

                if (!AreEnemiesAlive(unit.UnitType))
                {
                    return (rounds * _units.Where(x => !x.IsDead).Sum(x => x.Health)).ToString();
                }


                var enemy = GetInRangeEnemy(unit);
                if (enemy is null)
                {
                    CompleteSolver solver = new(_grid);
                    solver.AddWallCharacter(unit.UnitType);
                    solver.SolveLabyrinth(unit.Point);

                    enemy = GetNextEnemy(unit, solver);
                    if (enemy is null)
                    {
                        continue;
                    }

                    var point = solver.GetFirstStepTo(enemy.Point).Order().First();
                    _grid.SetValue(unit.Point, '.');
                    unit.Point = point;
                    _grid.SetValue(unit.Point, unit.UnitType);

                    enemy = GetInRangeEnemy(unit);
                }

                if (enemy is null)
                {
                    continue;
                }

                enemy.Health -= 3;
                if (enemy.IsDead)
                {
                    _grid.SetValue(enemy.Point, '.');
                }
            }

            rounds++;
        }
    }

    private void AddUnits(char unitType)
    {
        foreach (var point in _grid.GetPointsOfValue(unitType))
        {
            _units.Add(new(unitType, point));
        }
    }

    private void RemoveDeadUnits()
    {
        _units.RemoveAll(x => x.IsDead);
    }

    private Unit? GetInRangeEnemy(Unit unit)
    {
        return _units.Where(x => !x.IsDead && x.UnitType != unit.UnitType && unit.Point.IsNeighbour(x.Point)).OrderBy(x => x.Health).FirstOrDefault();
    }

    private Unit? GetNextEnemy(Unit unit, CompleteSolver solver)
    {
        var minLength = int.MaxValue;
        Unit? nextEnemy = null;

        foreach (var enemy in _units.Where(x => !x.IsDead && unit.UnitType != x.UnitType))
        {
            if (solver.TryGetLength(enemy.Point, out var length))
            {
                if (length < minLength)
                {
                    minLength = length;
                    nextEnemy = enemy;
                }
                else if (length == minLength)
                {
                    if (enemy.CompareTo(nextEnemy) < 0)
                    {
                        nextEnemy = enemy;
                    }
                }
            }
        }

        return nextEnemy;
    }

    private bool AreEnemiesAlive(char unitType)
    {
        return _units.Any(x => x.UnitType != unitType && !x.IsDead);
    }

    private class Unit : IComparable<Unit>
    {
        public char UnitType { get; private init; }
        public int Health { get; set; }
        public Point Point { get; set; }

        public bool IsDead => Health <= 0;

        public Unit(char unitType, Point point)
        {
            UnitType = unitType;
            Health = 200;
            Point = point;
        }

        public int CompareTo(Unit? other)
        {
            return Point.CompareTo(other!.Point);
        }
    }
}
