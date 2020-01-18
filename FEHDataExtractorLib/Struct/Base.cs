using System;
using System.Collections.Generic;
using System.Text;

namespace FEHDataExtractorLib.Struct {
    public static class Base {
        public static readonly StringsUpdatable    WeaponNames      = new StringsUpdatable(new string[] { "Sword", "Lance", "Axe", "Red Bow", "Blue Bow", "Green Bow", "Bow", "Red Dagger", "Blue Dagger", "Green Dagger", "Dagger", "Red Tome", "Blue Tome", "Green Tome", "Staff", "Red Breath", "Blue Breath", "Green Breath", "Colorless Breath", "Red Beast", "Blue Beast", "Green Beast", "Colorless Beast" });
        public static readonly StringsUpdatable    Tome_Elem        = new StringsUpdatable(new string[] { "None", "Fire", "Thunder", "Wind", "Light", "Dark" });
        public static readonly StringsUpdatable    Movement         = new StringsUpdatable(new string[] { "Infantry", "Armored", "Cavalry", "Flying" });
        public static readonly StringsUpdatable    Series           = new StringsUpdatable(new string[] { "Heroes", "Shadow Dragon and the Blade of Light / Mystery of the Emblem / Shadow Dragon / New Mystery of the Emblem", "Gaiden / Echoes", "Genealogy of the Holy War", "Thracia 776", "The Binding Blade", "The Blazing Blade", "The Sacred Stones", "Path of Radiance", "Radiant Dawn", "Awakening", "Fates", "Three Houses" });
        public static readonly StringsUpdatable    BadgeColor       = new StringsUpdatable(new string[] { "Scarlet", "Azure", "Verdant", "Trasparent" });
        public static readonly StringsUpdatable    ShardColor       = new StringsUpdatable(new string[] { "Universal", "Scarlet", "Azure", "Verdant", "Trasparent" });
        public static readonly StringsUpdatable    SkillCategory    = new StringsUpdatable(new string[] { "Weapon", "Assist", "Special", "Passive A", "Passive B", "Passive C", "Sacred Seal", "Refined Weapon Skill Effect", "Beast Effect" });
        public static readonly StringsUpdatable    Ranks            = new StringsUpdatable(new string[] { "C", "B", "A", "S" });
        public static readonly StringsUpdatable    LegendaryElement = new StringsUpdatable(new string[] { "Fire", "Water", "Wind", "Earth", "Light", "Dark", "Astra", "Anima" });
        public static readonly StringsUpdatable    LegendaryKind    = new StringsUpdatable(new string[] { "Legendary/Mythic", "Duo" });
        public static readonly StringsUpdatable    Colours          = new StringsUpdatable(new string[] { "Red", "Blue", "Green", "Colorless" });
        public static readonly String[]            PrintSkills      = { "Default Weapon", "Default Assist", "Default Special", "Unknown", "Unknown", "Unknown", "Unlocked Weapon", "Unlocked Assist", "Unlocked Special", "Passive A", "Passive B", "Passive C", "Unknown", "Unknown" };
    }
}
