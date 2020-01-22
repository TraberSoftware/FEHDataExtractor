using System;

namespace FEHDataExtract_CLI {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                __usage();
                Environment.Exit(1);
            }

            string inpath = args[0];
            string action = args.Length > 1 ? args[1] : string.Empty;

            bool extracted = new EXHandler().Handle(inpath, action);
        }

        private static void __usage() {
            Console.WriteLine(
                "Application usage: " + Environment.NewLine +
                "    " + "fextract" + " " + "{file_path}"      + " " + "{action}" + Environment.NewLine +
                "    " + "fextract" + " " + "{directory_path}" + " - this will extract everything recursively and automatically" + Environment.NewLine + 
                Environment.NewLine +
                " List of actions (targeting a file): " + Environment.NewLine +
                "  [*] " + "decompress - Decompress .lz" + Environment.NewLine +
                "  [*] " + "enemies    - Extract Enemy data"          + Environment.NewLine +
                "  [*] " + "bonds      - Extract Forging Bonds data"  + Environment.NewLine +
                "  [*] " + "world      - Extract GC World data"       + Environment.NewLine +
                "  [*] " + "generic    - Extract Generic text"        + Environment.NewLine +
                "  [*] " + "heroes     - Extract Heroes data"         + Environment.NewLine +
                "  [*] " + "messages   - Extract Messages"            + Environment.NewLine +
                "  [*] " + "quests     - Extract Quests data"         + Environment.NewLine +
                "  [*] " + "skills     - Extract Skills"              + Environment.NewLine + 
                "  [*] " + "tempest    - Extract Tempest Trials data" + Environment.NewLine +
                "  [*] " + "weapons    - Extract Weapons data"
            );
        }
    }
}
