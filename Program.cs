using AdventOfCode.Year2020;
using AdventOfCode.Utils;

#if DEBUG
checked
{
    Aufgabe24b aufgabe = new();
    Console.WriteLine(aufgabe.Calc());
}
#else
new CompleteCalc().Calc();
#endif