using NUnit.Framework;

[TestFixture]
public class SmokeDetectorTest {

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