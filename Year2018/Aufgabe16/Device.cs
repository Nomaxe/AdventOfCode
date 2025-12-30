namespace AdventOfCode.Year2018.Namespace16;

internal class Device
{
    public int RegisterA { get; private set; }
    public int RegisterB { get; private set; }
    public int RegisterC { get; private set; }
    public int RegisterD { get; private set; }

    private readonly List<Instruction> _code = [];

    public void AddInstruction(string opcode, int value1, int value2, int value3)
    {
        _code.Add(new(opcode, value1, value2, value3));
    }

    public void Calc()
    {
        foreach (var instruction in _code)
        {
            switch (instruction.Opcode.ToLower())
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
            ;
        }
    }

    public List<string> GetPossibleOpcodes(int value1, int value2, int value3,
                                           int registerA, int registerB, int registerC, int registerD,
                                           int resultA, int resultB, int resultC, int resultD)
    {
        Instruction instruction = new("0", value1, value2, value3);
        List<Action<Instruction>> opcodes = [AddR, AddI, MulR, MulI, BAnR, BAnI, BOrR, BOrI, SetR, SetI, GTIR, GTRI, GTRR, EqIR, EqRI, EqRR];
        List<string> possibleOpcodes = [];

        foreach (var opcode in opcodes)
        {
            RegisterA = registerA;
            RegisterB = registerB;
            RegisterC = registerC;
            RegisterD = registerD;

            opcode(instruction);

            if (RegisterA == resultA && RegisterB == resultB && RegisterC == resultC && RegisterD == resultD)
            {
                possibleOpcodes.Add(opcode.Method.Name);
            }
        }

        return possibleOpcodes;
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
            _ => throw new NotImplementedException()
        };

    }

    private readonly record struct Instruction(string Opcode, int Value1, int Value2, int Value3)
    {

    }
}
