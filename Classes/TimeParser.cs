using System;
using System.Globalization;

namespace BerlinClock.Classes
{
    internal class TimeParser : ITimeParser
    {
        public Time Parse(string input)
        {
            Time timeModel;

            if (!TimeSpan.TryParseExact(input, "HH:mm:ss", CultureInfo.InvariantCulture, out TimeSpan time))
            {
                var timeParts = input.Split(':');
                if (timeParts.Length != 3)
                    throw new InvalidOperationException("Can't parse input date.");

                timeModel = new Time(int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            }
            else timeModel = new Time(time.Hours, time.Minutes, time.Seconds);

            return timeModel;
        }
    }
}
