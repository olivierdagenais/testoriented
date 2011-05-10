public class SmokeDetector {
	public const double Level = 0.3;
	public const int Danger = 5, WaitTime = 20;
	private int time = 0, offTime = 0, detected = 0;
	private bool alarmOn = false, waiting = false;

	public int Cycle(double level) {
		int signal = 0;
		time++;
		if (level > Level && detected < Danger)
			detected++;
		else if (detected > 0)
			detected--;
		
		if (!alarmOn && detected == Danger) {
			alarmOn = true;
			signal = 1;
		}
		
		if (alarmOn) {
			if (!waiting && detected == 0) {
				waiting = true;
				offTime = time + WaitTime;
			}
			if (waiting && detected == Danger)
				waiting = false;
			if (waiting && time > offTime) {
				waiting = false;
				alarmOn = false;
				signal = -1;
			}
		}
		return signal;
	}
}