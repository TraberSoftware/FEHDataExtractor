namespace FEHDataExtractorLib.Struct.Reward {
    public class ArenaAssaultItem : SingleCountDependant {
        public string Item;
        public ArenaAssaultItem(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.ARENA_ASSAULT_ITEM;
        }

        public ArenaAssaultItem(FEHDataExtractorLib.ArenaAssaultItem arenaAssaultItem) : this(arenaAssaultItem as FEHDataExtractorLib.SingleCountDependant) {
            this.Item  = Base.ArenaAssaultItems.Get(arenaAssaultItem.Item);
        }
    }
}
