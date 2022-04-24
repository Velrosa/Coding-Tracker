using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Coding_Tracker;

namespace CodingSessionTracker.UnitTests
{
    [TestClass]
    public class NumberValidationTests
    {
        [TestMethod]
        public void IsNumberValid_EmptyNumber_ReturnsFalse()
        {
            var result = Validation.IsNumberValid("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsNumberValid_IsLetters_ReturnsFalse()
        {
            var result = Validation.IsNumberValid("Hello World");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsNumberValid_IsNegative_ReturnsFalse()
        {
            var result = Validation.IsNumberValid("-6");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsNumberValid_MENUReturn_ReturnsTrue()
        {
            var result = Validation.IsNumberValid("MENU");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNumberValid_CorrectNumber_ReturnsTrue()
        {
            var result = Validation.IsNumberValid("6");

            Assert.IsTrue(result);
        }
    }
}
