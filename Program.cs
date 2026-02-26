using AdventOfCode.Year2020;
using AdventOfCode.Utils;

#if DEBUG
Aufgabe25 aufgabe = new();
Console.WriteLine(aufgabe.Calc());
#else
new CompleteCalc().Calc();
#endif