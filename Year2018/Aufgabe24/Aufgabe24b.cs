using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018;

internal partial class Aufgabe24b : IAufgabe
{
    private readonly List<Unit> _orginal;
    private List<Unit>? _units;

    public Aufgabe24b()
    {
        var input = Utilities.ReadInput(2018, 24);
        _orginal = new(input.Length - 3);

        var army = Army.ImmuneSystem;

        for (int i = 1; i < input.Length; i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                i++;
                army = Army.Infection;
                continue;
            }

            _orginal.Add(new(army, input[i]));
        }
    }

    public string Calc()
    {
        for (int i = 0; i < 20_000; i++)
        {
            if (Calc(i))
            {
                return _units!.Sum(x => x.UnitCount).ToString();
            }
        }

        throw new NotImplementedException();
    }

    public bool Calc(int boost)
    {
        _units = _orginal.Select(x => x.Clone()).ToList();
        foreach (var unit in _units.Where(x => x.Army == Army.ImmuneSystem))
        {
            unit.AttackDamage += boost;
        }

        while (true)
        {
            var armyImmuneSystem = _units.Where(x => x.Army == Army.ImmuneSystem).ToList();
            var armyInfection = _units.Where(x => x.Army == Army.Infection).ToList();

            if (armyInfection.Count == 0)
            {
                return true;
            }
            if (armyImmuneSystem.Count == 0)
            {
                return false;
            }

            foreach (var unit in _units)
            {
                unit.Attacks = null;
            }

            var targetSelectionImmuneSystem = TargetSelection(armyImmuneSystem, armyInfection);
            var targetSelectionInfection = TargetSelection(armyInfection, armyImmuneSystem);

            if (!targetSelectionImmuneSystem && !targetSelectionInfection)
            {
                return false;
            }

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
    }

    private static bool TargetSelection(List<Unit> attacker, List<Unit> defender)
    {
        List<Unit> alreadyAttacked = new();
        bool someoneAttacks = false;

        foreach (var unit in attacker.OrderByDescending(x => x.EffectivePower).ThenByDescending(x => x.Initiative))
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

                if (!someoneAttacks && unit.GetDamageTo(otherUnit) >= otherUnit.Health)
                {
                    someoneAttacks = true;
                }
            }
        }

        return someoneAttacks;
    }

    private partial class Unit
    {
        public Army Army { get; private init; }
        public int UnitCount { get; private set; }
        public int Health { get; private init; }
        public int AttackDamage { get; set; }
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

        public Unit Clone()
        {
            return (Unit)MemberwiseClone();
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
