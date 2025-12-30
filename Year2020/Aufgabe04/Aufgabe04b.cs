using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020;

internal partial class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe04b()
    {
        _input = Utilities.ReadInput(2020, 4);
    }

    public string Calc()
    {
        int count = 0;
        int currentCount = 0;

        foreach (var line in _input.Append(string.Empty))
        {
            if (string.IsNullOrEmpty(line))
            {
                if (currentCount == 7)
                {
                    count++;
                }

                currentCount = 0;
                continue;
            }

            var split = line.Split(' ');
            foreach (var item in split)
            {
                var field = item[0..3];
                switch (field)
                {
                    case "byr":
                        {
                            var year = int.Parse(item[^4..]);
                            if (year < 1920 || year > 2002)
                            {
                                continue;
                            }
                        }
                        break;
                    case "iyr":
                        {
                            var year = int.Parse(item[^4..]);
                            if (year < 2010 || year > 2020)
                            {
                                continue;
                            }
                        }
                        break;
                    case "eyr":
                        {
                            var year = int.Parse(item[^4..]);
                            if (year < 2020 || year > 2030)
                            {
                                continue;
                            }
                        }
                        break;
                    case "hgt":
                        {
                            var unit = item[^2..];
                            if (item.Length <= 6)
                            {
                                continue;
                            }

                            var value = int.Parse(item[4..^2]);
                            switch (unit)
                            {
                                case "cm":
                                    if (value < 150 || value > 193)
                                    {
                                        continue;
                                    }
                                    break;
                                case "in":
                                    if (value < 59 || value > 76)
                                    {
                                        continue;
                                    }
                                    break;
                                default:
                                    continue;
                            }
                        }
                        break;
                    case "hcl":
                        if (!HairColorRegex().IsMatch(line[4..]))
                        {
                            continue;
                        }
                        break;
                    case "ecl":
                        {
                            var value = item[4..];
                            switch (value)
                            {
                                case "amb":
                                case "blu":
                                case "brn":
                                case "gry":
                                case "grn":
                                case "hzl":
                                case "oth":
                                    break;
                                default:
                                    continue;
                            }
                        }
                        break;
                    case "pid":
                        {
                            var value = item[4..];
                            if (value.Length != 9 || value.Any(x => !char.IsAsciiDigit(x)))
                            {
                                continue;
                            }
                        }
                        break;
                    default:
                        continue;
                }

                currentCount++;
            }
        }

        return count.ToString();
    }

    [GeneratedRegex(@"#([\da-f]{6})")]
    private static partial Regex HairColorRegex();
}
