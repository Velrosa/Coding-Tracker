using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Coding_Tracker;

namespace CodingSessionTracker.UnitTests
{
    [TestClass]
    public class DurationValidationTests
    {
        [TestMethod]
        public void IsDurationValid_InvalidTimeSpan_ReturnsNull()
        {
            string startTime = DateTime.Now.ToString();
            string endTime = DateTime.Now.AddMinutes(60).ToString();

            var result = Validation.IsDurationValid(endTime, startTime);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsDurationValid_CorrectDuration_ReturnsDuration()
        {
            string startTime = DateTime.Now.ToString();
            string endTime = DateTime.Now.AddMinutes(60).ToString();
            string duration = (DateTime.Parse(endTime) - DateTime.Parse(startTime)).ToString();

            var result = Validation.IsDurationValid(startTime, endTime);

            Assert.AreEqual(duration, result);
        }
    }
}
