using System.Collections.Generic;
using HappyPortal.Lib.Services;
using NUnit.Framework;

namespace HappyPortal.UnitTests
{
    [TestFixture]
    public class HappyIndexCalculatorTest
    {
        [Test]
        public void CalculateIndex_Success()
        {
            List<int> indicators = new List<int>();

            indicators.Clear();
            indicators.Add(1);
            Assert.AreEqual(100,HappyIndexCalculator.Calculate(indicators));

            indicators.Clear();
            indicators.Add(1);
            indicators.Add(1);
            indicators.Add(1);
            indicators.Add(1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(33, HappyIndexCalculator.Calculate(indicators));

            indicators.Clear();
            indicators.Add(1);
            indicators.Add(1);
            indicators.Add(1);
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(0, HappyIndexCalculator.Calculate(indicators));

            indicators.Clear();
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
        }

        [Test]
        public void CalculateNegativeIndex_Success()
        {
            //test proves that no matter how much you subtract form zero the index is -100
            List<int> indicators = new List<int>();

            indicators.Clear();
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
            indicators.Add(-1);
            indicators.Add(-1);
            indicators.Add(-1);
            Assert.AreEqual(-100, HappyIndexCalculator.Calculate(indicators));
        }
    }
}
