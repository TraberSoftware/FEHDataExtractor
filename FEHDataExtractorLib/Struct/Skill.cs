using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {
    public class Skill {
        // Mandatory fields
        public uint                       ID;
        public string                     IDTag;
        public string                     Category;
        public string                     NameID;
        public string                     DescriptionID;
        public int                        Range;
        public int                        Might;
        public string                     Effect;
        public bool                       Healing;
        public bool                       Exclusive;
        public uint                       SPCost;
        public List<string>               PreRequisites;
        public Dictionary<string, string> Sprites;
        public HeroStats                  Bonuses;
        public HeroStats                  RefineStats;
        public List<string>               EquipableBy;
        public List<string>               Effectiviness;
        public List<string>               Weakness;
        public List<string>               Shieldness;
        public List<string>               Adaptativeness;
        public bool                       IsRefined;
        public bool                       IsBreath;
        public bool                       IsStaff;
        public bool                       IsDagger;
        public bool                       IsBeast;

        // Unneeded fields
        [JsonIgnore]
        public uint                       NumId;
        [JsonIgnore]
        public uint                       SortId;
        [JsonIgnore]
        public uint                       IconId;
        [JsonIgnore]
        public string                     BeastEffectID;
        [JsonIgnore]
        public uint                       Weapon;
        [JsonIgnore]
        public uint                       Movement;
        [JsonIgnore]
        public string                     NextSkill;
        [JsonIgnore]
        public HeroStats                  SkillParams;
        [JsonIgnore]
        public HeroStats                  ClassParams;
        [JsonIgnore]
        public sbyte                      Promotions;
        [JsonIgnore]
        public sbyte                      PromotionRarity;
        [JsonIgnore]
        public string                     RefineID;
        [JsonIgnore]
        public string                     RefineBase;
        [JsonIgnore]
        public byte                       RefineSort;
        [JsonIgnore]
        public ushort                     Score;
        [JsonIgnore]
        public string                     TomeClass;
        [JsonIgnore]
        public bool                       EnemyOnly;
        [JsonIgnore]
        public int                        Cooldown;
        [JsonIgnore]
        public int                        EffectRange;
        [JsonIgnore]
        public int                        AssistCooldown;
        [JsonIgnore]
        public uint                       TimingID;
        [JsonIgnore]
        public uint                       AbilityID;
        [JsonIgnore]
        public uint                       Limit1ID;
        [JsonIgnore]
        public List<string>               Limit1Params;
        [JsonIgnore]
        public uint                       Limit2ID;
        [JsonIgnore]
        public List<string>               Limit2Params;
        [JsonIgnore]
        public string                     TargetWeapon;
        [JsonIgnore]
        public string                     TargetMovement;
        [JsonIgnore]
        public string                     Passive;
        [JsonIgnore]
        public int                        MinLV;
        [JsonIgnore]
        public int                        MaxLV;
        [JsonIgnore]
        public bool                       AllowedRandom;
        [JsonIgnore]
        public bool                       AllowedTrainingTower;

        public Skill(SingleSkill skill) {
            this.ID              = skill.Num_id.Value;
            this.IDTag           = skill.Id_tag.Value;
            this.RefineBase      = skill.Refine_base.Value;
            this.NameID          = Base.Table.Contains(skill.Name_id.Value) ? Base.Table[skill.Name_id.Value].ToString() : "";
            this.DescriptionID   = Base.Table.Contains(skill.Desc_id.Value) ? Base.Table[skill.Desc_id.Value].ToString() : "";
            this.IDTag           = skill.Id_tag.Value;
            this.RefineBase      = skill.Refine_base.Value     != string.Empty ? Util.GetString(skill.Refine_base)     + " - " + skill.Refine_base.Value     : "";
            this.RefineID        = skill.Refine_id.Value       != string.Empty ? Util.GetString(skill.Refine_id)       + " - " + skill.Refine_id.Value       : "";
            this.BeastEffectID   = skill.Beast_effect_id.Value != string.Empty ? Util.GetString(skill.Beast_effect_id) + " - " + skill.Beast_effect_id.Value : "";
            this.SortId          = skill.Sort_id.Value;
            this.IconId          = skill.Icon_id.Value;
            this.SPCost          = skill.Sp_cost.Value;
            this.Category        = Base.SkillCategories.Get(skill.Category.Value);
            this.TomeClass       = Base.TomeElements.Get(skill.Tome_class.Value);
            this.IsRefined         = skill.Refined.Value    == 1 ? true : false;
            this.Exclusive       = skill.Exclusive.Value  == 1 ? true : false;
            this.EnemyOnly       = skill.Enemy_only.Value == 1 ? true : false;
            this.Healing         = skill.Healing.Value    == 1 ? true : false;
            this.Range           = skill.Range.Value;
            this.Might           = skill.Might.Value;
            this.Cooldown        = skill.Cooldown_count.Value;
            this.AssistCooldown  = skill.Assist_cd.Value;
            this.EffectRange     = skill.Skill_range.Value;
            this.Score           = skill.Score.Value;
            this.Promotions      = skill.Promotion_tier.Value;
            this.PromotionRarity = skill.Promotion_rarity.Value;
            this.RefineSort      = skill.Refine_sort_id.Value;

            this.Bonuses     = new HeroStats(skill.Statistics);
            this.SkillParams = new HeroStats(skill.Skill_params);
            this.RefineStats = new HeroStats(skill.Refine_stats);
            this.ClassParams = new HeroStats(skill.Class_params);

            this.PreRequisites = new List<string>();
            foreach (StringXor Prerequisite in skill.Prerequisites) {
                string PrerequisiteString = Util.GetString(Prerequisite).Trim();

                if (PrerequisiteString != string.Empty) {
                    this.PreRequisites.Add(
                        Util.GetString(Prerequisite) +
                        ((Prerequisite.Value.Trim() != string.Empty) ? " - " + Prerequisite.Value : "")
                    );
                }
            }

            this.NextSkill       = skill.Next_skill.Value != string.Empty ? Util.GetString(skill.Next_skill) + " - " + skill.Next_skill.Value : "";
            this.Sprites         = new Dictionary<string, string> {
                { "Bow Sprite",            "" },
                { "Weapon Sprite",         "" },
                { "Arrow Sprite",          "" },
                { "Map Animation",         "" },
                { "AoE Special Animation", "" },
            };
            for(int i = 0; i < skill.Sprites.Length; i++) {
                switch (i) {
                    case 0:
                        this.Sprites["Bow Sprite"] = skill.Sprites[i].Value;
                        break;
                    case 1:
                        if (this.Sprites["Bow Sprite"] != string.Empty) {
                            this.Sprites["Arrow Sprite"] = skill.Sprites[i].Value;
                        }
                        else {
                            this.Sprites["Weapon Sprite"] = skill.Sprites[i].Value;
                        }
                        break;
                    case 2:
                        this.Sprites["Map Animation"] = skill.Sprites[i].Value;
                        break;
                    case 3:
                        this.Sprites["AoE Special Animation"] = skill.Sprites[i].Value;
                        break;
                }
            }

            int    tmp       = 1;
            this.EquipableBy = new List<string>();
            for (int i = 0; i < Base.WeaponNames.Length; i++) {
                if (((skill.Wep_equip.Value & tmp) >> i) == 1) {
                    this.EquipableBy.Add(Base.WeaponNames.Get(i));

                    if (skill.Category.Value == 0) {
                        if (Base.WeaponsData[i].Is_breath)
                            this.IsBreath = true;
                        if (Base.WeaponsData[i].Is_staff)
                            this.IsStaff  = true;
                        if (Base.WeaponsData[i].Is_dagger)
                            this.IsDagger = true;
                        if (Base.WeaponsData[i].Is_beast)
                            this.IsBeast  = true;
                    }
                }
                tmp = tmp << 1;
            }
            this.EquipableBy.Add(ExtractUtils.BitmaskConvertToString(skill.Mov_equip.Value, Base.MovementTypes));

            string effect = "";
            if (this.IsBreath) {
                if(skill.Class_params.Hp.Value == 1) {
                    effect = "Targets lowest defense on enemy with Movement target and Weapon target";
                }
            }

            if (this.IsStaff) {
                if (skill.Class_params.Hp.Value == 1) {
                    effect = "Damage calculated the same as other weapons";
                }
                if (skill.Class_params.Hp.Value == 2) {
                    effect = "Foe cannot counterattack";
                }
            }
            this.Effect = effect;

            if (this.IsDagger && skill.Class_params.Hp.Value > 0) {
                effect      = "Inflicts ";
                String temp = "";
                if (skill.Class_params.Atk.Value > 0) {
                    effect += "-" + skill.Class_params.Atk + " Attack";
                    temp    = ",";
                }
                if (skill.Class_params.Spd.Value > 0) {
                    effect += temp + "-" + skill.Class_params.Spd + " Speed";
                    temp    = ",";
                }
                if (skill.Class_params.Def.Value > 0) {
                    effect += temp + "-" + skill.Class_params.Def + " Defense";
                    temp    = ",";
                }
                if (skill.Class_params.Res.Value > 0) {
                    effect += temp + "-" + skill.Class_params.Res + " Resistance";
                    temp    = ",";
                }
                effect += "on foes within " + skill.Class_params.Hp + " spaces of target through their next action";
            }

            if (this.IsBeast && (skill.Class_params.Hp.Value == 1)) {
                effect     += "When transformed, grants: ";
                String temp = "";
                if (skill.Class_params.Atk.Value > 0) {
                    effect += skill.Class_params.Atk + " Attack";
                    temp    = ",";
                }
                if (skill.Class_params.Spd.Value > 0) {
                    effect += temp + skill.Class_params.Spd + " Speed";
                    temp    = ",";
                }
                if (skill.Class_params.Def.Value > 0) {
                    effect += temp + skill.Class_params.Def + " Defense";
                    temp    = ",";
                }
                if (skill.Class_params.Res.Value > 0) {
                    effect += temp + skill.Class_params.Res + " Resistance";
                    temp    = ",";
                }
            }

            string   WeaponEffectiveness = ExtractUtils.BitmaskConvertToString(skill.Wep_effective.Value, Base.WeaponNames);
            string MovementEffectiveness = ExtractUtils.BitmaskConvertToString(skill.Mov_effective.Value, Base.MovementTypes);

            this.Effectiviness = new List<string>();
            if (WeaponEffectiveness != string.Empty) {
                this.Effectiviness.Add(
                    WeaponEffectiveness
                );
            }
            if(MovementEffectiveness != string.Empty) {
                this.Effectiviness.Add(
                    MovementEffectiveness
                );
            }

            string   WeaponWeakness = ExtractUtils.BitmaskConvertToString(skill.Wep_weakness.Value, Base.WeaponNames);
            string MovementWeakness = ExtractUtils.BitmaskConvertToString(skill.Mov_weakness.Value, Base.MovementTypes);

            this.Weakness = new List<string>();
            if(WeaponWeakness != string.Empty) {
                this.Weakness.Add(
                    WeaponWeakness
                );
            }
            if(MovementWeakness != string.Empty) {
                this.Weakness.Add(
                    MovementWeakness
                );
            }

            string   WeaponShieldness = ExtractUtils.BitmaskConvertToString(skill.Wep_shield.Value, Base.WeaponNames);
            string MovementShieldness = ExtractUtils.BitmaskConvertToString(skill.Mov_shield.Value, Base.MovementTypes);

            this.Shieldness = new List<string>();
            if(WeaponShieldness != string.Empty) {
                this.Shieldness.Add(
                    WeaponShieldness
                );
            }
            if(MovementShieldness != string.Empty) {
                this.Shieldness.Add(
                    MovementShieldness
                );
            }

            string   WeaponAdaptativeness = ExtractUtils.BitmaskConvertToString(skill.Wep_adaptive.Value, Base.WeaponNames);
            string MovementAdaptativeness = ExtractUtils.BitmaskConvertToString(skill.Mov_adaptive.Value, Base.MovementTypes);

            this.Adaptativeness = new List<string>();
            if(WeaponAdaptativeness != string.Empty) {
                this.Adaptativeness.Add(
                    WeaponAdaptativeness
                );
            }
            if(MovementAdaptativeness != string.Empty) {
                this.Adaptativeness.Add(
                    MovementAdaptativeness
                );
            }

            this.TimingID     = skill.Timing_id.Value;
            this.AbilityID    = skill.Ability_id.Value;
            this.Limit1ID     = skill.Limit1_id.Value;
            this.Limit1Params = new List<string>();

            for (int i = 0; i < skill.Limit1_params.Length; i++) {
                if (skill.Limit1_id.Value != 0 && !skill.Limit1_params[i].Value.Equals("")) {
                    Limit1Params.Add("Limit 1 Parameter " + (i + 1) + ": " + skill.Limit1_params[i]);
                }
            }

            this.Limit2ID     = skill.Limit1_id.Value;
            this.Limit2Params = new List<string>();

            for (int i = 0; i < skill.Limit2_params.Length; i++) {
                if (skill.Limit2_id.Value != 0 && !skill.Limit2_params[i].Value.Equals("")) {
                    Limit1Params.Add("Limit 2 Parameter " + (i + 1) + ": " + skill.Limit2_params[i]);
                }
            }

            this.TargetWeapon         = ExtractUtils.BitmaskConvertToString(skill.Target_wep.Value, Base.WeaponNames);
            this.TargetMovement       = ExtractUtils.BitmaskConvertToString(skill.Target_mov.Value, Base.MovementTypes);
            this.Passive              = skill.Passive_next.Value != string.Empty ? Util.GetString(skill.Passive_next) : "";
            this.AllowedRandom        = skill.Random_allowed.Value  == 0 ? false : true;
            this.AllowedTrainingTower = skill.Tt_inherit_base.Value == 0 ? false : true;
            this.MinLV                = skill.Min_lv.Value;
            this.MaxLV                = skill.Min_lv.Value;
        }
    }
}
