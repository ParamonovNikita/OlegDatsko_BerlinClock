using System;
using System.Globalization;
using System.Linq;
using System.Text;

using BerlinClock.Classes;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private static readonly int expectedOutputCapacity = 28;

        // R - red lamp, Y - yellow lamp, O - turned off
        private static readonly string AllTurnedOnPattern =
@"Y[S:%2]
RRRR[H:5]
RRRR[H:1]
YYRYYRYYRYY[M:5]
YYYY[M:1]";

        public string convertTime(string aTime)
        {
            ITimeParser timeParser = new TimeParser();
            var time = timeParser.Parse(aTime);

            var sb = new StringBuilder(expectedOutputCapacity);

            var patternRows = AllTurnedOnPattern.Split(new[] { "\n" }, StringSplitOptions.None);
            foreach (var row in patternRows.Take(patternRows.Length - 1))
            {
                ParseClockLampRow(sb, time, row);
				sb.Append("\n");
            }
            ParseClockLampRow(sb, time, patternRows.Last());

            return sb.ToString();
        }

        private void ParseClockLampRow(StringBuilder sb, Time time, string patternRow)
        {
            if (string.IsNullOrWhiteSpace(patternRow))
                throw new FormatException("Row is empty.");

            string calculationPattern = GetCalculationPattern(patternRow);
            string pattern = GetPattern(patternRow);

            sb.Append(pattern);

            (int patternValue, bool hasSpecialCharacter) = ParseCalculationPattern(time, calculationPattern);
            if (!hasSpecialCharacter) patternValue = pattern.Length - patternValue;

            if (hasSpecialCharacter && patternValue != 0) sb[sb.Length - 1] = 'O';
            else while (patternValue-- > 0) sb[sb.Length - patternValue - 1] = 'O';
        }

        private static string GetPattern(string pattern)
        {
            return pattern.Substring(0, pattern.IndexOf('['));
        }

        private static string GetCalculationPattern(string pattern)
        {
            var patternStart = pattern.IndexOf('[');
            var patternEnd = pattern.IndexOf(']');

            if (patternStart < 0 || patternEnd < 0 || patternStart >= patternEnd)
                throw new FormatException($"Invalid format in row '{pattern}'.");

            return pattern.Substring(patternStart + 1, patternEnd - patternStart - 1);
        }

        private (int time, bool hasSpecialCharacter) ParseCalculationPattern(Time time, string calculationPattern)
        {
            var splittedPattern = calculationPattern.Split(':');

            if (splittedPattern.Length != 2)
                throw new FormatException("Calculation format. Expected [S].");

            var timePattern = splittedPattern.FirstOrDefault();
            int concreteTime = GetTimeFromTimePattern(time, timePattern);

            var formulaPattern = splittedPattern.LastOrDefault();

            IFormulaExecutor formula = new FormulaExecutor();
            (int formulaResult, int formulaValue, string specialCharacter) = formula.Execute(concreteTime, formulaPattern);

            if (specialCharacter == null) SubtractTime(time, formulaValue * formulaResult, timePattern);

            return (formulaResult, specialCharacter != null);
        }

        private int GetTimeFromTimePattern(Time time, string timePattern)
        {
            int concreteTime;
            switch (timePattern)
            {
                case "H":
                    concreteTime = time.Hours;
                    break;
                case "M":
                    concreteTime = time.Minutes;
                    break;
                case "S":
                    concreteTime = time.Seconds;
                    break;
                default:
                    throw new FormatException($"Unknown character '{timePattern}'");
            }

            return concreteTime;
        }

        private void SubtractTime(Time time, int value, string timePattern)
        {
            switch (timePattern)
            {
                case "H":
                    time.Hours -= value;
                    break;
                case "M":
                    time.Minutes -= value;
                    break;
                case "S":
                    time.Seconds -= value;
                    break;
            }
        }
    }
}
