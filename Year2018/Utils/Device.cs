namespace AdventOfCode.Year2018.Utils;

internal class Device
{
    public int RegisterA { get; set; }
    public int RegisterB { get; private set; }
    public int RegisterC { get; private set; }
    public int RegisterD { get; private set; }
    public int RegisterE { get; private set; }
    public int RegisterF { get; private set; }
    public int? MaxSteps { get; set; }
    public bool StopAtReadOfRegister0 { get; set; }

    private int _instructionPointerRegister = 0;
    private readonly List<Instruction> _code = [];

    public void AddInstruction(string input)
    {
        if (input[0] == '#')
        {
            _instructionPointerRegister = int.Parse(input[4..]);
            return;
        }

        var split = input.Split(' ');
        _code.Add(new(split[0], int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3])));
    }

    public void Calc()
    {
        int nextInstructionIndex = GetValue(_instructionPointerRegister);
        int steps = 0;

        while (nextInstructionIndex < _code.Count)
        {
            var instruction = _code[nextInstructionIndex];

            switch (instruction.Opcode)
            {
                case "addi":
                    AddI(instruction);
                    break;
                case "addr":
                    AddR(instruction);
                    break;
                case "mulr":
                    MulR(instruction);
                    break;
                case "muli":
                    MulI(instruction);
                    break;
                case "banr":
                    BAnR(instruction);
                    break;
                case "bani":
                    BAnI(instruction);
                    break;
                case "borr":
                    BOrR(instruction);
                    break;
                case "bori":
                    BOrI(instruction);
                    break;
                case "setr":
                    SetR(instruction);
                    break;
                case "seti":
                    SetI(instruction);
                    break;
                case "gtir":
                    GTIR(instruction);
                    break;
                case "gtri":
                    GTRI(instruction);
                    break;
                case "gtrr":
                    GTRR(instruction);
                    break;
                case "eqir":
                    EqIR(instruction);
                    break;
                case "eqri":
                    EqRI(instruction);
                    break;
                case "eqrr":
                    EqRR(instruction);
                    break;
                default:
                    throw new NotImplementedException();
            }

            nextInstructionIndex = GetValue(_instructionPointerRegister) + 1;
            SetValue(_instructionPointerRegister, nextInstructionIndex);

            if (MaxSteps.HasValue)
            {
                steps++;
                if (steps >= MaxSteps.Value)
                {
                    break;
                }
            }

            if (StopAtReadOfRegister0 && DidReadRegister0(instruction))
            {
                return;
            }
        }
    }

    private void AddR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) + GetValue(instruction.Value2));
    }

    private void AddI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) + instruction.Value2);
    }

    private void MulR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) * GetValue(instruction.Value2));
    }

    private void MulI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) * instruction.Value2);
    }

    private void BAnR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) & GetValue(instruction.Value2));
    }

    private void BAnI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) & instruction.Value2);
    }

    private void BOrR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) | GetValue(instruction.Value2));
    }

    private void BOrI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) | instruction.Value2);
    }

    private void SetR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1));
    }

    private void SetI(Instruction instruction)
    {
        SetValue(instruction.Value3, instruction.Value1);
    }

    private void GTIR(Instruction instruction)
    {
        SetValue(instruction.Value3, instruction.Value1 > GetValue(instruction.Value2) ? 1 : 0);
    }

    private void GTRI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) > instruction.Value2 ? 1 : 0);
    }

    private void GTRR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) > GetValue(instruction.Value2) ? 1 : 0);
    }

    private void EqIR(Instruction instruction)
    {
        SetValue(instruction.Value3, instruction.Value1 == GetValue(instruction.Value2) ? 1 : 0);
    }

    private void EqRI(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) == instruction.Value2 ? 1 : 0);
    }

    private void EqRR(Instruction instruction)
    {
        SetValue(instruction.Value3, GetValue(instruction.Value1) == GetValue(instruction.Value2) ? 1 : 0);
    }

    private void SetValue(int register, int value)
    {
        switch (register)
        {
            case 0:
                RegisterA = value;
                break;
            case 1:
                RegisterB = value;
                break;
            case 2:
                RegisterC = value;
                break;
            case 3:
                RegisterD = value;
                break;
            case 4:
                RegisterE = value;
                break;
            case 5:
                RegisterF = value;
                break;
            default:
                throw new NotImplementedException();
        }
        ;
    }

    private int GetValue(int register)
    {
        return register switch
        {
            0 => RegisterA,
            1 => RegisterB,
            2 => RegisterC,
            3 => RegisterD,
            4 => RegisterE,
            5 => RegisterF,
            _ => throw new NotImplementedException()
        };

    }

    private static bool DidReadRegister0(Instruction instruction)
    {
        return instruction.Opcode switch
        {
            "addr" or "mulr" or "banr" or "borr" or "gtrr" or "eqrr" => instruction.Value1 == 0 || instruction.Value2 == 0,
            "addi" or "muli" or "bani" or "bori" or "setr" or "gtri" or "eqri" => instruction.Value1 == 0,
            "gtir" or "eqir" => instruction.Value2 == 0,
            _ => false,
        };
    }

    public int GetResultOfCurrentResultRegister()
    {
        return GetValue(_code[_instructionPointerRegister].Value3);
    }

    private readonly record struct Instruction(string Opcode, int Value1, int Value2, int Value3)
    {
        public override string ToString()
        {
            return $"{Opcode} {Value1} {Value2} {Value3}";
        }
    }
}
