using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEHDataExtractorLib.Struct {
    public static class Base {
        public static          StringsUpdatable    WeaponNames       = new StringsUpdatable(new string[] { "Sword", "Lance", "Axe", "Red Bow", "Blue Bow", "Green Bow", "Bow", "Red Dagger", "Blue Dagger", "Green Dagger", "Dagger", "Red Tome", "Blue Tome", "Green Tome", "Staff", "Red Breath", "Blue Breath", "Green Breath", "Colorless Breath", "Red Beast", "Blue Beast", "Green Beast", "Colorless Beast" });
        public static readonly StringsUpdatable    TomeElements      = new StringsUpdatable(new string[] { "None", "Fire", "Thunder", "Wind", "Light", "Dark" });
        public static readonly StringsUpdatable    MovementTypes     = new StringsUpdatable(new string[] { "Infantry", "Armored", "Cavalry", "Flying" });
        public static readonly StringsUpdatable    GameSeries        = new StringsUpdatable(new string[] { "Heroes", "Shadow Dragon and the Blade of Light / Mystery of the Emblem / Shadow Dragon / New Mystery of the Emblem", "Gaiden / Echoes", "Genealogy of the Holy War", "Thracia 776", "The Binding Blade", "The Blazing Blade", "The Sacred Stones", "Path of Radiance", "Radiant Dawn", "Awakening", "Fates", "Three Houses", "Tokyo Mirage Sessions ♯FE Encore" });
        public static readonly StringsUpdatable    BadgeColors       = new StringsUpdatable(new string[] { "Scarlet", "Azure", "Verdant", "Trasparent" });
        public static readonly StringsUpdatable    ShardColors       = new StringsUpdatable(new string[] { "Universal", "Scarlet", "Azure", "Verdant", "Trasparent" });
        public static readonly StringsUpdatable    SkillCategories   = new StringsUpdatable(new string[] { "Weapon", "Assist", "Special", "Passive A", "Passive B", "Passive C", "Sacred Seal", "Refined Weapon Skill Effect", "Beast Effect" });
        public static readonly StringsUpdatable    Ranks             = new StringsUpdatable(new string[] { "C", "B", "A", "S" });
        public static readonly StringsUpdatable    LegendaryElements = new StringsUpdatable(new string[] { "Fire", "Water", "Wind", "Earth", "Light", "Dark", "Astra", "Anima" });
        public static readonly StringsUpdatable    LegendaryKinds    = new StringsUpdatable(new string[] { "Legendary/Mythic", "Duo" });
        public static readonly StringsUpdatable    Colours           = new StringsUpdatable(new string[] { "Red", "Blue", "Green", "Colorless" });
        public static readonly String[]            PrintSkills       = { "Default Weapon", "Default Assist", "Default Special", "Unknown", "Unknown", "Unknown", "Unlocked Weapon", "Unlocked Assist", "Unlocked Special", "Passive A", "Passive B", "Passive C", "Unknown", "Unknown" };

        public static readonly string[]            Thrones           = {
            "Gold",
            "Silver",
            "Bronze"
        };

        public static readonly StringsUpdatable    ArenaAssaultItems = new StringsUpdatable(new string[] {
            "Elixir",
            "Fortifying Horn",
            "Special Blade",
            "Infantry Boots",
            "Naga's Tear",
            "Dancer's Veil",
            "Lightning Charm",
            "Panic Charm",
            "Fear Charm",
            "Pressure Charm"
        });

        public static readonly StringsUpdatable    Rewards           = new StringsUpdatable(new string[] {
            "Orb",
            "Hero",
            "Hero Feather",
            "Stamina Potion",
            "Dueling Crest",
            "Light's Blessing",
            "Crystal",
            "",
            "",
            "",
            "",
            "",
            "Badge",
            "Battle Flag",
            "Sacred Seal",
            "Arena Assault Item",
            "Sacred Coin",
            "Refining Stone",
            "Divine Dew",
            "Arena Medal",
            "Blessing",
            "Conquest Lance",
            "Accessory",
            "Conversation",
            "",
            "Arena Crown",
            "Heroic Grail",
            "Aether Stone",
            "Throne",
            "Summoning Ticket",
            "Dragonflower",
            "Forma Torch"
        });

        public static readonly StringsUpdatable RewardTrigger  = new StringsUpdatable(new string[] { 
            "",
            "On foe defeat",
            "On scenario clear",
            "On Arena Assault clear",
            "On Tap Battle floor clear",
            "On Tap Battle boss clear",
            "On Forma floor clear...?",
            "On Forma tower clear...?"
        });
        public static readonly StringsUpdatable RewardGameMode = new StringsUpdatable(new string[] { 
            "",
            "Normal Map",
            "",
            "Special Map",
            "",
            "Training Tower",
            "Arena Duel",
            "Voting Gauntlet",
            "Tempest Trials",
            "",
            "",
            "Arena Assault",
            "Tap Battle",
            "",
            "Grand Conquests",
            "",
            "",
            "Aether Raids",
            "Heroic Ordeals",
            "Alliegence Battles",
            "Aether Raids Practice",
            "Rökkr Battles",
            "Forma Tower...?"
        });

        public static readonly StringsUpdatable RewardDifficulty = new StringsUpdatable(new string[] {
            "",
            "Hard",
            "Lunatic",
            "Infernal",
            "",
            "",
            "Intermediate",
            "Advanced",
            "",
            "",
            "",
            "consecutive battles to win"
        });

        public static readonly StringsUpdatable ForgingBondsHearts = new StringsUpdatable(new string[] {
            "Green",
            "Blue",
            "Red",
            "Yellow",
            "Unknown",
            "Unknown2"
        });

        private static SingleWeaponClass[] __WeaponsData;
        public static  SingleWeaponClass[]   WeaponsData {
            get {
                if (__WeaponsData == null) {
                    __WeaponsData    = new SingleWeaponClass[23];
                    __WeaponsData[0] = new SingleWeaponClass("Sword", 0, "Red",   1, false, false, false, false, false);
                    __WeaponsData[1] = new SingleWeaponClass("Lance", 1, "Blue",  1, false, false, false, false, false);
                    __WeaponsData[2] = new SingleWeaponClass("Axe",   2, "Green", 1, false, false, false, false, false);

                    // Bows
                    for (int i = 0; i < 4; i++) {
                        __WeaponsData[i + 3] = new SingleWeaponClass(
                            (i != 3 ? Colours.Get(i) + " " : "") + "Bow",
                            3 + i, Colours.Get(i),
                            2,
                            false,
                            false,
                            false,
                            false,
                            false
                        );
                    }

                    // Daggers
                    for (int i = 0; i < 4; i++) {
                        __WeaponsData[i + 7] = new SingleWeaponClass(
                            (i != 3 ? Colours.Get(i) + " " : "") + "Dagger",
                            7 + i,
                            Colours.Get(i),
                            2,
                            false,
                            false,
                            true,
                            false,
                            false
                        );
                    }

                    // Tomes
                    for (int i = 0; i < 3; i++) {
                        __WeaponsData[i + 11] = new SingleWeaponClass(
                            Colours.Get(i) + " Tome",
                            11 + i,
                            Colours.Get(i),
                            2,
                            true,
                            false,
                            false,
                            false,
                            false
                        );
                    }

                    // Staff
                    __WeaponsData[14] = new SingleWeaponClass(
                        "Staff",
                        14,
                        "Colorless",
                        2,
                        true,
                        true,
                        false,
                        false,
                        false
                    );

                    // Dragons
                    for (int i = 0; i < 4; i++) {
                        __WeaponsData[i + 15] = new SingleWeaponClass(
                            Colours.Get(i) + " Breath",
                            15 + i,
                            Colours.Get(i),
                            1,
                            true,
                            false,
                            false,
                            true,
                            false
                        );
                    }

                    // Beasts
                    for (int i = 0; i < 4; i++) {
                        __WeaponsData[i + 19] = new SingleWeaponClass(
                            Colours.Get(i) + " Beast",
                            19 + i,
                            Colours.Get(i),
                            1,
                            false,
                            false,
                            false,
                            false,
                            true
                        );
                    }
                }

                return __WeaponsData;
            }
            set { }
        }

        public static Hashtable Table = new Hashtable();
    }
}
