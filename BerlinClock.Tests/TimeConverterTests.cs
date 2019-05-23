using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerlinClock.Tests
{
    [TestClass]
    public class TimeConverterTests
    {
        [TestMethod]
        public void ConvertTime000000ShouldConvert()
        {
            var time = "00:00:00";
            var expectedResult = @"Y
OOOO
OOOO
OOOOOOOOOOO
OOOO";

            ITimeConverter timeConverter = new TimeConverter();
            var res = timeConverter.convertTime(time);

            Assert.AreEqual(expectedResult, res);
        }

        [TestMethod]
        public void ConvertTime131701ShouldConvert()
        {
            var time = "13:17:01";
            var expectedResult = @"O
RROO
RRRO
YYROOOOOOOO
YYOO";

            ITimeConverter timeConverter = new TimeConverter();
            var res = timeConverter.convertTime(time);

            Assert.AreEqual(expectedResult, res);
        }

        [TestMethod]
        public void ConvertTime235959ShouldConvert()
        {
            var time = "23:59:59";
            var expectedResult = @"O
RRRR
RRRO
YYRYYRYYRYY
YYYY";

            ITimeConverter timeConverter = new TimeConverter();
            var res = timeConverter.convertTime(time);

            Assert.AreEqual(expectedResult, res);
        }

        [TestMethod]
        public void ConvertTime240000ShouldConvert()
        {
            var time = "24:00:00";
            var expectedResult = @"Y
RRRR
RRRR
OOOOOOOOOOO
OOOO";

            ITimeConverter timeConverter = new TimeConverter();
            var res = timeConverter.convertTime(time);

            Assert.AreEqual(expectedResult, res);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConvertNegativeShouldFail()
        {
            var time = "-23:59:59";

            ITimeConverter timeConverter = new TimeConverter();
            timeConverter.convertTime(time);
        }
    }
}
