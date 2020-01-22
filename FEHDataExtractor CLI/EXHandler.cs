using System;
using System.Collections.Generic;
using System.Configuration;
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

        private string                             __MessagesPath    = "";
        private Dictionary<string, ExtractionBase> __ExtractionBases = new Dictionary<string, ExtractionBase>();
        private int __offset = 0x20;

        public EXHandler() {
            this.__initializeSettings();

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

        public bool Handle(string file, string action = "") {
            //
            if (Directory.Exists(file)) {
                this.__HandleDirectory(file);

                return true;
            }

            // Is file
            if (File.Exists(file)) {
                return this.__HandleFile(file, action);
            }

            return false;
        }

        private void __HandleDirectory(string directory) {
            // Handle heroes
            string HeroesPath = Path.Combine(
                directory,
                "SRPG" + Path.DirectorySeparatorChar + "Person"
            );
            if (Directory.Exists(HeroesPath)) {
                Console.WriteLine(" [*] Extracting heroes data...");
                foreach (string file in Directory.EnumerateFiles(HeroesPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new BaseExtractArchive<SinglePerson>(), file);
                }
            }

            // Handle Grand Conquest
            string GCPath = Path.Combine(
                directory,
                "Occupation" + Path.DirectorySeparatorChar + "World"
            );
            if (Directory.Exists(GCPath)) {
                Console.WriteLine(" [*] Extracting Grand Conquest data...");
                foreach (string file in Directory.EnumerateFiles(GCPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new GCWorld(), file);
                }
            }

            // Handle Skills
            string SkillsPath = Path.Combine(
                directory,
                "SRPG" + Path.DirectorySeparatorChar + "Skill"
            );
            if (Directory.Exists(SkillsPath)) {
                Console.WriteLine(" [*] Extracting skills data...");
                foreach (string file in Directory.EnumerateFiles(SkillsPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new BaseExtractArchive<SingleSkill>(), file);
                }
            }

            // Handle Quests
            string QuestsPath = Path.Combine(
                directory,
                "Mission"
            );
            if (Directory.Exists(QuestsPath)) {
                Console.WriteLine(" [*] Extracting missions data...");
                foreach (string file in Directory.EnumerateFiles(QuestsPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new BaseExtractArchive<Quest_group>(), file);
                }
            }

            // Handle Tempest Trials
            string TempestTrialsPath = Path.Combine(
                directory,
                "SRPG" + Path.DirectorySeparatorChar + "SequentialMap"
            );
            if (Directory.Exists(TempestTrialsPath)) {
                Console.WriteLine(" [*] Extracting Tempest Trials data...");
                foreach (string file in Directory.EnumerateFiles(TempestTrialsPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new BaseExtractArchive<TempestTrial>(), file);
                }
            }

            // Handle Forging Bonds
            string ForgingBondsPath = Path.Combine(
                directory,
                "Portrait"
            );
            if (Directory.Exists(ForgingBondsPath)) {
                Console.WriteLine(" [*] Extracting Forging Bonds data...");
                foreach (string file in Directory.EnumerateFiles(ForgingBondsPath)) {
                    if (!file.EndsWith(".bin.lz")) {
                        continue;
                    }
                    this.__extractData(new Forging_Bonds(), file);
                }
            }

            // Handle Weapons
            string WeaponsPath = Path.Combine(
                directory,
                "SRPG" + Path.DirectorySeparatorChar + "Weapon.bin.lz"
            );
            if (File.Exists(WeaponsPath)) {
                Console.WriteLine(" [*] Extracting weapons data...");
                this.__extractData(new WeaponClasses(), WeaponsPath);
            }
        }

        private bool __HandleFile(string file, string action = "") {
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

        private void __initializeSettings() {
            this.__MessagesPath = ConfigurationManager.AppSettings.Get("MessagesPath").Trim();
            Console.WriteLine("Loading messages from: " + this.__MessagesPath);

            LoadMessages.openFolder(this.__MessagesPath);
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
            PathManip += dataExtractor.Name.Equals("Decompress") ? "bin" : (dataExtractor.PrintJSON ? "json" : "txt");

            if (file.Equals(PathManip)) {
                PathManip += dataExtractor.Name.Equals("Decompress") ? ".bin" : (dataExtractor.PrintJSON ? ".json" : ".txt");
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
