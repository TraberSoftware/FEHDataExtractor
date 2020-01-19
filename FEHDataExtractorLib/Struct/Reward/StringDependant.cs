namespace FEHDataExtractorLib.Struct.Reward {
    public class StringDependant : Reward {
        public string Name;
        public string Raw;
        public int    Length;

        public StringDependant(FEHDataExtractorLib.StringDependant stringDependant) {
            if (this.Type == RewardType.NONE) {
                this.Type = RewardType.SINGLE_COUNT_DEPENDANT;
            }

            this.Raw    = stringDependant.Rew;
            this.Length = stringDependant.Length;
            this.Name   = Util.GetString(this.Raw);
        }
    }
}
