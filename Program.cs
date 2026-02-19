using AdventOfCode.Year2020;
using AdventOfCode.Utils;

#if DEBUG
checked
{
    Aufgabe23b aufgabe = new();
    Console.WriteLine(aufgabe.Calc());
}
#else
new CompleteCalc().Calc();
#endif