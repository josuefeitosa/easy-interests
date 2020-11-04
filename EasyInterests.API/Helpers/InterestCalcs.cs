namespace EasyInterests.API.Helpers
{
    public static class InterestCalcs
    {
        public static double SimpleInterest(double value, double interestPercent, double interval)
        {
          return value * (1 + (interestPercent * interval));
        }

        public static double CompoundInterest(double value, double interestPercent, double interval, int i = 1)
        {
          if (i <= interval)
          {
            var newValue = value * (1 + interestPercent);
            i++;

            return CompoundInterest(newValue, interestPercent, interval, i);
          }

          return value;
        }
    }
}
