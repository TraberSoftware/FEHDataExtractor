namespace FEHDataExtractorLib.Struct.Reward {
    public class Conversation : Reward {
        public int    Length;
        public string Raw;
        public int    Support;

        public Conversation() {
            this.Type = RewardType.CONVERSATION;
        }

        public Conversation(FEHDataExtractorLib.Conversation conversation) : this() {
            this.Length  = conversation.Length;
            this.Raw     = conversation.Rew;
            this.Support = conversation.Support;
        }
    }
}
