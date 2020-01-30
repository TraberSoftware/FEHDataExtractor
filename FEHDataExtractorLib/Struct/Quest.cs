using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {

    public class QuestGroup {
        public string          ID;
        public string          Name;
        public DateTime        DateStart;
        public DateTime        DateEnd;
        public List<QuestList> QuestList;

        public QuestGroup(Quest_group questGroup) {
            this.ID = questGroup.Id_tag.Value;
            this.Name = questGroup.Group.Value;
            this.DateStart = DateTimeOffset.FromUnixTimeSeconds(questGroup.Start.Value) .DateTime.ToLocalTime();
            this.DateEnd   = DateTimeOffset.FromUnixTimeSeconds(questGroup.Finish.Value).DateTime.ToLocalTime();

            this.QuestList = new List<QuestList>();
            foreach(Quest_list questList in questGroup.Lists) {
                this.QuestList.Add(
                    new QuestList(questList)
                );
            }
        }
    }

    public class QuestList {
        public string      Difficulty;
        public int         Count;
        public List<Quest> Quests;

        public QuestList(Quest_list questList) {
            this.Difficulty = !questList.Difficulty.Value.Equals("") ? questList.Difficulty.Value : "None";
            this.Count      = questList.Quests.Length;
            this.Quests     = new List<Quest>();

            foreach(Quest_definition questDefinition in questList.Quests) {
                this.Quests.Add(new Quest(questDefinition));
            }
        }
    }

    public class Quest {
        public string            ID;
        public string            CommonID;
        public string            MapID;

        public uint              Times;
        public string            MapGroup;
        public string            Trigger;
        public string            GameMode;
        public string            Difficulty;
        public int               Survive;

        public QuestRequirements HeroRequirement;
        public QuestRequirements EnemyRequirement;

        public List<string> Rewards;

        public Quest(Quest_definition questDefinition) {
            this.ID       = questDefinition.Quest_id.Value;
            this.CommonID = questDefinition.Common_id.Value;
            this.MapID    = questDefinition.Map_id.Value;

            this.Times      = questDefinition.Times.Value;
            this.MapGroup   = questDefinition.Map_group.Value;
            this.Trigger    = Base.RewardTrigger .Get((int) questDefinition.Trigger.Value);
            this.GameMode   = Base.RewardGameMode.Get((int) questDefinition.Game_mode.Value);
            this.Difficulty = (questDefinition.Difficulty.Value != -1) ? Base.RewardDifficulty.Get(questDefinition.Difficulty.Value) : "Any";
            this.Survive    = (questDefinition.Survive.Value != -1)    ? questDefinition.Survive.Value : 1;

            
            this.HeroRequirement  = new QuestRequirements(questDefinition.Unit_reqs);
            this.EnemyRequirement = new QuestRequirements(questDefinition.Foe_reqs);

            this.Rewards = new List<string>();
            foreach(SingleReward singleReward in questDefinition.Reward.Rewards.Rewards) {
                this.Rewards.Add(
                    singleReward.Thing.ToString()
                );
            }
        }

        public class QuestRequirements {
            public string Hero;
            public string Color;
            public string Weapon;
            public string Movement;
            public short  MinLevel;
            public bool   Blessed;
            public string Blessing;

            public QuestRequirements(QuestUnitMatch questUnitMatch) {
                this.Hero     = (!questUnitMatch.Hero_id.Value.Equals("")) ? Util.GetHeroName(questUnitMatch.Hero_id.Value)        : "Any";
                this.Color    = (questUnitMatch.Color.Value    != -1)      ? Base.Colours      .Get(questUnitMatch.Color.Value)    : "Any";
                this.Weapon   = (questUnitMatch.Wep_type.Value != -1)      ? Base.WeaponNames  .Get(questUnitMatch.Wep_type.Value) : "Any";
                this.Movement = (questUnitMatch.Mov_type.Value != -1)      ? Base.MovementTypes.Get(questUnitMatch.Mov_type.Value) : "Any";
                this.MinLevel = (questUnitMatch.Lv.Value       != -1)      ? questUnitMatch.Lv.Value : (short) 1;
                this.Blessed  = questUnitMatch.Blessing.Value != 0 ? true : false;
                this.Blessing = questUnitMatch.Blessing.Value != 0 ? Base.LegendaryElements.Get(questUnitMatch.Blessing.Value) : "";
            }
        }
    }
}
