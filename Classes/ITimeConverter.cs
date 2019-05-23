using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public interface ITimeConverter
    {
        String convertTime(String aTime);
    }
}
