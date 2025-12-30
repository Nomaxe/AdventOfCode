using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe21 : IAufgabe
{
    private readonly double _bossHealth;
    private readonly double _bossDamage;
    private readonly double _bossArmor;
    private int _minGold;
    private readonly Stats[] _weapons;
    private readonly Stats[] _armor;
    private readonly Stats[] _rings;

    public Aufgabe21()
    {
        var input = Utilities.ReadInput(2015, 21);
        _bossHealth = input[0].GetNumbers()[0];
        _bossDamage = input[1].GetNumbers()[0];
        _bossArmor = input[2].GetNumbers()[0];
        _minGold = int.MaxValue;
        _weapons =
        [
            new(4, 0, 8),
            new(5, 0, 10),
            new(6, 0, 25),
            new(7, 0, 40),
            new(8, 0, 74)
        ];
        _armor =
        [
            new(0, 1, 13),
            new(0, 2, 31),
            new(0, 3, 53),
            new(0, 4, 75),
            new(0, 5, 102)
        ];
        _rings =
        [
            new(0, 1, 20),
            new(1, 0, 25),
            new(0, 2, 40),
            new(2, 0, 50),
            new(0, 3, 80),
            new(3, 0, 100)
        ];
    }

    public string Calc()
    {
        CheckWeapon();

        return _minGold.ToString();
    }

    private void CheckWeapon()
    {
        foreach (var weapon in _weapons)
        {
            if (weapon.Gold >= _minGold)
            {
                return;
            }

            Check(weapon.Damage, weapon.Armor, weapon.Gold);

            CheckArmor(weapon);
        }
    }

    private void CheckArmor(Stats weapon)
    {
        CheckRing1(weapon.Damage, weapon.Armor, weapon.Gold);

        foreach (var armor in _armor)
        {
            var gold = weapon.Gold + armor.Gold;

            if (gold >= _minGold)
            {
                return;
            }

            Check(weapon.Damage, armor.Armor, gold);

            CheckRing1(weapon.Damage, armor.Armor, gold);
        }
    }

    private void CheckRing1(int damage, int armor, int gold)
    {
        for (int i = 0; i < _rings.Length; i++)
        {
            var ring = _rings[i];
            var currentGold = gold + ring.Gold;

            if (currentGold >= _minGold)
            {
                return;
            }

            var currentDamage = damage + ring.Damage;
            var currentArmor = armor + ring.Armor;

            Check(currentDamage, currentArmor, currentGold);

            CheckRing2(currentDamage, currentArmor, currentGold, i);
        }
    }

    private void CheckRing2(int damage, int armor, int gold, int offset)
    {
        for (int i = offset + 1; i < _rings.Length; i++)
        {
            var ring = _rings[i];
            var currentGold = gold + ring.Gold;

            if (currentGold >= _minGold)
            {
                return;
            }

            var currentDamage = damage + ring.Damage;
            var currentArmor = armor + ring.Armor;

            Check(currentDamage, currentArmor, currentGold);
        }
    }

    private void Check(int damage, int armor, int gold)
    {
        var playerRounds = Math.Round(_bossHealth / (damage - _bossArmor), MidpointRounding.ToPositiveInfinity);
        var bossRounds = Math.Round(100 / (_bossDamage - armor), MidpointRounding.ToPositiveInfinity);

        if (playerRounds <= bossRounds)
        {
            _minGold = gold;
        }
    }

    private readonly struct Stats(int damage, int armor, int gold)
    {
        public int Damage { get; private init; } = damage;
        public int Armor { get; private init; } = armor;
        public int Gold { get; private init; } = gold;
    }
}
