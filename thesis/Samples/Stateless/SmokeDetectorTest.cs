using NUnit.Framework;

[TestFixture]
public class SmokeDetectorTest {

	[Test]
	public void AccumulateDetection() {
		int time = 0, offTime = 0, detected = 0;
		bool alarmOn = false, waiting = false;
		for (int i = 1; i <= 4; i++)
		{
			int actual = SmokeDetector.Cycle (0.6,
				ref time, ref offTime, ref detected,
				ref alarmOn, ref waiting);
			Assert.AreEqual (0, actual);
			Assert.AreEqual (i, time);
			Assert.AreEqual (i, detected);
		}
	}

	[Test]
	public void OnAfterFiveThenOffAfterFivePlusTwenty () {
		var detector = new SmokeDetector();

		// 4 seconds at level of 0.6 accumulates detection
		for (int i = 0; i < 4; i++) {
			Assert.AreEqual (0, detector.Cycle (0.6));
		}

		// the 5th second at level 0.6 triggers alarm
		Assert.AreEqual (1, detector.Cycle (0.6));

		// alarm sounds while detection decays
		// through 5 seconds at level 0.1
		for (int i = 0; i < 5; i++) {
			Assert.AreEqual (0, detector.Cycle (0.1));
		}

		// alarm sounds for another 20 seconds
		for (int i = 0; i < 20; i++) {
			Assert.AreEqual (0, detector.Cycle (0.1));
		}

		// 20 seconds without breaching level means
		// it is safe to turn off alarm
		Assert.AreEqual (-1, detector.Cycle (0.1));
	}
}