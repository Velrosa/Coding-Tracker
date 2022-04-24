using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Coding_Tracker;

namespace CodingSessionTracker.UnitTests
{
    [TestClass]
    public class DateValidationTests
    {
        [TestMethod]
        public void IsDateValid_EmptyDate_ReturnsFalse()
        {
            var result = Validation.IsDateValid("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDateValid_InvalidFormat_ReturnsFalse()
        {
            var result = Validation.IsDateValid("15051987");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDateValid_WordsEntered_ReturnsFalse()
        {
            var result = Validation.IsDateValid("Hello World");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsDateValid_MENUReturn_ReturnsTrue()
        {
            var result = Validation.IsDateValid("MENU");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsDateValid_CorrectDate_ReturnsTrue()
        {
            var result = Validation.IsDateValid("15/05/1987 12:00:00");

            Assert.IsTrue(result);
        }
    }
}
