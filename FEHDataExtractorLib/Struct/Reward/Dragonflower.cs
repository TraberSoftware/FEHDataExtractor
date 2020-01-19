using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEHDataExtractorLib.Struct.Reward {
    public class Dragonflower : SingleCountDependant {
        public string DragonflowerType;

        public Dragonflower(FEHDataExtractorLib.SingleCountDependant singleCountDependant) : base(singleCountDependant) {
            this.Type = RewardType.DRAGONFLOWER;
        }

        public Dragonflower(FEHDataExtractorLib.Dragonflower dragonflower) : this(dragonflower as FEHDataExtractorLib.SingleCountDependant) {
            this.DragonflowerType = Base.MovementTypes.Get(dragonflower.Type);
        }
    }
}
