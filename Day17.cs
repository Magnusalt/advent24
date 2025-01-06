public class Day17() : Day<string, long>(17)
{
    private readonly List<int> Program = [2, 4, 1, 5, 7, 5, 4, 3, 1, 6, 0, 3, 5, 5, 3, 0];
    private long RegisterA = 1024;
    private long RegisterB = 0;
    private long RegisterC = 0;

    private int InstructionPointer = 0;

    private List<long> Output = [];

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

        long[] output = new long[16];
        long startA = (long)Math.Pow(8, 15);

        var pow = 14;

        while (!output.Zip(Program, (a, b) => a == b).All(c => c))
        {
            var a = startA;
            output = new long[16];
            var i = 0;
            while (a > 0)
            {
                var b = a % 8;
                b ^= 5;
                var c = a / (long)Math.Pow(2, b);
                b ^= c;
                b ^= 6;

                a /= 8;
                var o = b % 8;

                output[i] = o;

                i++;
            }

            if (pow > 0 && output[pow..].Zip(Program[pow..], (a, b) => a == b).All(c => c))
            {
                while (output[pow] == Program[pow])
                {
                    pow--;
                }
            }

            startA += (long)Math.Pow(8, pow);
        }

        return startA - 1;
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

    private long GetComboValue(int comboOperand)
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

    private void ADV(long comboValue)
    {
        RegisterA /= (int)Math.Pow(2, comboValue);
    }

    private void BXL(int literalValue)
    {
        RegisterB ^= literalValue;
    }

    private void BST(long comboValue)
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

    private void OUT(long comboValue)
    {
        Output.Add(comboValue % 8);
    }

    private void BDV(long comboValue)
    {
        RegisterB = RegisterA / (int)Math.Pow(2, comboValue);
    }
    private void CDV(long comboValue)
    {
        RegisterC = RegisterA / (long)Math.Pow(2, comboValue);
    }
}