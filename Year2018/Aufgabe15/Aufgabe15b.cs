using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2018;

internal class Aufgabe15b : IAufgabe
{
    private Grid _grid;
    private readonly Grid _orginal;
    private readonly List<Unit> _units;
    private int _rounds;
    private int _elvesAttack;

    public Aufgabe15b()
    {
        _orginal = Grid.CreateCharGrid(2018, 15);
        _grid = _orginal;
        _units = new();
        _rounds = 0;
        _elvesAttack = 3;
    }

    public string Calc()
    {
        while (true)
        {
            _grid = _orginal.Clone();
            _units.Clear();
            _rounds = 0;
            _elvesAttack++;

            if (Simulate())
            {
                return (_rounds * _units.Where(x => !x.IsDead).Sum(x => x.Health)).ToString();
            }
        }
    }

    private bool Simulate()
    {
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
                    return true;
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

                enemy.Health -= unit.UnitType == 'E' ? _elvesAttack : 3;
                if (enemy.IsDead)
                {
                    if (enemy.UnitType == 'E')
                    {
                        return false;
                    }

                    _grid.SetValue(enemy.Point, '.');
                }
            }

            _rounds++;
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
