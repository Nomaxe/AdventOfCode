using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe11 : IAufgabe
{
    private readonly string _input;
    private readonly char[] _password;

    public Aufgabe11()
    {
        _input = Utilities.ReadInput(2015, 11)[0];
        _password = _input.ToCharArray();
    }

    public string Calc()
    {
        do
        {
            if (Check())
            {
                return new(_password);
            }

            SetNextPassword();
        } while (true);

        throw new NotImplementedException();
    }

    private void SetNextPassword()
    {
        for (int i = _password.Length - 1; i >= 0; i--)
        {
            if (_password[i] < 'z')
            {
                _password[i]++;
                return;
            }

            _password[i] = 'a';
        }
    }

    private bool Check()
    {
        return Increasing() && Contains() && Pairs();
    }

    private bool Increasing()
    {
        for (int i = 2; i < _password.Length; i++)
        {
            if (_password[i] - _password[i - 2] == 2 && _password[i] - _password[i - 1] == 1)
            {
                return true;
            }
        }

        return false;
    }

    private bool Contains()
    {
        return !_password.Any(x => x == 'i' || x == 'o' || x == 'l');
    }

    private bool Pairs()
    {
        int pairs = 0;

        for (int i = 1; i < _password.Length; i++)
        {
            if (_password[i - 1] == _password[i])
            {
                pairs++;
                i++;
            }
        }

        return pairs >= 2;
    }
}
