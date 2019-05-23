using System;

namespace BerlinClock.Classes
{
    internal class Time
    {
        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }


        private int _hours;
        public int Hours
        {
            get => _hours;
            set
            {
                NotNegativeCheck(value);
                _hours = value;
            }
        }

        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set
            {
                NotNegativeCheck(value);
                _minutes = value;
            }
        }

        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                NotNegativeCheck(value);
                _seconds = value;
            }
        }

        private void NotNegativeCheck(int value)
        {
            if (value < 0)
                throw new InvalidOperationException("Can't set negative time.");
        }
    }
}
