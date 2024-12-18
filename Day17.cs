public class Day17() : Day<string, long>(17)
{
    private readonly List<int> Program = [2, 4, 1, 5, 7, 5, 4, 3, 1, 6, 0, 3, 5, 5, 3, 0];
    private int RegisterA = 61156655;
    private int RegisterB = 0;
    private int RegisterC = 0;

    private int InstructionPointer = 0;

    private List<int> Output = [];

    public override string RunPart1()
    {
        while (InstructionPointer < Program.Count)
        {
            Do();
        }

        return string.Join(',', Output);
    }


    public override long RunPart2()
    {
        var result = string.Join(',', Program);
        var regA = 0;
        bool resFound = false;
        while (!resFound && regA < int.MaxValue)
        {
            Reset(regA);
            while (InstructionPointer < Program.Count)
            {
                Do();
            }

            if (string.Join(',', Output) == result)
            {
                resFound = true;
            }
            else
            {
                regA += 8;
            }
        }

        return regA;
    }

    private void Reset(int regA)
    {
        InstructionPointer = 0;
        RegisterA = regA;
        RegisterB = 0;
        RegisterC = 0;
        Output.Clear();
    }

    private void Do()
    {
        var opCode = Program[InstructionPointer];
        var comboOperand = Program[InstructionPointer + 1];

        switch (opCode)
        {
            case 0:
                ADV(GetComboValue(comboOperand));
                break;
            case 1:
                BXL(comboOperand);
                break;
            case 2:
                BST(GetComboValue(comboOperand));
                break;
            case 3:
                JNZ(comboOperand);
                break;
            case 4:
                BXC();
                break;
            case 5:
                OUT(GetComboValue(comboOperand));
                break;
            case 6:
                BDV(GetComboValue(comboOperand));
                break;
            case 7:
                CDV(GetComboValue(comboOperand));
                break;
            default:
                throw new Exception("Invalid opcode");
        }
        if (opCode != 3)
        {
            InstructionPointer += 2;
        }
    }

    private int GetComboValue(int comboOperand)
    {
        return comboOperand switch
        {
            0 or 1 or 2 or 3 => comboOperand,
            4 => RegisterA,
            5 => RegisterB,
            6 => RegisterC,
            _ => throw new Exception("Invalid")
        };
    }

    private void ADV(int comboValue)
    {
        RegisterA /= (int)Math.Pow(2, comboValue);
    }

    private void BXL(int literalValue)
    {
        RegisterB ^= literalValue;
    }

    private void BST(int comboValue)
    {
        RegisterB = comboValue % 8;
    }

    private void JNZ(int literalValue)
    {
        if (RegisterA == 0)
        {
            InstructionPointer += 2;
            return;
        }
        InstructionPointer = literalValue;
    }

    private void BXC()
    {
        RegisterB ^= RegisterC;
    }

    private void OUT(int comboValue)
    {
        Output.Add(comboValue % 8);
    }

    private void BDV(int comboValue)
    {
        RegisterB = RegisterA / (int)Math.Pow(2, comboValue);
    }
    private void CDV(int comboValue)
    {
        RegisterC = RegisterA / (int)Math.Pow(2, comboValue);
    }
}