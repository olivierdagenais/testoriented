using NUnit.Framework;

namespace StateProblem.ManualTests
{
    /// <summary>
    /// A class to test <see cref="Thermostat"/>.
    /// </summary>
    [TestFixture]
    public class ThermostatTest
    {
        // ReSharper disable InconsistentNaming
        [Test]
        public void thermostat_TwentyDegreesAndFullPower ()
        {
            var heater_on = Thermostat.thermostat (20, 11);
            Assert.AreEqual (0, heater_on);
        }

        [Test]
        public void thermostat_SeventeenDegreesAndFullPower ()
        {
            var heater_on = Thermostat.thermostat (17, 100);
            Assert.AreEqual (1, heater_on);
        }

        [Test]
        public void thermostat_SeventeenDegreesButLessThanHalfPower ()
        {
            var heater_on = Thermostat.thermostat (17, 49);
            Assert.AreEqual (0, heater_on);
        }
        // ReSharper restore InconsistentNaming
    }
}
