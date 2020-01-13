using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FEHDataExtractorLib;

namespace FEHDataExtract_CLI {
    public class EXHandler {
        public static Dictionary<string, string> ActionMap = new Dictionary<string, string> {
            { "decompress", "Decompress"     },
            { "enemies",    "Enemies"        },
            { "bonds",      "Forging Bonds"  },
            { "world",      "GC World"       },
            { "generic",    "Generic Text"   },
            { "heroes",     "Heroes"         },
            { "messages",   "Messages"       },
            { "quests",     "Quests"         },
            { "skills",     "Skills"         },
            { "tempest",    "Tempest Trials" },
            { "weapons",    "Weapon Classes" }
        };

        private Dictionary<string, ExtractionBase> __ExtractionBases = new Dictionary<string, ExtractionBase>();
        private int __offset = 0x20;

        public EXHandler() {
            this.__initializeWeapons();

            this.__ExtractionBases = new Dictionary<string, ExtractionBase> {
                { "world",      new GCWorld()                             },
                { "enemies",    new BaseExtractArchive<SingleEnemy>()     },
                { "heroes",     new BaseExtractArchive<SinglePerson>()    },
                { "quests",     new BaseExtractArchive<Quest_group>()     },
                { "skills",     new BaseExtractArchive<SingleSkill>()     },
                { "weapons",    new WeaponClasses()                       },

                { "messages",   new Messages()                            },
                { "bonds",      new Forging_Bonds()                       },
                { "tempest",    new BaseExtractArchive<TempestTrial>()    },
                { "generic",    new GenericText("", CommonRelated.Common) },
                { "decompress", new Decompress()                          },
            };
        }

        private void __initializeWeapons() {
            SingleWeaponClass[] weaponClasses = new SingleWeaponClass[23];
            weaponClasses[0] = new SingleWeaponClass("Sword", 0, "Red", 1,   false, false, false, false, false);
            weaponClasses[1] = new SingleWeaponClass("Lance", 1, "Blue", 1,  false, false, false, false, false);
            weaponClasses[2] = new SingleWeaponClass("Axe",   2, "Green", 1, false, false, false, false, false);

            // Bows
            for (int i = 0; i < 4; i++) {
                weaponClasses[i + 3] = new SingleWeaponClass(
                    (i != 3 ? ExtractionBase.Colours.getString(i) + " " : "") + "Bow",
                    3 + i, ExtractionBase.Colours.getString(i),
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
                weaponClasses[i + 7] = new SingleWeaponClass(
                    (i != 3 ? ExtractionBase.Colours.getString(i) + " " : "") + "Dagger",
                    7 + i,
                    ExtractionBase.Colours.getString(i),
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
                weaponClasses[i + 11] = new SingleWeaponClass(
                    ExtractionBase.Colours.getString(i) + " Tome",
                    11 + i,
                    ExtractionBase.Colours.getString(i),
                    2,
                    true,
                    false,
                    false,
                    false,
                    false
                );
            }

            // Staff
            weaponClasses[14] = new SingleWeaponClass(
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
                weaponClasses[i + 15] = new SingleWeaponClass(
                    ExtractionBase.Colours.getString(i) + " Breath",
                    15 + i,
                    ExtractionBase.Colours.getString(i),
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
                weaponClasses[i + 19] = new SingleWeaponClass(
                    ExtractionBase.Colours.getString(i) + " Beast",
                    19 + i,
                    ExtractionBase.Colours.getString(i),
                    1,
                    false,
                    false,
                    false,
                    false,
                    true
                );
            }
            ExtractionBase.WeaponsData = weaponClasses;
        }

        public bool Handle(string file, string action = "") {
            ExtractionBase dataExtractor = action != string.Empty && this.__ExtractionBases.ContainsKey(action) ? this.__ExtractionBases[action] : null;

            Console.WriteLine("Extracting data from file {0}", file);
            if (dataExtractor != null) {
                try {
                    this.__extractData(dataExtractor, file);

                    return true;
                }
                catch(Exception e) {

                }
            }
            else {
                Console.WriteLine(" [*] Action not selected, detecting automatically...");

                foreach(KeyValuePair<string, ExtractionBase> iteratedDataExtractor in this.__ExtractionBases) {
                    /*
                    // Skip decompress
                    switch (iteratedDataExtractor.Key) {
                        case "world":
                        case "decompress":
                        case "generic":
                            continue;
                            break;
                    }
                    */

                    try {
                        if(this.__extractData(iteratedDataExtractor.Value, file)) {
                            Console.WriteLine(" [*] Detected data type: {0}", iteratedDataExtractor.Value.Name);
                        }

                        return true;
                    }
                    catch(Exception e) {
                        Console.WriteLine(" [*] Failed using {0}", iteratedDataExtractor.Value.Name);
                    }
                }
            }

            return false;
        }

        private bool __extractData(ExtractionBase dataExtractor, string file) {
            string ext    = System.IO.Path.GetExtension(file).ToLower();
            byte[] data   = Decompression.Open(file);
            String output = "";

            if (data != null && dataExtractor != null && !(dataExtractor.Name.Equals("") || dataExtractor.Name.Equals("Decompress"))) {
                HSDARC a = new HSDARC(0, data);
                while (a.Ptr_list_length - a.NegateIndex > a.Index) {
                    dataExtractor.InsertIn(a, this.__offset, data);
                    output += dataExtractor.ToString();
                }
            }

            String PathManip = file.Remove(file.Length - 3, 3);
            if (ext.Equals(".lz")) {
                PathManip = file.Remove(file.Length - 6, 6);
            }
            PathManip += dataExtractor.Name.Equals("Decompress") ? "bin" : "txt";

            if (file.Equals(PathManip)) {
                PathManip += dataExtractor.Name.Equals("Decompress") ? ".bin" : ".txt";
            }

            if (dataExtractor.Name.Equals("Decompress") && data != null) {
                if (data.Length == 0) {
                    return false;
                }

                File.WriteAllBytes(PathManip, data);
            }
            else {
                if(output.Length == 0) {
                    return false;
                }

                File.WriteAllText(PathManip, output);
            }

            return true;
        }
    }
}
