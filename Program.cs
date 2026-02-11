using AdventOfCode.Year2018;
using AdventOfCode.Utils;

#if DEBUG
checked
{
    Aufgabe12 aufgabe = new();
    Console.WriteLine(aufgabe.Calc());
}
#else
new CompleteCalc().Calc();
#endif