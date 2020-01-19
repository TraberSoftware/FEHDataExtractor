namespace FEHDataExtractorLib.Struct.Reward {
    class Blessing : SingleCountDependant {
        public string BlessingType;

        public Blessing(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.BLESSING;
        }

        public Blessing(FEHDataExtractorLib.Blessing blessing) : this(blessing as FEHDataExtractorLib.SingleCountDependant) {
            this.BlessingType = Base.LegendaryElements.getString(blessing.Element - 1);
        }
    }
}
