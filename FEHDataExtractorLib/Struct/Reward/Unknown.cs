namespace FEHDataExtractorLib.Struct.Reward {
    public class Unknown : Reward {
        public string GuessedType;
        public int    GuessedCount;

        public Unknown(FEHDataExtractorLib.Unknown unknown) {
            this.Type         = RewardType.UNKNOWN;

            this.GuessedType  = unknown.Kind.ToString("X");
            this.GuessedCount = unknown.TheoricalCount;
        }
    }
}
