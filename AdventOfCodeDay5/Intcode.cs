using System;
using System.Collections.Generic;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
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
        /// Get parameter
        /// </summary>
        /// <param name="immediateParameterMode">Immediate parameter mode</param>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        /// <returns>Address</returns>
        private int GetParameter(bool immediateParameterMode)
        {
            if ((InstructionPointer + 1) >= intcode.Count)
            {
                throw new IntcodeInsufficientArgumentsException(((uint)(intcode.Count) - InstructionPointer) - 1U);
            }
            int ret = intcode[(int)(InstructionPointer + 1U)];
            if (!immediateParameterMode)
            {
                ValidateAddress(ret, InstructionPointer + 1U);
            }
            return ret;
        }

        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="leftParameter">Left parameter</param>
        /// <param name="rightParameter">Right parameter</param>
        /// <param name="parameterModes">Parameter mode</param>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        private void GetParameters(out int leftParameter, out int rightParameter, byte parameterModes)
        {
            if ((InstructionPointer + 2) >= intcode.Count)
            {
                throw new IntcodeInsufficientArgumentsException(((uint)(intcode.Count) - InstructionPointer) - 1U);
            }
            leftParameter = intcode[(int)(InstructionPointer + 1U)];
            rightParameter = intcode[(int)(InstructionPointer + 2U)];
            if ((parameterModes & 0x1) == 0x0)
            {
                ValidateAddress(leftParameter, InstructionPointer + 1U);
            }
            if ((parameterModes & 0x2) == 0x0)
            {
                ValidateAddress(rightParameter, InstructionPointer + 2U);
            }
        }

        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="leftParameter">Left parameter</param>
        /// <param name="rightParameter">Right parameter</param>
        /// <param name="outputParameter">Output parameter</param>
        /// <param name="parameterModes">Parameter mode</param>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        private void GetParameters(out int leftParameter, out int rightParameter, out int outputParameter, byte parameterModes)
        {
            if ((InstructionPointer + 3) >= intcode.Count)
            {
                throw new IntcodeInsufficientArgumentsException(((uint)(intcode.Count) - InstructionPointer) - 1U);
            }
            leftParameter = intcode[(int)(InstructionPointer + 1U)];
            rightParameter = intcode[(int)(InstructionPointer + 2U)];
            outputParameter = intcode[(int)(InstructionPointer + 3U)];
            if ((parameterModes & 0x1) == 0x0)
            {
                ValidateAddress(leftParameter, InstructionPointer + 1U);
            }
            if ((parameterModes & 0x2) == 0x0)
            {
                ValidateAddress(rightParameter, InstructionPointer + 2U);
            }
            if ((parameterModes & 0x4) == 0x0)
            {
                ValidateAddress(outputParameter, InstructionPointer + 3U);
            }
        }

        /// <summary>
        /// Program step
        /// </summary>
        /// <returns>"true" if instruction was executed, except "false" if opcode was "99"</returns>
        /// <exception cref="IntcodeInvalidOpcodeException">Invalid opcode</exception>
        /// <exception cref="IntcodeInsufficientArgumentsException">Insufficient arguments</exception>
        /// <exception cref="IntcodeInvalidAddressException">Invalid address</exception>
        /// <exception cref="IntcodeInvalidParameterModeException">Invalid parameter mode</exception>
        public bool Step()
        {
            bool ret = false;
            int left_parameter;
            int right_parameter;
            int output_parameter;
            string input;
            int value;
            if (InstructionPointer < intcode.Count)
            {
                int instruction = intcode[(int)InstructionPointer];
                byte parameter_mode = 0;
                if (((instruction % 1000) / 100) == 1)
                {
                    parameter_mode |= 0x1;
                }
                if (((instruction % 10000) / 1000) == 1)
                {
                    parameter_mode |= 0x2;
                }
                if ((instruction / 10000) == 1)
                {
                    parameter_mode |= 0x4;
                }
                switch (instruction % 100)
                {
                    case 1:
                        if ((parameter_mode & 0x4) == 0x4)
                        {
                            throw new IntcodeInvalidParameterModeException(parameter_mode, InstructionPointer);
                        }
                        GetParameters(out left_parameter, out right_parameter, out output_parameter, parameter_mode);
                        intcode[output_parameter] = (((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) + (((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter]);
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 2:
                        if ((parameter_mode & 0x4) == 0x4)
                        {
                            throw new IntcodeInvalidParameterModeException(parameter_mode, InstructionPointer);
                        }
                        GetParameters(out left_parameter, out right_parameter, out output_parameter, parameter_mode);
                        intcode[output_parameter] = (((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) * (((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter]);
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 3:
                        if ((parameter_mode & 0x1) == 0x1)
                        {
                            throw new IntcodeInvalidParameterModeException(parameter_mode, InstructionPointer);
                        }
                        Console.Write("Requesting input: ");
                        input = Console.ReadLine();
                        if (int.TryParse(input, out value))
                        {
                            if ((parameter_mode & 0x1) == 0x0)
                            {
                                intcode[GetParameter(false)] = value;
                            }
                        }
                        InstructionPointer += 2U;
                        ret = true;
                        break;
                    case 4:
                        Console.WriteLine("Output at " + InstructionPointer + ": " + (((parameter_mode & 0x1) == 0x1) ? GetParameter(true) : intcode[GetParameter(false)]));
                        InstructionPointer += 2U;
                        ret = true;
                        break;
                    case 5:
                        GetParameters(out left_parameter, out right_parameter, parameter_mode);
                        if ((((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) != 0)
                        {
                            InstructionPointer = (uint)(((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter]);
                        }
                        else
                        {
                            InstructionPointer += 3U;
                        }
                        ret = true;
                        break;
                    case 6:
                        GetParameters(out left_parameter, out right_parameter, parameter_mode);
                        if ((((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) == 0)
                        {
                            InstructionPointer = (uint)(((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter]);
                        }
                        else
                        {
                            InstructionPointer += 3U;
                        }
                        ret = true;
                        break;
                    case 7:
                        if ((parameter_mode & 0x4) == 0x4)
                        {
                            throw new IntcodeInvalidParameterModeException(parameter_mode, InstructionPointer);
                        }
                        GetParameters(out left_parameter, out right_parameter, out output_parameter, parameter_mode);
                        intcode[output_parameter] = (((((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) < (((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter])) ? 1 : 0);
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 8:
                        if ((parameter_mode & 0x4) == 0x4)
                        {
                            throw new IntcodeInvalidParameterModeException(parameter_mode, InstructionPointer);
                        }
                        GetParameters(out left_parameter, out right_parameter, out output_parameter, parameter_mode);
                        intcode[output_parameter] = (((((parameter_mode & 0x1) == 0x1) ? left_parameter : intcode[left_parameter]) == (((parameter_mode & 0x2) == 0x2) ? right_parameter : intcode[right_parameter])) ? 1 : 0);
                        InstructionPointer += 4U;
                        ret = true;
                        break;
                    case 99:
                        ++InstructionPointer;
                        break;
                    default:
                        throw new IntcodeInvalidOpcodeException(instruction % 100, InstructionPointer);
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
        /// <exception cref="IntcodeInvalidParameterModeException">Invalid parameter mode</exception>
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
