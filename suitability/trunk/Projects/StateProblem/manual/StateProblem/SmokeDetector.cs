namespace StateProblem
{
    public class SmokeDetector
    {
        // ReSharper disable InconsistentNaming
        private const double LEVEL = 0.3;
        private const int DANGER = 5, WAIT_TIME = 20;

        private static int time = 0, off_time = 0;
        private static int detected = 0, alarm_on = 0, waiting = 0;
        public static void smoke_detector (double level, ref int signal_on, ref int signal_off)
        {
            time ++;

            if (level > LEVEL && detected < DANGER)
                detected++;
            else if (detected > 0)
                detected--;

            if (alarm_on == 0 && detected == DANGER)
            { alarm_on = 1; signal_on = 1;}
            
            if (alarm_on == 1)
            {
                if (waiting == 0 && detected == 0)
                { waiting = 1; off_time = time + WAIT_TIME; }

                if (waiting == 1 && detected == DANGER)
                    waiting = 0;

                if (waiting == 1 && time > off_time)
                { waiting = 0; alarm_on = 0; signal_off = 1; }
            }
        }
        // ReSharper restore InconsistentNaming
    }
}
