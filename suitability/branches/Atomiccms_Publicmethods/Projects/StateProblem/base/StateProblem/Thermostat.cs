namespace StateProblem
{
    public class Thermostat
    {
        // ReSharper disable InconsistentNaming
        private const int TEMP_THRESHOLD = 18;
        private const int POWER_THRESHOLD = 50;

        public static int thermostat(int temp, int power)
        {
            int heater_on;

            if (temp < TEMP_THRESHOLD)
                heater_on = 1;
            else
                heater_on = 0;

            if (heater_on == 1)
                if (power < POWER_THRESHOLD)
                    heater_on = 0;
            return heater_on;
        }
        // ReSharper restore InconsistentNaming
    }
}
