using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe22 : IAufgabe
{
    private readonly int _bossStartHealth;
    private readonly int _bossDamage;
    private int _minMana;

    public Aufgabe22()
    {
        var input = Utilities.ReadInput(2015, 22);
        _bossStartHealth = input[0].GetNumbers()[0];
        _bossDamage = input[1].GetNumbers()[0];
        _minMana = int.MaxValue;
    }

    public string Calc()
    {
        DoPlayerTurn(_bossStartHealth, 50, 500, 0, null, null, null);

        return _minMana.ToString();
    }

    private void DoPlayerTurn(int bossHealth, int currentHealth, int currentMana, int manaSpend, Effect? shield, Effect? poison, Effect? recharge)
    {
        if (currentMana < 53)
        {
            return;
        }

        shield = shield?.DoTurn();
        poison = poison?.DoTurn();
        recharge = recharge?.DoTurn();

        if (poison.HasValue)
        {
            bossHealth -= 3;

            if (bossHealth <= 0)
            {
                _minMana = int.Min(_minMana, manaSpend);
                return;
            }
        }

        if (recharge.HasValue)
        {
            currentMana += 101;
        }

        //MagicMissile
        DoBossTurn(bossHealth - 4, currentHealth, currentMana - 53, manaSpend + 53, shield, poison, recharge);

        //Drain
        if (currentMana >= 73)
        {
            DoBossTurn(bossHealth - 2, currentHealth + 2, currentMana - 73, manaSpend + 73, shield, poison, recharge);
        }

        //Armor
        if (currentMana >= 113 && (!shield.HasValue || shield.Value.CanCast))
        {
            DoBossTurn(bossHealth, currentHealth, currentMana - 113, manaSpend + 113, new(6), poison, recharge);
        }

        //Poison
        if (currentMana >= 173 && (!poison.HasValue || poison.Value.CanCast))
        {
            DoBossTurn(bossHealth, currentHealth, currentMana - 173, manaSpend + 173, shield, new(6), recharge);
        }

        //Recharge
        if (currentMana >= 229 && (!recharge.HasValue || recharge.Value.CanCast))
        {
            DoBossTurn(bossHealth, currentHealth, currentMana - 229, manaSpend + 229, shield, poison, new(5));
        }
    }

    private void DoBossTurn(int bossHealth, int currentHealth, int currentMana, int manaSpend, Effect? shield, Effect? poison, Effect? recharge)
    {
        if (manaSpend >= _minMana)
        {
            return;
        }

        if (bossHealth <= 0)
        {
            _minMana = int.Min(_minMana, manaSpend);
            return;
        }

        shield = shield?.DoTurn();
        poison = poison?.DoTurn();
        recharge = recharge?.DoTurn();

        int armor = 0;

        if (shield.HasValue)
        {
            armor = 7;
        }

        if (poison.HasValue)
        {
            bossHealth -= 3;

            if (bossHealth <= 0)
            {
                _minMana = int.Min(_minMana, manaSpend);
                return;
            }
        }

        if (recharge.HasValue)
        {
            currentMana += 101;
        }

        currentHealth -= _bossDamage - armor;

        if (currentHealth <= 0)
        {
            return;
        }

        DoPlayerTurn(bossHealth, currentHealth, currentMana, manaSpend, shield, poison, recharge);
    }

    private readonly struct Effect(int timer)
    {
        public bool CanCast => timer == 0;

        public Effect? DoTurn()
        {
            if (timer > 0)
            {
                return new(timer - 1);
            }

            return null;
        }
    }
}
