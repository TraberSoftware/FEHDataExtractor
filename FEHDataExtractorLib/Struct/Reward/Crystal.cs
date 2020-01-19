namespace FEHDataExtractorLib.Struct.Reward {
    public class Crystal : SingleCountDependant {
        public string CrystalType;
        public string Color;

        public Crystal(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.CRYSTAL;
        }

        public Crystal(FEHDataExtractorLib.Crystal crystal) : this(crystal as FEHDataExtractorLib.SingleCountDependant) {
            this.CrystalType = crystal.Is_Crystal == 1 ? "GEM" : "SHARD";
            this.Color       = Base.ShardColors.getString(crystal.Color);
        }
    }
}
