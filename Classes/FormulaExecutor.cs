using System;
using System.Linq;

namespace BerlinClock.Classes
{
    internal sealed class FormulaExecutor : IFormulaExecutor
    {
        private static readonly string[] SpecialCharacters = { "%" };

        public (int result, int formulaValue, string specialCharacter) Execute(int time, string formula)
        {
            string specialCharacter = SpecialCharacters.FirstOrDefault(x => formula.Contains(x));

            if (specialCharacter != null)
            {
                formula = formula.Replace(specialCharacter, "");
            }

            if (int.TryParse(formula, out int value))
            {
                var result = specialCharacter != null
                    ? SpecialCharacterExecution(specialCharacter, time, value)
                    : time / value;

                return (result, value, specialCharacter);
            }
            else throw new InvalidCastException("Integer expected");
        }

        private int SpecialCharacterExecution(string specialCharacter, int time, int value)
        {
            switch (specialCharacter)
            {
                case "%":
                    return time % value;
                default:
                    return time;
            }
        }
    }
}
