using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct {
    public class HeroStats {
        public int Hp;
        public int Atk;
        public int Spd;
        public int Def;
        public int Res;

        public HeroStats() { }

        public HeroStats(Stats stats) {
            this.Hp  = stats.Hp.Value;
            this.Atk = stats.Atk.Value;
            this.Spd = stats.Spd.Value;
            this.Def = stats.Def.Value;
            this.Res = stats.Res.Value;
        }
    }
}
