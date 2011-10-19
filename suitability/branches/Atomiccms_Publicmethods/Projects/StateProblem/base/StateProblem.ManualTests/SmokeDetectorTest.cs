using NUnit.Framework;
using StateProblem;

namespace StateProblem.ManualTests
{
    /// <summary>
    /// A class to test <see cref="SmokeDetector"/>.
    /// </summary>
    [TestFixture]
    public class SmokeDetectorTest
    {
        // ReSharper disable InconsistentNaming
        [Test]
        public void smoke_detector_IdleAtSafeLevels ()
        {
            SmokeDetector.Reset ();
            int signal_on, signal_off;

            // 60 seconds at level of 0.1 should be peaceful
            for (int i = 0; i < 60; i++)
            {
                signal_off = 0; signal_on = 0;
                SmokeDetector.smoke_detector (0.1, ref signal_on, ref signal_off);
                Assert.AreEqual (0, signal_off, "Iteration #" + i);
                Assert.AreEqual (0, signal_on, "Iteration #" + i);
            }
        }

        [Test]
        public void smoke_detector_OnAfterFiveThenOffAfterFivePlusTwenty ()
        {
            SmokeDetector.Reset ();
            int signal_on = 0, signal_off = 0;

            // 4 seconds at level of 0.6 accumulates detection
            for (int i = 0; i < 4; i++)
            {
                signal_off = 0; signal_on = 0;
                SmokeDetector.smoke_detector (0.6, ref signal_on, ref signal_off);
                Assert.AreEqual (0, signal_off, "Iteration #" + i);
                Assert.AreEqual (0, signal_on, "Iteration #" + i);

            }
            // the 5th second at level 0.6 triggers alarm
            signal_off = 0; signal_on = 0;
            SmokeDetector.smoke_detector (0.6, ref signal_on, ref signal_off);
            Assert.AreEqual (0, signal_off);
            Assert.AreEqual (1, signal_on);

            // alarm sounds while detection decays through 5 seconds at level 0.1
            for (int i = 0; i < 5; i++)
            {
                signal_off = 0; signal_on = 0;
                SmokeDetector.smoke_detector (0.1, ref signal_on, ref signal_off);
                Assert.AreEqual (0, signal_off, "Iteration #" + i);
                Assert.AreEqual (0, signal_on, "Iteration #" + i);

            }
            // alarm sounds for another 20 seconds
            for (int i = 0; i < 20; i++)
            {
                signal_off = 0; signal_on = 0;
                SmokeDetector.smoke_detector (0.1, ref signal_on, ref signal_off);
                Assert.AreEqual (0, signal_off, "Iteration #" + i);
                Assert.AreEqual (0, signal_on, "Iteration #" + i);
            }
            // 20 seconds without breaching level means it is safe to turn off alarm
            signal_off = 0; signal_on = 0;
            SmokeDetector.smoke_detector (0.1, ref signal_on, ref signal_off);
            Assert.AreEqual (1, signal_off);
            Assert.AreEqual (0, signal_on);
        }
        // ReSharper restore InconsistentNaming
    }
}