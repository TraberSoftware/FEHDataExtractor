namespace FEHDataExtractorLib.Struct.Reward {
    public class SingleCountDependant : Reward {
        public string Name;
        public int    Count;

        public SingleCountDependant(FEHDataExtractorLib.SingleCountDependant singleCountDependant) {
            if(this.Type == RewardType.NONE) {
                this.Type = RewardType.SINGLE_COUNT_DEPENDANT;
            }

            this.Count = singleCountDependant.Count;
            this.Name  = Base.Rewards.getString(singleCountDependant.Kind);
        }
    }
}
