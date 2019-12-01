using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Advent of Code - Day 1 namespace
/// </summary>
namespace AdventOfCodeDay1
{
    /// <summary>
    /// Fuel calculator class
    /// </summary>
    public class FuelCalculator
    {
        /// <summary>
        /// Calculate with fuel mass
        /// </summary>
        private bool calculateWithFuelMass;

        /// <summary>
        /// Input mass
        /// </summary>
        private List<int> inputMass = new List<int>();

        /// <summary>
        /// Fuel
        /// </summary>
        private int? fuel;

        /// <summary>
        /// Input mass
        /// </summary>
        public IReadOnlyList<int> InputMass => inputMass;


        /// <summary>
        /// Calculate with fuel mass
        /// </summary>
        public bool CalculateWithFuelMass
        {
            get => calculateWithFuelMass;
            set
            {
                if (calculateWithFuelMass != value)
                {
                    calculateWithFuelMass = value;
                    fuel = null;
                }
            }
        }

        /// <summary>
        /// Fuel
        /// </summary>
        public int Fuel
        {
            get
            {
                if (fuel == null)
                {
                    int result = 0;
                    Parallel.ForEach(inputMass, (input_mass) =>
                    {
                        if (calculateWithFuelMass)
                        {
                            int partial_mass = input_mass;
                            do
                            {
                                partial_mass = Math.Max((partial_mass / 3) - 2, 0);
                                Interlocked.Add(ref result, partial_mass);
                            }
                            while (partial_mass > 0);
                        }
                        else
                        {
                            Interlocked.Add(ref result, Math.Max((input_mass / 3) - 2, 0));
                        }
                    });
                    fuel = result;
                }
                return fuel.Value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="calculateWithFuelMass">Calculate with fuel mass</param>
        public FuelCalculator(bool calculateWithFuelMass)
        {
            CalculateWithFuelMass = calculateWithFuelMass;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="fuelCalculator">Fuel calculator</param>
        public FuelCalculator(FuelCalculator fuelCalculator)
        {
            AddRange(fuelCalculator.inputMass);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputMass">Input mass</param>
        public FuelCalculator(IEnumerable<int> inputMass)
        {
            AddRange(inputMass);
        }

        /// <summary>
        /// Add mass
        /// </summary>
        /// <param name="mass">Mass</param>
        public void Add(int mass)
        {
            inputMass.Add(mass);
            fuel = null;
        }

        /// <summary>
        /// Add mass range
        /// </summary>
        /// <param name="mass">Mass</param>
        public void AddRange(IEnumerable<int> mass)
        {
            if (mass != null)
            {
                inputMass.AddRange(mass);
                fuel = null;
            }
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            inputMass.Clear();
            fuel = null;
        }
    }
}
