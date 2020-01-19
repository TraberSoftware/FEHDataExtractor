namespace FEHDataExtractorLib.Struct.Reward {
    public class Hero : StringDependant {
        public int Rarity;

        public Hero(FEHDataExtractorLib.StringDependant hero) : base(hero) {
            this.Type = RewardType.HERO;
        }

        public Hero(FEHDataExtractorLib.Hero hero) : this(hero as FEHDataExtractorLib.StringDependant) {
            this.Rarity = hero.Rarity;
        }
    }
}
