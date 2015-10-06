namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;

    public class Utils
    {
        #region Static Fields

        public static readonly Dictionary<string, double> Sleeps = new Dictionary<string, double>();

        #endregion

        #region Public Methods and Operators

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static void Sleep(double duration, string name)
        {
            double dur;
            var tick = Environment.TickCount;
            if (!Sleeps.TryGetValue(name, out dur) || dur < tick + duration)
            {
                Sleeps[name] = tick + duration;
            }
        }

        public static bool SleepCheck(string name)
        {
            double asd;
            return !Sleeps.TryGetValue(name, out asd) || Environment.TickCount > asd;
        }

        #endregion
    }
}