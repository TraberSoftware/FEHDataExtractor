namespace FEHDataExtractorLib.Struct.Reward {
    public enum RewardType {
        NONE,
        UNKNOWN,
        SINGLE_COUNT_DEPENDANT,
        STRING_DEPENDANT,
        COUNTED,
        THRONE,
        HERO,
        CRYSTAL,
        BADGE,
        ARENA_ASSAULT_ITEM,
        BLESSING,
        CONVERSATION,
        DRAGONFLOWER,
        OTHER
    }

    public class Reward {
        public RewardType Type = RewardType.NONE;
    }
}
