﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FEHDataExtractorLib.Struct {

    public class LegendaryHeroData {
        public string                  Kind;
        public Dictionary<string, int> BonusStats;
        public string                  Element;
        public bool                    IsDuel;
        public string                  DuoSkill;
        public int                     BSTValue;
    }

    public class Unit {
        [JsonProperty(Order = 1)]
        public uint                    ID;
        [JsonProperty(Order = 2)]
        public string                  IdLabel;

        [JsonProperty(Order = 3)]
        public string                  Name;
        [JsonProperty(Order = 4)]
        public string                  Epithet;
        [JsonProperty(Order = 5)]
        public string                  Description;
        [JsonProperty(Order = 6)]
        public string                  VoiceActor;
        [JsonProperty(Order = 7)]
        public string                  Illustrator;

        [JsonProperty(Order = 9)]
        public string                  Roman;
        [JsonProperty(Order = 10)]
        public string                  FaceName;
        [JsonProperty(Order = 11)]
        public string                  FaceNameAlt;
        [JsonProperty(Order = 12)]
        public DateTime?               Timestamp;
        [JsonProperty(Order = 13)]
        public string                  WeaponType;
        [JsonProperty(Order = 14)]
        public string                  TomeClass;
        [JsonProperty(Order = 15)]
        public string                  MoveType;

        [JsonProperty(Order = 16)]
        public HeroStats BaseStats;
        [JsonProperty(Order = 17)]
        public HeroStats MaxStats;
        [JsonProperty(Order = 18)]
        public List<string> Superboon;
        [JsonProperty(Order = 18)]
        public List<string> Superbane;
        [JsonProperty(Order = 18)]
        public HeroStats GrowthStats;
        [JsonProperty(Order = 18)]
        public int BST;

        public Unit(CharacterRelated person) {
            this.ID          = person.Id_num.Value;
            this.IdLabel     = person.Id_tag.Value;
            this.Roman       = person.Roman.Value;

            if (Base.Table.Contains("M" + person.Id_tag.ToString())) {
                this.Name        = Base.Table["M" + person.Id_tag.ToString()].ToString();
                this.Epithet     = Base.Table.Contains("M" + person.Id_tag.Value.Insert(3, "_HONOR"))  ? Base.Table["M" + person.Id_tag.Value.Insert(3, "_HONOR")].ToString()  : "";
                this.Description = Base.Table.Contains("M" + person.Id_tag.Value.Insert(3, "_H"))      ? Base.Table["M" + person.Id_tag.Value.Insert(3, "_H")].ToString().Replace("\\n", " ").Replace("\\r", " ") : "";
                this.VoiceActor  = Base.Table.Contains("M" + person.Id_tag.Value.Insert(3, "_VOICE"))  ? Base.Table["M" + person.Id_tag.Value.Insert(3, "_VOICE")].ToString()  : "";
                this.Illustrator = Base.Table.Contains("M" + person.Id_tag.Value.Insert(3, "_ILLUST")) ? Base.Table["M" + person.Id_tag.Value.Insert(3, "_ILLUST")].ToString() : "";
            }

            this.FaceName    = person.Face_name.Value;
            this.FaceNameAlt = person.Face_name2.Value;

            DateTime timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(person.Timestamp.Value);
            this.Timestamp   = timestamp;
            if(person.Timestamp.Value <= 0) {
                this.Timestamp = null;
            }

            this.WeaponType = Base.WeaponNames.Get(person.Weapon_type.Value);
            this.TomeClass  = Base.TomeElements.Get(person.Tome_class.Value);
            this.MoveType   = Base.MovementTypes.Get(person.Move_type.Value);

            Stats Level40Stats = new Stats(person.Base_stats, person.Growth_rates);

            this.BaseStats   = new HeroStats(person.Base_stats);
            this.MaxStats    = new HeroStats(Level40Stats);
            this.GrowthStats = new HeroStats(person.Growth_rates);

            this.BST =
                this.MaxStats.Hp  +
                this.MaxStats.Atk + 
                this.MaxStats.Spd + 
                this.MaxStats.Def + 
                this.MaxStats.Res
            ;

            string[] SuperboonData = Util.SuperStats(person.Base_stats, person.Growth_rates, Level40Stats, true ).Split(',');
            string[] SuperbaneData = Util.SuperStats(person.Base_stats, person.Growth_rates, Level40Stats, false).Split(',');

            this.Superboon = new List<string>();
            this.Superbane = new List<string>();

            if (SuperboonData.Length > 0) {
                foreach(string SuperboonDataItem in SuperboonData) {
                    this.Superboon.Add(SuperboonDataItem.Trim());
                }
            }
            if (SuperbaneData.Length > 0) {
                foreach (string SuperbaneDataItem in SuperbaneData) {
                    this.Superbane.Add(SuperbaneDataItem.Trim());
                }
            }
        }
    }

    public class Enemy : Unit {
        [JsonProperty(Order = 19)]
        public bool Spawnable;

        [JsonProperty(Order = 20)]
        public bool IsBoss;
        public Enemy(SingleEnemy person) : base(person) {
            this.Spawnable = (person.Spawnable_Enemy.Value == 0) ? true  : false;
            this.IsBoss    = (person.Is_boss.Value         == 0) ? false : true;
        }
    }

    public class Hero : Unit {
        [JsonProperty(Order = 22)]
        public Dictionary<string, Dictionary<string, string>> Skills;

        [JsonProperty(Order = 21)]
        public LegendaryHeroData LegendaryHeroData;

        [JsonProperty(Order = 8)]
        public string Series;

        public Hero(SinglePerson person) : base(person) {
            this.Series = Base.GameSeries.Get(person.Series1.Value);

            // Is a legendary hero!
            if (person.Legendary.Bonuses != null) {
                this.LegendaryHeroData = new LegendaryHeroData {
                    BonusStats = new Dictionary<string, int> {
                        { "hp",  person.Legendary.Bonuses.Hp.Value  },
                        { "atk", person.Legendary.Bonuses.Atk.Value },
                        { "spd", person.Legendary.Bonuses.Spd.Value },
                        { "def", person.Legendary.Bonuses.Def.Value },
                        { "res", person.Legendary.Bonuses.Res.Value }
                    },
                    BSTValue = person.Legendary.Bst.Value,
                    DuoSkill = person.Legendary.Duo_skill_id.Value,
                    Element  = person.Legendary.Element.Value > 0 ? Base.LegendaryElements.Get(person.Legendary.Element.Value - 1) : "",
                    IsDuel   = person.Legendary.Is_duel.Value == 0 ? true : false,
                    Kind     = Base.LegendaryKinds.Get(person.Legendary.Kind.Value - 1)
                };
            }

            this.Skills = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 0; i < person.Skills.Length / Base.PrintSkills.Length; i++) {
                string Index = (i + 1) + " Star";
                if(i == 0) {
                    Index += "s";
                }

                this.Skills.Add(
                    Index, new Dictionary<string, string>()
                );

                for (int j = 0; j < Base.PrintSkills.Length; j++) {
                    string SkillTypeName = Base.PrintSkills[j];

                    // Skip unknown used skills struct values
                    if(SkillTypeName == "Unknown") {
                        continue;
                    }

                    this.Skills[Index].Add(
                        SkillTypeName,
                        Base.Table.Contains("M" + person.Skills[i, j].Value) ?
                        Base.Table["M" + person.Skills[i, j].Value].ToString()
                        :
                        ""
                    );
                }
            }
        }
    }
}
