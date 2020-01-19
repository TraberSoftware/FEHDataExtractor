using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct.Reward {
    public class Badge : SingleCountDependant {
        public bool   Great;
        public string Color;

        public Badge(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.BADGE;
        }

        public Badge(FEHDataExtractorLib.Badge badge) : this(badge as FEHDataExtractorLib.SingleCountDependant) {
            this.Great = badge.Is_Great == 1 ? true : false;
            this.Color = Base.BadgeColors.Get(badge.Color);
        }
    }
}
