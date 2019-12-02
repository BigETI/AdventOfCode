using System.Collections.Generic;

/// <summary>
/// Advent of Code - Day 2 namespace
/// </summary>
namespace AdventOfCodeDay2
{
    /// <summary>
    /// Intcode class
    /// </summary>
    public class Intcode
    {
        /// <summary>
        /// Intcode
        /// </summary>
        private List<int> intcode = new List<int>();

        /// <summary>
        /// Instruction pointer
        /// </summary>
        public uint InstructionPointer { get; private set; }

        /// <summary>
        /// Array access operator
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns></returns>
        public int this[int index]
        {
            get => intcode[index];
            set => intcode[index] = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="intcode">Intcode</param>
        public Intcode(IEnumerable<int> intcode)
        {
            AddRange(intcode);
        }

        /// <summary>
        /// Add code
        /// </summary>
        /// <param name="code">Code</param>
        public void Add(int code)
        {
            intcode.Add(code);
        }

        /// <summary>
        /// Add intcode range
        /// </summary>
        /// <param name="intcode"></param>
        public void AddRange(IEnumerable<int> intcode)
        {
            if (intcode != null)
            {
                this.intcode.AddRange(intcode);
            }
        }

        /// <summary>
        /// Validate address
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="atAddress">At address</param>
        /// <exception cref="IntcodeInvalidAddressException">Invalid address</exception>
        private void ValidateAddress(int address, uint atAddress)
        {
            if ((address < 0) || (address >= intcode.Count))
            {
                throw new IntcodeInvalidAddressException(address, atAddress);
            }
        }

        /// <summary>
        /// Get arguments
        /// </summary>
        /// <param name="leftAddress">Left address</param>
        /// <param name="rightAddress">Right address</param>
        /// <param name="outputAddress">Output address</param>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        private void GetArguments(out int leftAddress, out int rightAddress, out int outputAddress)
        {
            if ((InstructionPointer + 3) >= intcode.Count)
            {
                throw new IntcodeInsufficientArgumentsException(((uint)(intcode.Count) - InstructionPointer) - 1U);
            }
            leftAddress = intcode[(int)(InstructionPointer + 1U)];
            rightAddress = intcode[(int)(InstructionPointer + 2U)];
            outputAddress = intcode[(int)(InstructionPointer + 3U)];
            ValidateAddress(leftAddress, InstructionPointer + 1U);
            ValidateAddress(rightAddress, InstructionPointer + 2U);
            ValidateAddress(outputAddress, InstructionPointer + 3U);
        }

        /// <summary>
        /// Program step
        /// </summary>
        /// <returns>"true" if instruction was executed, except "false" if opcode was "99"</returns>
        /// <exception cref="IntcodeInvalidOpcodeException">Invalid opcode</exception>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        /// <exception cref="IntcodeInvalidAddressException">Invalid address</exception>
        public bool Step()
        {
            bool ret = false;
            int left_address;
            int right_address;
            int output_address;
            if (InstructionPointer < intcode.Count)
            {
                switch (intcode[(int)InstructionPointer])
                {
                    case 1:
                        GetArguments(out left_address, out right_address, out output_address);
                        intcode[output_address] = intcode[left_address] + intcode[right_address];
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 2:
                        GetArguments(out left_address, out right_address, out output_address);
                        intcode[output_address] = intcode[left_address] * intcode[right_address];
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 99:
                        ++InstructionPointer;
                        break;
                    default:
                        throw new IntcodeInvalidOpcodeException(intcode[(int)InstructionPointer], InstructionPointer);
                }
            }
            return ret;
        }

        /// <summary>
        /// Execute program
        /// </summary>
        /// <exception cref="IntcodeInvalidOpcodeException">Invalid opcode</exception>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        /// <exception cref="IntcodeInvalidAddressException">Invalid address</exception>
        public void Execute()
        {
            while (Step()) ;
        }

        /// <summary>
        /// Reset position
        /// </summary>
        public void ResetPosition()
        {
            InstructionPointer = 0U;
        }

        /// <summary>
        /// Clear intcode
        /// </summary>
        public void ClearIntcode()
        {
            intcode.Clear();
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            ResetPosition();
            ClearIntcode();
        }
    }
}
