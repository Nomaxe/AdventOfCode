using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018;

internal partial class Aufgabe24 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Unit> _units;

    public Aufgabe24()
    {
        _input = Utilities.ReadInput(2018, 24);
        _units = new(_input.Length - 3);
    }

    public string Calc()
    {
        var army = Army.ImmuneSystem;

        for (int i = 1; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                i++;
                army = Army.Infection;
                continue;
            }

            _units.Add(new(army, _input[i]));
        }

        while (true)
        {
            var armyImmuneSystem = _units.Where(x => x.Army == Army.ImmuneSystem).ToList();
            var armyInfection = _units.Where(x => x.Army == Army.Infection).ToList();

            if (armyImmuneSystem.Count == 0 || armyInfection.Count == 0)
            {
                break;
            }

            foreach (var unit in _units)
            {
                unit.Attacks = null;
            }

            TargetSelection(armyImmuneSystem, armyInfection);
            TargetSelection(armyInfection, armyImmuneSystem);

            foreach (var unit in _units.OrderByDescending(x => x.Initiative))
            {
                if (unit.UnitCount <= 0)
                {
                    continue;
                }

                unit.DoDamage();
            }

            _units.RemoveAll(x => x.UnitCount <= 0);
        }

        return _units.Sum(x => x.UnitCount).ToString();
    }

    private static void TargetSelection(List<Unit> attacker, List<Unit> defender)
    {
        List<Unit> alreadyAttacked = new();

        foreach (var unit in attacker.OrderByDescending(x => x.EffectivePower).ThenBy(x => x.Initiative))
        {
            var otherUnit = defender.Where(x => !alreadyAttacked.Contains(x))
                                    .Where(x => unit.GetDamageTo(x) > 0)
                                    .OrderByDescending(unit.GetDamageTo)
                                    .ThenByDescending(x => x.EffectivePower)
                                    .ThenByDescending(x => x.Initiative)
                                    .FirstOrDefault();

            if (otherUnit != null)
            {
                unit.Attacks = otherUnit;
                alreadyAttacked.Add(otherUnit);
            }
        }
    }

    private partial class Unit
    {
        public Army Army { get; private init; }
        public int UnitCount { get; private set; }
        public int Health { get; private init; }
        public int AttackDamage { get; private init; }
        public AttackType AttackType { get; private init; }
        public AttackType Immune { get; private init; }
        public AttackType Weak { get; private init; }
        public int Initiative { get; private init; }
        public Unit? Attacks { get; set; }

        public int EffectivePower => UnitCount * AttackDamage;

        public Unit(Army army, string input)
        {
            Army = army;
            var numbers = input.GetUnsignedNumbers();
            UnitCount = numbers[0];
            Health = numbers[1];
            AttackDamage = numbers[2];
            Initiative = numbers[3];
            Regex regex = AttackTypeRegex();
            var matches = regex.Matches(input);
            AttackType = Enum.Parse<AttackType>(matches[^1].Value, true);

            if (matches.Count > 1)
            {
                bool weak = input[input.IndexOf('(') + 1] == 'w';

                for (int i = 0; i < matches.Count - 1; i++)
                {
                    if (matches[i].Value == ";")
                    {
                        weak = !weak;
                        continue;
                    }

                    var type = Enum.Parse<AttackType>(matches[i].Value, true);
                    if (weak)
                    {
                        Weak |= type;
                    }
                    else
                    {
                        Immune |= type;
                    }
                }
            }
        }

        public int GetDamageTo(Unit other)
        {
            if (other.Immune.HasFlag(AttackType))
            {
                return 0;
            }

            if (other.Weak.HasFlag(AttackType))
            {
                return EffectivePower * 2;
            }

            return EffectivePower;
        }

        public void DoDamage()
        {
            if (Attacks is null)
            {
                return;
            }

            Attacks.UnitCount -= GetDamageTo(Attacks) / Attacks.Health;
        }

        [GeneratedRegex("bludgeoning|cold|fire|radiation|slashing|;")]
        private static partial Regex AttackTypeRegex();
    }

    private enum Army
    {
        ImmuneSystem,
        Infection
    }

    [Flags]
    private enum AttackType
    {
        None = 0,
        Bludgeoning = 1,
        Cold = 2,
        Fire = 4,
        Radiation = 8,
        Slashing = 16
    }
}
