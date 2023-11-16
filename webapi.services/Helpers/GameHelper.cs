namespace webapi.services.Helpers
{
    public static class GameHelper
    {
        private const int _baseFieldPrice = 10;
        private const int _baseOilPumpPrice = 10;
        private const int _baseNextPumpingSeconds = 15;
        private const int _baseBarrels = 1;

        public static int GetFieldPrice(int multiplier)
        {
            return _baseFieldPrice * multiplier;
        }

        public static int GetOilPumpPrice(int multiplier)
        {
            return _baseOilPumpPrice * multiplier;
        }

        public static int GetNextPumpingSeconds(int multiplier)
        {
            return _baseNextPumpingSeconds * multiplier;
        }

        public static int GetBarrels(int multiplier)
        {
            return _baseBarrels * multiplier;
        }
    }
}
